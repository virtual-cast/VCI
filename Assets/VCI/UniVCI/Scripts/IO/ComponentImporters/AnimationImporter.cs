using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniGLTF;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public static class AnimationImporter
    {
        public static async Task LoadAsync(
            VciData data,
            AnimationClipFactory animationClipFactory,
            IAxisInverter inverter,
            IAwaitCaller awaitCaller)
        {
            var gltf = data.GltfData.GLTF;
            if (gltf.animations == null || gltf.animations.Count == 0)
            {
                return;
            }

            var subAssetKeys = AnimationImporterUtil.EnumerateSubAssetKeys(gltf).ToArray();
            var animationRootNodes = GetAnimationRootNodes(data);

            // Load
            for (var animationIdx = 0; animationIdx < gltf.animations.Count; ++animationIdx)
            {
                var key = subAssetKeys[animationIdx];
                var gltfAnimation = gltf.animations[animationIdx];
                var (_, rootNode) = animationRootNodes[animationIdx];

                await animationClipFactory.LoadAnimationClipAsync(key, async () =>
                {
                    return AnimationImporterUtil.ConvertAnimationClip(data.GltfData, gltfAnimation, inverter, rootNode);
                });
            }

            await awaitCaller.NextFrame();
        }

        public static async Task SetupAsync(
            VciData data,
            IReadOnlyList<Transform> unityNodes,
            GameObject unityRoot,
            AnimationClipFactory animationClipFactory,
            IAwaitCaller awaitCaller)
        {
            var gltf = data.GltfData.GLTF;
            var subAssetKeys = AnimationImporterUtil.EnumerateSubAssetKeys(gltf).ToArray();
            var animationRootNodes = GetAnimationRootNodes(data);

            for (var animationIdx = 0; animationIdx < gltf.animations.Count; ++animationIdx)
            {
                var key = subAssetKeys[animationIdx];
                var (rootNodeIdx, _) = animationRootNodes[animationIdx];

                var rootNode = rootNodeIdx == -1 ? unityRoot : unityNodes[rootNodeIdx].gameObject;
                var animationComponent = rootNode.GetOrAddComponent<Animation>();
                var clip = animationClipFactory.GetAnimationClip(key);
                animationComponent.AddClip(clip, clip.name);
            }

            await awaitCaller.NextFrame();
        }

        private static List<(int nodeIdx, glTFNode node)> GetAnimationRootNodes(VciData data)
        {
            var gltf = data.GltfData.GLTF;

            // Default: Root Animation
            var animationRootNodes = gltf.animations.Select<glTFAnimation, (int nodeIdx, glTFNode node)>(x => (-1, null)).ToList();

            // Determine specified root nodes
            foreach (var (nodeIdx, vciAnimation) in data.AnimationNodes)
            {
                foreach (var animationReference in vciAnimation.animationReferences)
                {
                    var animationIdx = animationReference.animation;
                    if (animationIdx < 0 || animationIdx >= gltf.animations.Count) continue;

                    // Each Animation
                    animationRootNodes[animationIdx] = (nodeIdx, gltf.nodes[nodeIdx]);
                }
            }
            return animationRootNodes;
        }
    }
}