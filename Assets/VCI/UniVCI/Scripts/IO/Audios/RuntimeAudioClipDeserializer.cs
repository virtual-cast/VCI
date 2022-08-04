using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using NAudio.Wave;
using NLayer.NAudioSupport;
using Unity.Collections;
using UnityEngine;
using VRMShaders;
using Debug = UnityEngine.Debug;

namespace VCI
{
    public static class RuntimeAudioClipDeserializer
    {
        public static async Task<AudioClip> ImportAsync(string name, string mimeType, NativeSlice<byte> bytes, IAwaitCaller awaitCaller)
        {
            // NOTE: Copy
            var data = bytes.ToArray();

            if (mimeType == AudioJsonObject.Mp3MimeType)
            {
                var wavData = await awaitCaller.Run(() => ToWavData(data));
                if (wavData == null) return default;

                return await CreateClipAsync(wavData, name, awaitCaller);
            }
            else
            {
                // NOTE: mimeType が audio/wav または不明値の場合、wav として読み込む.
                return await CreateClipAsync(data, name, awaitCaller);
            }
        }

        private static byte[] ToWavData(byte[] data)
        {
            try
            {
                using (var ms = new MemoryStream(data))
                using (var reader = new Mp3FileReader(ms, wf => new Mp3FrameDecompressor(wf)))
                using (var outStream = new MemoryStream())
                {
                    WaveFileWriter.WriteWavFileToStream(outStream, new WaveFloatTo16Provider(reader));
                    return outStream.ToArray();
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                return null;
            }
        }

        private static async Task<AudioClip> CreateClipAsync(byte[] wavData, string name, IAwaitCaller awaitCaller)
        {
            using (var memoryStream = new MemoryStream(wavData))
            {
                if (!WaveUtil.TryReadHeader(memoryStream, out var waveHeader))
                {
                    Debug.LogWarning("Cannot read wave header.");
                    return default;
                }

                return await RuntimeAudioClipAssetCreator.CreateAsync(
                    name,
                    wavData,
                    waveHeader.FormatChunkSize + 28,
                    waveHeader.BitPerSample,
                    waveHeader.DataChunkSize / waveHeader.BlockSize,
                    waveHeader.Channel,
                    waveHeader.SampleRate,
                    false,
                    awaitCaller);
            }
        }
    }
}
