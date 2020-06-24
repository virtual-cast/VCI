#if ((NET_4_6 || NET_STANDARD_2_0) && UNITY_2017_1_OR_NEWER)
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace VCI
{
    public static class AudioClipMaker
    {
        private static readonly float RANGE_VALUE_BIT_8 = 1.0f / Mathf.Pow(2, 7); // 1 / 128
        private static readonly float RANGE_VALUE_BIT_16 = 1.0f / Mathf.Pow(2, 15); // 1 / 32768
        private static readonly int BIT_8 = 8;
        private static readonly int BIT_16 = 16;
        private static readonly int BIT_24 = 24;

        public static IEnumerator Create(
            string name,
            byte[] rawData,
            int wavBufIdx,
            int bitPerSample,
            int samples,
            int channels,
            int frequency,
            bool isStream,
            Action<AudioClip> callback
        )
        {
            var task = Task.Run(() => CreateRangedRawData(rawData, wavBufIdx, samples, channels, bitPerSample));

            while (true)
                if (task.IsCompleted || task.IsFaulted || task.IsCanceled)
                    break;
                else
                    yield return null;

            callback(Create(name, task.Result, samples, channels, frequency, isStream));
        }

        private static AudioClip Create(
            string name,
            float[] rangedData,
            int samples,
            int channels,
            int frequency,
            bool isStream
        )
        {
            var clip = AudioClip.Create(name, samples, channels, frequency, isStream);
            clip.SetData(rangedData, 0);
            return clip;
        }

        private static float[] CreateRangedRawData(byte[] rawData, int wavBufIdx, int samples, int channels,
            int bitPerSample)
        {
            var rangedRawData = new float[samples * channels];

            var stepByte = bitPerSample / BIT_8;
            var nowIdx = wavBufIdx;

            for (var i = 0; i < samples * channels; ++i)
            {
                rangedRawData[i] = ConvertByteToFloatData(rawData, nowIdx, bitPerSample);
                nowIdx += stepByte;
            }

            return rangedRawData;
        }

        private static float ConvertByteToFloatData(byte[] rawData, int idx, int bitPerSample)
        {
            var floatData = 0.0f;
            try
            {
                if (bitPerSample == BIT_8)
                {
                    floatData = ((int) rawData[idx] - 0x80) * RANGE_VALUE_BIT_8;
                }
                else if (bitPerSample == BIT_16)
                {
                    floatData = BitConverter.ToInt16(rawData, idx) * RANGE_VALUE_BIT_16;
                }
                else if (bitPerSample == BIT_24)
                {
                    // skip low bit
                    floatData = BitConverter.ToInt16(rawData, idx + 1) * RANGE_VALUE_BIT_16;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
                throw;
            }
            return floatData;
        }
    }
}
#endif