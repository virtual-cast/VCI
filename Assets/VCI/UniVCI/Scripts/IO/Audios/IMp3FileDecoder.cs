using System;
using System.Threading;
using System.Threading.Tasks;
using VRMShaders;

namespace VCI
{
    public interface IMp3FileDecoder
    {
        Task<Mp3DecodeResult> DecodeFromRawBytesAsync(byte[] mp3Bytes, IAwaitCaller awaitCaller, CancellationToken ct = default);
    }

    public readonly struct Mp3DecodeResult
    {
        public float[] Samples { get; }
        public int SamplingFrequency { get; }
        public int Channels { get; }
        public int SamplesPerChannel => Samples.Length / Channels;

        public Mp3DecodeResult(float[] samples, int samplingFrequency, int channels)
        {
            Samples = samples;
            SamplingFrequency = samplingFrequency;
            Channels = channels;
        }
    }

    public sealed class VciMp3FileDecodeException : Exception
    {
        public VciMp3FileDecodeException(string message) : base(message)
        {
        }
    }
}
