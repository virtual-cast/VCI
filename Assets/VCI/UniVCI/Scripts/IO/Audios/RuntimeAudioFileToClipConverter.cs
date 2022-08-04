using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public sealed class RuntimeAudioFileToClipConverter
    {
        private readonly IMp3FileDecoder _mp3FileDecoder;

        public RuntimeAudioFileToClipConverter(IMp3FileDecoder mp3FileDecoder)
        {
            _mp3FileDecoder = mp3FileDecoder;
        }

        public async Task<AudioClip> ConvertAsync(string name, string mimeType, NativeSlice<byte> rawFile, IAwaitCaller awaitCaller, CancellationToken ct = default)
        {
            // NOTE: Copy
            var data = rawFile.ToArray();

            try
            {
                if (mimeType == AudioJsonObject.Mp3MimeType)
                {
                    var mp3DecodeResult = await _mp3FileDecoder.DecodeFromRawBytesAsync(data, awaitCaller, ct);

                    // NOTE: Createで求められるsampleLengthは、「1チャンネル分のサンプル数」
                    var clip = AudioClip.Create(name, mp3DecodeResult.SamplesPerChannel, mp3DecodeResult.Channels, mp3DecodeResult.SamplingFrequency, false);
                    clip.SetData(mp3DecodeResult.Samples, 0);

                    return clip;
                }
                else
                {
                    var wavReadResult = await awaitCaller.Run(() => WavFileReader.ReadSamplesFromRawBytes(data));

                    // NOTE: Createで求められるsampleLengthは、「1チャンネル分のサンプル数」
                    var clip = AudioClip.Create(name, wavReadResult.SamplesPerChannel, wavReadResult.Channels, wavReadResult.SamplingFrequency, false);
                    clip.SetData(wavReadResult.Samples, 0);

                    return clip;
                }
            }
            // NOTE: キャンセル時のOperationCanceledExceptionのみ貫通させる
            catch (Exception e) when (!(e is OperationCanceledException))
            {
                // NOTE: この関数を呼び出す側が、AudioClipへの変換処理に失敗して処理が中断されることを想定していない
                // そのため、ここで失敗をcatchしてログに原因を出力する
                Debug.LogException(e);
                return default;
            }
        }
    }
}
