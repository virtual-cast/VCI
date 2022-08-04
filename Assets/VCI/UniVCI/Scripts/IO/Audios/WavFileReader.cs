using System;
using System.IO;
using UnityEngine;

namespace VCI
{
    public static class WavFileReader
    {
        private static readonly float RangeValueBIT8 = 1.0f / Mathf.Pow(2, 7); // 1 / 128
        private static readonly float RangeValueBIT16 = 1.0f / Mathf.Pow(2, 15); // 1 / 32768
        private const int BIT_8 = 8;
        private const int BIT_16 = 16;
        private const int BIT_24 = 24;
        // NOTE: WAVファイルフォーマットの各チャンク/フィールドのバイト数
        // 参考 -> https://www.youfit.co.jp/archives/1418
        private const int RIFF_CHUNK_BYTES = 12;
        private const int FMT_CHUNK_ID_FIELD_BYTES = 4;
        private const int FMT_CHUNK_SIZE_FIELD_BYTES = 4;
        private const int DATA_CHUNK_ID_FIELD_BYTES = 4;
        private const int DATA_CHUNK_SIZE_FIELD_BYTES = 4;

        public static WavReadResult ReadSamplesFromRawBytes(byte[] wavBytes)
        {
            using var memoryStream = new MemoryStream(wavBytes);
            if (!WaveUtil.TryReadHeader(memoryStream, out var waveHeader))
            {
                throw new WavFileReadException("Cannot read wav header.");
            }

            var samplesPerChannel = waveHeader.DataChunkSize / waveHeader.BlockSize;
            var channels = waveHeader.Channel;
            var bitPerSample = waveHeader.BitPerSample;
            // NOTE: WAVファイルの音声データ部の先頭のindex
            var dataBufferIndex =
                RIFF_CHUNK_BYTES +
                FMT_CHUNK_ID_FIELD_BYTES + FMT_CHUNK_SIZE_FIELD_BYTES + waveHeader.FormatChunkSize +
                DATA_CHUNK_ID_FIELD_BYTES + DATA_CHUNK_SIZE_FIELD_BYTES;

            var allSampleCount = samplesPerChannel * channels;

            var outputSamples = new float[allSampleCount];
            var stepByte = bitPerSample / BIT_8;

            for (var i = 0; i < allSampleCount; ++i)
            {
                outputSamples[i] = ReadWavByteAsFloatSample(wavBytes, dataBufferIndex + i * stepByte, bitPerSample);
            }

            return new WavReadResult(outputSamples, channels, waveHeader.SampleRate);
        }

        // バイト配列のうち、indexからbitPerSample分のデータを-1 ~ 1のfloatサンプルとして読みだして返す
        // NOTE: VC wikiのVCIで使える音声フォーマットの中で挙げられているのが8bit, 16bit, 24bit
        // 32 bitのwavも一応存在しうるが、非対応なのでここでは考慮しない
        private static float ReadWavByteAsFloatSample(byte[] wavBytes, int index, int bitPerSample)
        {
            var floatData = 0.0f;
            try
            {
                if (bitPerSample == BIT_8)
                {
                    floatData = ((int) wavBytes[index] - 0x80) * RangeValueBIT8;
                }
                else if (bitPerSample == BIT_16)
                {
                    floatData = BitConverter.ToInt16(wavBytes, index) * RangeValueBIT16;
                }
                else if (bitPerSample == BIT_24)
                {
                    // skip low bit
                    floatData = BitConverter.ToInt16(wavBytes, index + 1) * RangeValueBIT16;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex);
                throw;
            }
            return floatData;
        }

        public readonly struct WavReadResult
        {
            public float[] Samples { get; }
            public int Channels { get; }
            public int SamplingFrequency { get; }

            public int SamplesPerChannel => Samples.Length / Channels;

            public WavReadResult(float[] samples, int channels, int samplingFrequency)
            {
                Samples = samples;
                Channels = channels;
                SamplingFrequency = samplingFrequency;
            }
        }
    }
}
