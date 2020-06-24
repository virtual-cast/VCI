using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using NAudio.Wave;
using NLayer.NAudioSupport;
using UnityEngine;
using VCIGLTF;
using Debug = UnityEngine.Debug;

namespace VCI
{
    public static class AudioImporter
    {
        public static IEnumerator Import(glTF_VCAST_vci_audio audio, ArraySegment<byte> bytes, GameObject target)
        {
            if (audio.mimeType == "audio/mp3")
            {
                MemoryStream wavStream = null;
                var task = Task.Run(() => wavStream = ToWavData(bytes));

                while (true)
                    if (task.IsCompleted || task.IsFaulted || task.IsCanceled)
                        break;
                    else
                        yield return null;

                if (wavStream == null) yield break;

                using (wavStream)
                {
                    yield return CreateClip(wavStream, 0, audio.name, target);
                }
            }
            else
            {
                using (var wavStream = new MemoryStream(bytes.Array, bytes.Offset, bytes.Count, false, true))
                {
                    yield return CreateClip(wavStream, bytes.Offset, audio.name, target);
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

        private static IEnumerator CreateClip(MemoryStream ms, int offset, string name, GameObject target)
        {
            if (!WaveUtil.TryReadHeader(ms, out var waveHeader))
            {
                Debug.LogWarning("Cannot read wave header.");
                yield break;
            }

            AudioClip audioClip = null;
            yield return AudioClipMaker.Create(
                name,
                ms.GetBuffer(),
                offset + waveHeader.FormatChunkSize + 28,
                waveHeader.BitPerSample,
                waveHeader.DataChunkSize / waveHeader.BlockSize,
                waveHeader.Channel,
                waveHeader.SampleRate,
                false,
                clip => { audioClip = clip; });

            if (audioClip == null) yield break;
            
            var audioSource = target.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }
}
