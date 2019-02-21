using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace VCI
{
    public static class AudioClipMaker
    {
        public static readonly float RANGE_VALUE_BIT_8 = 1.0f / Mathf.Pow(2, 7);   // 1 / 128
        public static readonly float RANGE_VALUE_BIT_16 = 1.0f / Mathf.Pow(2, 15); // 1 / 32768
        public static readonly int BASE_CONVERT_SAMPLES = 1024 * 20;
        public static int BIT_8 = 8;
        public static int BIT_16 = 16;

        public static IEnumerator Create(
            string name,
            byte[] raw_data,
            int wav_buf_idx,
            int bit_per_sample,
            int samples,
            int channels,
            int frequency,
            bool isStream,
            Action<AudioClip> callback
        )
        {
            var task = Task.Run<float[]>(()=>{
                return CreateRangedRawData(raw_data, wav_buf_idx, samples, channels, bit_per_sample);
            });

            while(true)
            {
                if(task.IsCompleted || task.IsFaulted || task.IsCanceled)
                {
                    break;
                }
                else
                {
                    yield return null;
                }
            }

            callback(Create(name, task.Result, samples, channels, frequency, isStream));
        }

        public static AudioClip Create(
            string name,
            float[] ranged_data,
            int samples,
            int channels,
            int frequency,
            bool isStream
        )
        {
            AudioClip clip = AudioClip.Create(name, samples, channels, frequency, isStream);
            clip.SetData(ranged_data, 0);

            return clip;
        }

        public static float[] CreateRangedRawData(byte[] byte_data, int wav_buf_idx, int samples, int channels, int bit_per_sample)
        {
            float[] ranged_rawdata = new float[samples * channels];

            int step_byte = bit_per_sample / BIT_8;
            int now_idx = wav_buf_idx;

            for (int i = 0; i < (samples * channels); ++i)
            {
                ranged_rawdata[i] = convertByteToFloatData(byte_data, now_idx, bit_per_sample);

                now_idx += step_byte;
            }

            return ranged_rawdata;
        }

        private static float convertByteToFloatData(byte[] byte_data, int idx, int bit_per_sample)
        {
            float float_data = 0.0f;

            try
            {
                if (bit_per_sample == BIT_8)
                {
                    float_data = ((int)byte_data[idx] - 0x80) * RANGE_VALUE_BIT_8;
                }
                else if (bit_per_sample == BIT_16)
                {
                    short sample_data = System.BitConverter.ToInt16(byte_data, idx);
                    float_data = sample_data * RANGE_VALUE_BIT_16;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                throw ex;
            }

            return float_data;
        }
    }
}

