using System.Collections.Generic;
using UnityEngine;

namespace VCIGLTF
{
    public class EachAnimationImporter : IAnimationImporter
    {
        public void Import(ImporterContext context)
        {
            var animationClips = new AnimationClip[context.GLTF.animations.Count];

            // node extension animation
            for (int i = 0;i < context.GLTF.nodes.Count; i++)
            {
                var node = context.GLTF.nodes[i];
                if (node.extensions == null || node.extensions.VCAST_vci_animation == null)
                    continue;

                var vciAnimation = node.extensions.VCAST_vci_animation;
                var root = context.Nodes[i];
                var animation = root.gameObject.AddComponent<Animation>();

                foreach (var animationReference in vciAnimation.animationReferences)
                {
                    var gltfAnimation = context.GLTF.animations[animationReference.animation];
                    AnimationClip clip = null;
                    if(animationClips[animationReference.animation] == null)
                    {
                        clip = AnimationImporterUtil.ImportAnimationClip(context, gltfAnimation, root);
                        animationClips[animationReference.animation] = clip;
                    }
                    else
                    {
                        clip = animationClips[animationReference.animation];
                    }
                        
                    if(clip != null)
                    {
                        animation.AddClip(clip, clip.name);
                    }
                }
            }

            // root animation
            var rootAnimation = context.Root.GetComponent<Animation>();
            if (rootAnimation == null)
                rootAnimation = context.Root.AddComponent<Animation>();

            for (int i=0;i< animationClips.Length; i++)
            {
                if (animationClips[i] != null)
                    continue;

                var gltfAnimation = context.GLTF.animations[i];

                animationClips[i] = AnimationImporterUtil.ImportAnimationClip(context, gltfAnimation, context.Root.transform);
                rootAnimation.AddClip(animationClips[i], animationClips[i].name);
            }

            context.AnimationClips = new List<AnimationClip>(animationClips);
        }
    }
}