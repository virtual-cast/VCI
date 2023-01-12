using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Effekseer;
using UnityEngine;
using VRMShaders;
using ColorSpace = VRMShaders.ColorSpace;

namespace VCI
{
    public static class EffekseerImporter
    {
        public static async Task<List<Effekseer.EffekseerEmitter>> LoadAsync(
            VciData vciData,
            IReadOnlyList<Transform> unityNodes,
            TextureFactory textureFactory,
            IAwaitCaller awaitCaller)
        {
            var effekseerEmitterComponents = new List<Effekseer.EffekseerEmitter>();

            if (!Application.isPlaying)
            {
                // NOTE: Editor Import では EditorEffekseerImporter が動作する。
                return effekseerEmitterComponents;
            }

            // NOTE: Effekseer の Root 拡張がない場合は、なにもしない.
            if (vciData.Effekseer == null)
            {
                return effekseerEmitterComponents;
            }

            var gltfData = vciData.GltfData;

            foreach (var (nodeIdx, effekseerEmitterExtension) in vciData.EffekseerEmittersNodes)
            {
                if (effekseerEmitterExtension?.emitters == null)
                {
                    continue;
                }
                var go = unityNodes[nodeIdx].gameObject;

                foreach (var emitter in effekseerEmitterExtension.emitters)
                {
                    var effectIndex = emitter.effectIndex;

                    var effect = vciData.Effekseer.effects[effectIndex];
                    // NOTE: Copy
                    var body = gltfData.GetBytesFromBufferView(effect.body.bufferView).ToArray();

                    var resourcePath = new Effekseer.EffekseerResourcePath();
                    if (!Effekseer.EffekseerEffectAsset.ReadResourcePath(body, ref resourcePath))
                    {
                        continue;
                    }

                    // Images
                    var effekseerTextures = new List<Effekseer.Internal.EffekseerTextureResource>();
                    if (effect.images != null && effect.images.Any())
                    {
                        for (int t = 0; t < effect.images.Count; t++)
                        {
                            var image = effect.images[t];
                            var path = resourcePath.TexturePathList[t];
                            // NOTE: Copy
                            var buffer = gltfData.GetBytesFromBufferView(image.bufferView).ToArray();

                            if (image.mimeType == EffekseerImageJsonObject.PngMimeTypeString)
                            {
                                var samplerParams = new SamplerParam(TextureWrapMode.Repeat, TextureWrapMode.Repeat, FilterMode.Bilinear, false);
                                var textureInfo = new DeserializingTextureInfo(buffer, image.mimeType, ColorSpace.sRGB, samplerParams);
                                var texture = await textureFactory.TextureDeserializer.LoadTextureAsync(textureInfo, awaitCaller);
                                effekseerTextures.Add(new Effekseer.Internal.EffekseerTextureResource()
                                {
                                    path = path,
                                    texture = texture
                                });
                            }
                            else
                            {
                                // NOTE: 不明な mimetype の画像が現れた場合、ロードできない.
                                Debug.LogError($"image format {image.mimeType} is not supported.");
                            }
                        }
                    }

                    // Models
                    var effekseerModels = new List<Effekseer.Internal.EffekseerModelResource>();
                    if (effect.models != null && effect.models.Any())
                    {
                        for (int t = 0; t < effect.models.Count; t++)
                        {
                            var model = effect.models[t];
                            var path = resourcePath.ModelPathList[t];
                            path = Path.ChangeExtension(path, "asset");
                            // NOTE: Copy
                            var modelBuffer = gltfData.GetBytesFromBufferView(model.bufferView).ToArray();
                            var modelAsset = ScriptableObject.CreateInstance<Effekseer.EffekseerModelAsset>();
                            modelAsset.bytes = modelBuffer;
                            effekseerModels.Add(new Effekseer.Internal.EffekseerModelResource
                            {
                                path = path,
                                asset = modelAsset,
                            });
                        }
                    }

                    Effekseer.EffekseerEffectAsset effectAsset =
                        ScriptableObject.CreateInstance<Effekseer.EffekseerEffectAsset>();
                    effectAsset.name = effect.effectName;
                    effectAsset.efkBytes = body;
                    effectAsset.Scale = effect.scale;
                    effectAsset.textureResources = effekseerTextures.ToArray();
                    effectAsset.modelResources = effekseerModels.ToArray();
                    effectAsset.soundResources = Array.Empty<Effekseer.Internal.EffekseerSoundResource>();

                    var emitterComponent = go.AddComponent<Effekseer.EffekseerEmitter>();
                    emitterComponent.effectAsset = effectAsset;
                    emitterComponent.playOnStart = emitter.isPlayOnStart;
                    emitterComponent.isLooping = emitter.isLoop;

                    // 過去バージョンのVCIでは値が存在しないことがある
                    emitterComponent.EmitterScale = (emitter.emitterScale ?? "") switch
                    {
                        EffekseerEmitterJsonObject.LocalEmitterScale => EffekseerEmitterScale.Local,
                        EffekseerEmitterJsonObject.GlobalEmitterScale => EffekseerEmitterScale.Global,
                        _ => EffekseerEmitterScale.Local, // 値が存在しない場合や、不正な値が渡されたときのデフォルト挙動はlocal
                    };

                    emitterComponent.effectAsset.LoadEffect();
                    effekseerEmitterComponents.Add(emitterComponent);

                    await awaitCaller.NextFrame();
                }
            }

            return effekseerEmitterComponents;
        }


    }
}