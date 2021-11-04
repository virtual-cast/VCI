using System.Collections.Generic;
using System.IO;
using System.Linq;
using UniGLTF;
using UnityEditor;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// Editor では、Effekseer アセットは efk、image などエフェクトごとにアセットを保存しなければならない。
    /// </summary>
    public sealed class EditorEffekseerImporter
    {
        private readonly VciData _vciData;
        private readonly VCIImporter _context;
        private readonly UnityPath _prefabFilePath;
        private readonly glTF_Effekseer _effekseerExtension;

        private UnityPath EffekseerDir => _prefabFilePath.GetAssetFolder(".Effekseers");

        public EditorEffekseerImporter(VciData vciData, VCIImporter context, UnityPath prefabFilePath)
        {
            _vciData = vciData;
            _context = context;
            _prefabFilePath = prefabFilePath;
            _effekseerExtension = _vciData.Effekseer;
        }

        private UnityPath GetEffectDirectory(EffekseerEffectJsonObject effect)
        {
            return EffekseerDir.Child(effect.effectName);
        }

        private UnityPath GetEffectTextureDirectory(EffekseerEffectJsonObject effect)
        {
            return GetEffectDirectory(effect).Child("Textures");
        }

        private UnityPath GetEffectModelDirectory(EffekseerEffectJsonObject effect)
        {
            return GetEffectDirectory(effect).Child("Textures");
        }

        /// <summary>
        /// ファイルを Project に Write するだけ
        /// </summary>
        public void ExtractAssetFiles()
        {
            if (_effekseerExtension?.effects == null)
            {
                return;
            }

            if (_effekseerExtension.effects.Count > 0)
            {
                EffekseerDir.EnsureFolder();
            }

            for (var i = 0; i < _effekseerExtension.effects.Count; ++i)
            {
                var effect = _effekseerExtension.effects[i];
                GetEffectDirectory(effect).EnsureFolder();

                var body = _context.Data.GetBytesFromBufferView(effect.body.bufferView).ToArray();
                var resourcePath = new Effekseer.EffekseerResourcePath();
                if (!Effekseer.EffekseerEffectAsset.ReadResourcePath(body, ref resourcePath))
                {
                    continue;
                }

                // Texture
                if (effect.images != null && effect.images.Any())
                {
                    var textureDir = GetEffectTextureDirectory(effect);
                    textureDir.EnsureFolder();

                    for (int t = 0; t < effect.images.Count; t++)
                    {
                        var image = effect.images[t];
                        var path = resourcePath.TexturePathList[t];
                        var buffer = _context.Data.GetBytesFromBufferView(image.bufferView);
                        var textureFilePath = textureDir.Child(Path.GetFileName(path));

                        if (image.mimeType == EffekseerImageJsonObject.PngMimeTypeString)
                        {
                            File.WriteAllBytes(textureFilePath.FullPath, buffer.ToArray());
                        }
                        else
                        {
                            Debug.LogError(string.Format("image format {0} is not suppported.", image.mimeType));
                        }
                    }
                }

                // Models
                if (effect.models != null && effect.models.Any())
                {
                    var modelDir = GetEffectModelDirectory(effect);
                    modelDir.EnsureFolder();

                    for (int t = 0; t < effect.models.Count; t++)
                    {
                        var model = effect.models[t];
                        var path = resourcePath.ModelPathList[t];
                        var buffer = _context.Data.GetBytesFromBufferView(model.bufferView);

                        var modelFilePath = modelDir.Child(Path.GetFileName(path));

                        File.WriteAllBytes(modelFilePath.FullPath, buffer.ToArray());
                    }
                }
            }
        }

        public void SetupAfterEditorDelayCall()
        {
            if (_effekseerExtension?.effects == null)
            {
                return;
            }

            var effects = new Dictionary<int, Effekseer.EffekseerEffectAsset>();

            for (var effectIdx = 0; effectIdx < _effekseerExtension.effects.Count; ++effectIdx)
            {
                var effect = _effekseerExtension.effects[effectIdx];
                var effectDir = GetEffectDirectory(effect);

                // efk file
                var effectFilePath = effectDir.Child($"{effect.effectName}.efk");
                var body = _context.Data.GetBytesFromBufferView(effect.body.bufferView).ToArray();
                Effekseer.EffekseerEffectAsset.CreateAsset(effectFilePath.Value, body);
                var effectAsset = AssetDatabase.LoadAssetAtPath<Effekseer.EffekseerEffectAsset>(
                    System.IO.Path.ChangeExtension(effectFilePath.Value, "asset"));
                effects.Add(effectIdx, effectAsset);

                // find assets
                // textures
                for (int t = 0; t < effectAsset.textureResources.Count(); t++)
                {
                    var path = effectAsset.textureResources[t].path;
                    var fileName = Path.GetFileName(path);
                    var textureFilePath = GetEffectTextureDirectory(effect).Child(fileName);
                    if (!textureFilePath.IsFileExists) continue;

                    var texture = textureFilePath.LoadAsset<Texture2D>();
                    effectAsset.textureResources[t].texture = texture;

                    // texture Importer settings
                    var textureImporter = (TextureImporter) TextureImporter.GetAtPath(textureFilePath.Value);
                    if (textureImporter != null)
                    {
                        textureImporter.isReadable = true;
                        textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
                        textureImporter.SaveAndReimport();
                    }
                }

                // models
                for (int t = 0; t < effectAsset.modelResources.Count(); t++)
                {
                    var path = effectAsset.modelResources[t].path;
                    var fileName = Path.GetFileName(path);
                    var modelFilePath = GetEffectModelDirectory(effect).Child(fileName);
                    if (!modelFilePath.IsFileExists) continue;

                    var model = modelFilePath.LoadAsset<Effekseer.EffekseerModelAsset>();
                    effectAsset.modelResources[t].asset = model;
                }

                // NOTE: Effekseer エフェクトアセットに変更があるので、フラグを立てる必要がある.
                EditorUtility.SetDirty(effectAsset);
            }

            foreach (var (nodeIdx, effekseerEmitterExtension) in _vciData.EffekseerEmittersNodes)
            {
                var gameObject = _context.Nodes[nodeIdx].gameObject;

                if (effekseerEmitterExtension == null || effekseerEmitterExtension.emitters == null)
                {
                    continue;
                }

                foreach (var emitter in effekseerEmitterExtension.emitters)
                {
                    var effectIndex = emitter.effectIndex;
                    var emitterComponent = gameObject.GetOrAddComponent<Effekseer.EffekseerEmitter>();
                    emitterComponent.effectAsset = effects[effectIndex];
                    emitterComponent.playOnStart = emitter.isPlayOnStart;
                    emitterComponent.isLooping = emitter.isLoop;
                }
            }
        }
    }
}