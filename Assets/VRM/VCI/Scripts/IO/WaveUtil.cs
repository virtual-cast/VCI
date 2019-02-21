using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VCI
{
    public static class WaveUtil
    {
        public struct WaveHeaderData
        {
            public string RiffHeader; // "riff"
            public Int32 FileSize;  // ファイルサイズ-8
            public string WaveHeader;  // "WAVE"
            public string FormatChunk;  // "fmt "
            public Int32 FormatChunkSize; // fmtチャンクのバイト数
            public Int16 FormatID; // フォーマット
            public Int16 Channel; // チャンネル数
            public Int32 SampleRate; // サンプリングレート
            public Int32 BytePerSec; // データ速度
            public Int16 BlockSize; // ブロックサイズ
            public Int16 BitPerSample;  // 量子化ビット数
            public string DataChunk; // "data"
            public Int32 DataChunkSize; // 波形データのバイト数
            public Int32 PlayTimeMsec;
        }

        public static bool TryReadHeader(byte[] bytes, out WaveHeaderData waveHeader)
        {
            waveHeader = new WaveHeaderData();

            using (Stream ms = new MemoryStream(bytes, false))
            {
                try
                {
                    BinaryReader br = new BinaryReader(ms);
                    waveHeader.RiffHeader = System.Text.Encoding.GetEncoding(20127).GetString(br.ReadBytes(4));
                    waveHeader.FileSize = BitConverter.ToInt32(br.ReadBytes(4), 0);
                    waveHeader.WaveHeader = System.Text.Encoding.GetEncoding(20127).GetString(br.ReadBytes(4));

                    int bytesPerSec = 0;
                    bool readFmtChunk = false;
                    bool readDataChunk = false;
                    while (!readFmtChunk || !readDataChunk)
                    {
                        string chunk = System.Text.Encoding.GetEncoding(20127).GetString(br.ReadBytes(4));
                        if (chunk.ToLower().CompareTo("fmt ") == 0)
                        {
                            waveHeader.FormatChunk = chunk;
                            waveHeader.FormatChunkSize = BitConverter.ToInt32(br.ReadBytes(4), 0);
                            waveHeader.FormatID = BitConverter.ToInt16(br.ReadBytes(2), 0);
                            waveHeader.Channel = BitConverter.ToInt16(br.ReadBytes(2), 0);
                            waveHeader.SampleRate = BitConverter.ToInt32(br.ReadBytes(4), 0);
                            waveHeader.BytePerSec = BitConverter.ToInt32(br.ReadBytes(4), 0);
                            waveHeader.BlockSize = BitConverter.ToInt16(br.ReadBytes(2), 0);
                            waveHeader.BitPerSample = BitConverter.ToInt16(br.ReadBytes(2), 0);
                            bytesPerSec = waveHeader.SampleRate * waveHeader.BlockSize;
                            readFmtChunk = true;
                        }
                        else if (chunk.ToLower().CompareTo("data") == 0)
                        {
                            waveHeader.DataChunk = chunk;
                            waveHeader.DataChunkSize = BitConverter.ToInt32(br.ReadBytes(4), 0);
                            waveHeader.PlayTimeMsec = (int)(((double)waveHeader.DataChunkSize / (double)bytesPerSec) * 1000);
                            readDataChunk = true;
                        }
                        else
                        {
                            Int32 size = BitConverter.ToInt32(br.ReadBytes(4), 0);
                            if (0 < size)
                            {
                                br.ReadBytes(size);
                            }
                        }
                    }
                }
                catch
                {
                    ms.Close();
                    return false;
                }
            }

            return true;
        }

        private static void WriteHeader(AudioClip clip, MemoryStream stream)
        {
            var hz = clip.frequency;
            var channels = clip.channels;
            var samples = clip.samples;

            stream.Seek(0, SeekOrigin.Begin);

            Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
            stream.Write(riff, 0, 4);

            Byte[] chunkSize = BitConverter.GetBytes(stream.Length - 8);
            stream.Write(chunkSize, 0, 4);

            Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
            stream.Write(wave, 0, 4);

            Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
            stream.Write(fmt, 0, 4);

            Byte[] subChunk1 = BitConverter.GetBytes(16);
            stream.Write(subChunk1, 0, 4);

            UInt16 one = 1;

            Byte[] audioFormat = BitConverter.GetBytes(one);
            stream.Write(audioFormat, 0, 2);

            Byte[] numChannels = BitConverter.GetBytes(channels);
            stream.Write(numChannels, 0, 2);

            Byte[] sampleRate = BitConverter.GetBytes(hz);
            stream.Write(sampleRate, 0, 4);

            Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2
            stream.Write(byteRate, 0, 4);

            UInt16 blockAlign = (ushort)(channels * 2);
            stream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

            UInt16 bps = 16;
            Byte[] bitsPerSample = BitConverter.GetBytes(bps);
            stream.Write(bitsPerSample, 0, 2);

            Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
            stream.Write(datastring, 0, 4);

            Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
            stream.Write(subChunk2, 0, 4);

        }

        public static byte[] GetWaveBinary(AudioClip clip)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Seek(44, SeekOrigin.Begin);

                var samples = new float[clip.samples * clip.channels];
                clip.GetData(samples, 0);

                Int16[] intData = new Int16[samples.Length];

                byte[] bytesData = new byte[samples.Length * 2];

                int rescaleFactor = 32767;
                for (int i = 0; i < samples.Length; i++)
                {
                    intData[i] = (short)(samples[i] * rescaleFactor);
                    byte[] byteArr = new byte[2];
                    byteArr = BitConverter.GetBytes(intData[i]);
                    byteArr.CopyTo(bytesData, i * 2);
                }


                Buffer.BlockCopy(intData, 0, bytesData, 0, bytesData.Length);
                stream.Write(bytesData, 0, bytesData.Length);

                WriteHeader(clip, stream);

                return stream.GetBuffer();
            }
        }
 
    }
}
