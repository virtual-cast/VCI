using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using NAudio.Wave;
using NLayer.NAudioSupport;
using UnityEngine;
using VRMShaders;
using Debug = UnityEngine.Debug;

namespace VCI
{
    public static class RuntimeAudioClipDeserializer
    {
        public static async Task<AudioClip> ImportAsync(string name, string mimeType, ArraySegment<byte> bytes, IAwaitCaller awaitCaller)
        {
            if (mimeType == AudioJsonObject.Mp3MimeType)
            {
                MemoryStream wavStream = null;
                await awaitCaller.Run(() => wavStream = ToWavData(bytes));
                if (wavStream == null) return default;

                using (wavStream)
                {
                    return await CreateClipAsync(wavStream, 0, name, awaitCaller);
                }
            }
            else
            {
                // NOTE: mimeType が audio/wav または不明値の場合、wav として読み込む.
                using (var wavStream = new MemoryStream(bytes.Array, bytes.Offset, bytes.Count, false, true))
                {
                    return await CreateClipAsync(wavStream, bytes.Offset, name, awaitCaller);
                }
            }
        }

        private static MemoryStream ToWavData(ArraySegment<byte> bytes)
        {
            try
            {
                using (var ms = new MemoryStream(bytes.Array, bytes.Offset, bytes.Count))
                using (var reader = new Mp3FileReader(ms, wf => new Mp3FrameDecompressor(wf)))
                {
                    var outStream = new MemoryStream();
                    WaveFileWriter.WriteWavFileToStream(outStream, new WaveFloatTo16Provider(reader));
                    outStream.Position = 0;
                    return outStream;
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                return null;
            }
        }

        private static async Task<AudioClip> CreateClipAsync(MemoryStream ms, int offset, string name, IAwaitCaller awaitCaller)
        {
            if (!WaveUtil.TryReadHeader(ms, out var waveHeader))
            {
                Debug.LogWarning("Cannot read wave header.");
                return default;
            }

            return await RuntimeAudioClipAssetCreator.CreateAsync(
                name,
                ms.GetBuffer(),
                offset + waveHeader.FormatChunkSize + 28,
                waveHeader.BitPerSample,
                waveHeader.DataChunkSize / waveHeader.BlockSize,
                waveHeader.Channel,
                waveHeader.SampleRate,
                false,
                awaitCaller);
        }
    }
}
