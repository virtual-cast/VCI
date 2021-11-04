using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;
using VRMShaders;
using ColorSpace = VRMShaders.ColorSpace;

namespace VCI
{
    public static class EffekseerExporter
    {
        public static (glTF_Effekseer, List<(glTFNode, glTF_Effekseer_emitters)>)? ExportEffekseer(
            ExportingGltfData exportingData,
            List<Transform> nodes,
            ITextureSerializer textureSerializer)
        {
            // Effekseer
            var effekseerExtension = new glTF_Effekseer()
            {
                effects = new List<EffekseerEffectJsonObject>()
            };

            // Effekseer emitter
            var emittersExtensions = new List<(glTFNode, glTF_Effekseer_emitters)>();
            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                var gltfNode = exportingData.GLTF.nodes[i];

                var emitters = node.GetComponents<Effekseer.EffekseerEmitter>()
                    .Where(emitter => emitter.effectAsset != null)
                    .ToArray();

                if (emitters != null && emitters.Length > 0)
                {
                    var emittersExtension = new glTF_Effekseer_emitters()
                    {
                        emitters = new List<EffekseerEmitterJsonObject>()
                    };

                    foreach (var emitter in emitters)
                    {
                        var effectIndex = AddEffekseerEffect(exportingData, effekseerExtension, emitter, textureSerializer);
                        emittersExtension.emitters.Add(new EffekseerEmitterJsonObject()
                        {
                            effectIndex = effectIndex,
                            isLoop = emitter.isLooping,
                            isPlayOnStart = emitter.playOnStart
                        });
                    }

                    emittersExtensions.Add((gltfNode, emittersExtension));
                }
            }

            if (effekseerExtension.effects.Count == 0)
            {
                return null;
            }

            return (effekseerExtension, emittersExtensions);
        }

        private static int AddEffekseerEffect(
            ExportingGltfData exportingData,
            glTF_Effekseer effekseerExtension,
            Effekseer.EffekseerEmitter emitterExtension,
            ITextureSerializer textureSerializer)
        {
            if(effekseerExtension.effects.FirstOrDefault(x => x.effectName == emitterExtension.effectAsset.name) == null)
            {
                var viewIndex = exportingData.ExtendBufferAndGetViewIndex(emitterExtension.effectAsset.efkBytes);

                // body
                var effect = new EffekseerEffectJsonObject()
                {
                    nodeIndex = 0,
                    nodeName = "Root",
                    effectName = emitterExtension.effectAsset.name,
                    scale = emitterExtension.effectAsset.Scale,
                    body = new EffekseerBodyJsonObject() {bufferView = viewIndex},
                    images = new List<EffekseerImageJsonObject>(),
                    models = new List<EffekseerModelJsonObject>()
                };

                // texture
                foreach (var texture in emitterExtension.effectAsset.textureResources)
                {
                    if (texture == null || texture.texture == null)
                    {
                        Debug.LogWarning("Effekseer Texture Asset is null. " + texture?.path);
                        continue;
                    }

                    FixTextureImporterSettings(texture.texture);

                    var (textureBytes, textureMime) = textureSerializer.ExportBytesWithMime(texture.texture, ColorSpace.sRGB);
                    var image = new EffekseerImageJsonObject()
                    {
                        bufferView = exportingData.ExtendBufferAndGetViewIndex(textureBytes),
                        mimeType = textureMime,
                    };
                    effect.images.Add(image);
                }

                // model
                foreach (var model in emitterExtension.effectAsset.modelResources)
                {
                    if (model == null || model.asset == null)
                    {
                        Debug.LogWarning("Effekseer Model Asset is null. " + model?.path);
                        continue;
                    }

                    var efkModel = new EffekseerModelJsonObject()
                    {
                        bufferView = exportingData.ExtendBufferAndGetViewIndex(model.asset.bytes)
                    };
                    effect.models.Add(efkModel);
                }

                effekseerExtension.effects.Add(effect);
                int index = effekseerExtension.effects.Count - 1;
                return index;
            }
            else
            {
                return effekseerExtension.effects.FindIndex(x => x.effectName == emitterExtension.effectAsset.name);
            }
        }

        private static void FixTextureImporterSettings(Texture2D texture)
        {
            if (Application.isPlaying || texture == null)
            {
                return;
            }

#if UNITY_EDITOR
            var texturePath = UnityEditor.AssetDatabase.GetAssetPath(texture);
            var textureImporter =
                (UnityEditor.TextureImporter) UnityEditor.TextureImporter.GetAtPath(texturePath);
            if (textureImporter != null)
            {
                textureImporter.isReadable = true;
                textureImporter.textureCompression = UnityEditor.TextureImporterCompression.Uncompressed;
                textureImporter.SaveAndReimport();
            }
#endif
        }
    }
}