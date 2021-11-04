using System;
using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// glTF ではサポートされない、ノードごとに独立した Animation を Export できる。
    /// </summary>
    public static class AnimationNodeExporter
    {
        public static List<(glTFNode, glTF_VCAST_vci_animation)> ExportAnimations(ExportingGltfData exportingData, GameObject root, List<Transform> nodes)
        {
            var animationExtensions = new List<(glTFNode, glTF_VCAST_vci_animation)>();
            if (Application.isPlaying)
            {
                // No support at Runtime
                return animationExtensions;
            }

#if UNITY_EDITOR
            // Animation
            // None RootAnimation
            var animators = root.GetComponentsInChildren<Animator>()
                .Where(x => root != x.gameObject)
                .ToArray();
            var animations = root.GetComponentsInChildren<Animation>()
                .Where(x => root != x.gameObject)
                .ToArray();
            // NodeIndex to AnimationClips
            var animationNodeList = new Dictionary<int, AnimationClip[]>();

            foreach (var animator in animators)
            {
                var animationClips = AnimationExporter.GetAnimationClips(animator);
                var nodeIndex = nodes.FindIndex(0, nodes.Count, x => x == animator.transform);
                if (animationClips.Any() && nodeIndex != -1)
                {
                    animationNodeList.Add(nodeIndex, animationClips.ToArray());
                }
            }

            foreach (var animation in animations)
            {
                var animationClips = AnimationExporter.GetAnimationClips(animation);
                var nodeIndex = nodes.FindIndex(0, nodes.Count, x => x == animation.transform);
                if (animationClips.Any() && nodeIndex != -1)
                {
                    animationNodeList.Add(nodeIndex, animationClips.ToArray());
                }
            }

            foreach (var animationNode in animationNodeList)
            {
                var clipIndices = new List<int>();
                // write animationClips
                foreach (var clip in animationNode.Value)
                {
                    var animationWithCurve = AnimationExporter.Export(clip, nodes[animationNode.Key], nodes);
                    WriteAnimationWithSampleCurves(exportingData, animationWithCurve, clip.name);
                    clipIndices.Add(exportingData.GLTF.animations.IndexOf(animationWithCurve.Animation));
                }

                // write node
                if (clipIndices.Count > 0)
                {
                    var gltfNode = exportingData.GLTF.nodes[animationNode.Key];

                    var animationExtension = new glTF_VCAST_vci_animation()
                    {
                        animationReferences = new List<AnimationReferenceJsonObject>()
                    };

                    foreach (var index in clipIndices)
                    {
                        animationExtension.animationReferences.Add(
                            new AnimationReferenceJsonObject() {animation = index});
                    }

                    animationExtensions.Add((gltfNode, animationExtension));
                }
            }
#endif

            return animationExtensions;
        }

        private static void WriteAnimationWithSampleCurves(ExportingGltfData exportingData, UniGLTF.AnimationExporter.AnimationWithSampleCurves animationWithCurve, string animationName)
        {
            foreach (var kv in animationWithCurve.SamplerMap)
            {
                var sampler = animationWithCurve.Animation.samplers[kv.Key];

                float min = float.PositiveInfinity;
                float max = float.NegativeInfinity;
                foreach (float value in kv.Value.Input)
                {
                    if (value < min)
                    {
                        min = value;
                    }
                    if (value > max)
                    {
                        max = value;
                    }
                }

                var inputAccessorIndex = exportingData.ExtendBufferAndGetAccessorIndex(kv.Value.Input);
                sampler.input = inputAccessorIndex;


                var outputAccessorIndex = exportingData.ExtendBufferAndGetAccessorIndex(kv.Value.Output);
                sampler.output = outputAccessorIndex;

                // modify accessors
                var outputAccessor = exportingData.GLTF.accessors[outputAccessorIndex];
                var channel = animationWithCurve.Animation.channels.First(x => x.sampler == kv.Key);
                switch (glTFAnimationTarget.GetElementCount(channel.target.path))
                {
                    case 1:
                        outputAccessor.type = "SCALAR";
                        //outputAccessor.count = ;
                        break;
                    case 3:
                        outputAccessor.type = "VEC3";
                        outputAccessor.count /= 3;
                        break;

                    case 4:
                        outputAccessor.type = "VEC4";
                        outputAccessor.count /= 4;
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }

            animationWithCurve.Animation.name = animationName;
            exportingData.GLTF.animations.Add(animationWithCurve.Animation);
        }
    }
}