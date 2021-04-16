using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniGLTF;

namespace VCI
{
    public sealed class EachAnimationImporter : IAnimationImporter
    {
        public List<AnimationClip> Import(glTF gltf, GameObject root, List<Transform> nodes, List<AnimationClip> clips, Axises invertAxis)
        {
            if (Application.isPlaying)
            {
                return RuntimeImport(gltf, root, nodes, invertAxis.Create());
            }
            else
            {
                return EditorImport(gltf, root, nodes, clips, invertAxis.Create());
            }
        }

        private List<AnimationClip> RuntimeImport(glTF gltf, GameObject root, List<Transform> nodes, IAxisInverter inverter)
        {
            var animationClips = new AnimationClip[gltf.animations.Count];

            // node extension animation
            for (int i = 0; i < gltf.nodes.Count; i++)
            {
                var node = gltf.nodes[i];
                if (node.extensions == null)
                {
                    continue;
                }

                glTF_VCAST_vci_animation extensions;
                if (!node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_animation.ExtensionName, glTF_VCAST_vci_animation_Deserializer.Deserialize, out extensions))
                {
                    continue;
                }

                var vciAnimation = extensions;
                var animation = nodes[i].gameObject.AddComponent<Animation>();

                foreach (var animationReference in vciAnimation.animationReferences)
                {
                    var gltfAnimation = gltf.animations[animationReference.animation];
                    AnimationClip clip = null;
                    if (animationClips[animationReference.animation] == null)
                    {
                        clip = AnimationImporterUtil.ConvertAnimationClip(gltf, gltfAnimation, inverter, node);
                        animationClips[animationReference.animation] = clip;
                    }
                    else
                    {
                        clip = animationClips[animationReference.animation];
                    }

                    if (clip != null)
                    {
                        animation.AddClip(clip, clip.name);
                    }
                }
            }

            // root animation
            var rootAnimation = root.GetComponent<Animation>();
            if (rootAnimation == null)
                rootAnimation = root.AddComponent<Animation>();

            for (int i = 0; i < animationClips.Length; i++)
            {
                if (animationClips[i] != null)
                    continue;

                var gltfAnimation = gltf.animations[i];

                animationClips[i] = AnimationImporterUtil.ConvertAnimationClip(gltf, gltfAnimation, inverter);
                rootAnimation.AddClip(animationClips[i], animationClips[i].name);
            }

            return new List<AnimationClip>(animationClips);
        }

        private List<AnimationClip> EditorImport(glTF gltf, GameObject root, List<Transform> nodes, List<AnimationClip> clips, IAxisInverter inverter)
        {
            var animationClips = new AnimationClip[gltf.animations.Count];
            List<AnimationClip> usedClips = new List<AnimationClip>();

            // node extension animation
            for (int i = 0; i < gltf.nodes.Count; i++)
            {
                var node = gltf.nodes[i];
                if (node.extensions == null)
                {
                    continue;
                }

                glTF_VCAST_vci_animation extensions;
                if (!node.extensions.TryDeserializeExtensions(glTF_VCAST_vci_animation.ExtensionName, glTF_VCAST_vci_animation_Deserializer.Deserialize, out extensions))
                {
                    continue;
                }

                var vciAnimation = extensions;
                var animation = nodes[i].gameObject.AddComponent<Animation>();

                foreach (var animationReference in vciAnimation.animationReferences)
                {
                    if (clips[animationReference.animation] != null)
                    {
                        var clip = clips[animationReference.animation];
                        animation.AddClip(clip, clip.name);
                        usedClips.Add(clip);
                    }
                }
            }

            // root animation
            var rootAnimation = root.GetComponent<Animation>();
            if (rootAnimation == null)
                rootAnimation = root.AddComponent<Animation>();

            foreach (var clip in clips)
            {
                if (!usedClips.Contains(clip))
                {
                    rootAnimation.AddClip(clip, clip.name);
                }
            }

            return new List<AnimationClip>(animationClips);
        }
    }
}