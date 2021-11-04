using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UniGLTF;
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
                    var bodySegment = gltfData.GetBytesFromBufferView(effect.body.bufferView);
                    var body = new byte[bodySegment.Count];
                    Buffer.BlockCopy(bodySegment.Array, bodySegment.Offset, body, 0, body.Count());

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
                            var buffer = gltfData.GetBytesFromBufferView(image.bufferView);

                            if (image.mimeType == EffekseerImageJsonObject.PngMimeTypeString)
                            {
                                var copyBuffer = new byte[buffer.Count];
                                Buffer.BlockCopy(buffer.Array, buffer.Offset, copyBuffer, 0,
                                    copyBuffer.Count());
                                var texture = await textureFactory.TextureDeserializer.LoadTextureAsync(copyBuffer, false, ColorSpace.sRGB, awaitCaller);
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
                            var modelSegment = gltfData.GetBytesFromBufferView(model.bufferView);
                            byte[] modelBuffer = new byte[modelSegment.Count];
                            Buffer.BlockCopy(modelSegment.Array, modelSegment.Offset, modelBuffer, 0, modelBuffer.Count());

                            effekseerModels.Add(new Effekseer.Internal.EffekseerModelResource()
                            {
                                path = path,
                                asset = new Effekseer.EffekseerModelAsset() {bytes = modelBuffer}
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
                    effectAsset.soundResources = new Effekseer.Internal.EffekseerSoundResource[0];

                    var emitterComponent = go.AddComponent<Effekseer.EffekseerEmitter>();
                    emitterComponent.effectAsset = effectAsset;
                    emitterComponent.playOnStart = emitter.isPlayOnStart;
                    emitterComponent.isLooping = emitter.isLoop;

                    emitterComponent.effectAsset.LoadEffect();
                    effekseerEmitterComponents.Add(emitterComponent);

                    await awaitCaller.NextFrame();
                }
            }

            return effekseerEmitterComponents;
        }


    }
}