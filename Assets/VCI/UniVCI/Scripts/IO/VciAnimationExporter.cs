using System;
using System.Linq;
using UniGLTF;

namespace VCI
{
    public class VciAnimationExporter
    {
        public static void WriteAnimationWithSampleCurves(glTF gltf, AnimationExporter.AnimationWithSampleCurves animationWithCurve, string animationName, int bufferIndex)
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

                var inputAccessorIndex = gltf.ExtendBufferAndGetAccessorIndex( bufferIndex, kv.Value.Input);
                sampler.input = inputAccessorIndex;


                var outputAccessorIndex =
                    gltf.ExtendBufferAndGetAccessorIndex(bufferIndex, kv.Value.Output);
                sampler.output = outputAccessorIndex;

                // modify accessors
                var outputAccessor = gltf.accessors[outputAccessorIndex];
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
            gltf.animations.Add(animationWithCurve.Animation);
        }
    }
}
