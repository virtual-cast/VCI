using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using NLayer.NAudioSupport;
using VRMShaders;

namespace VCI
{
    public sealed class NAudioMp3DecoderImpl : IMp3FileDecoder
    {
        // NOTE: RuntimeAudioClipDeserializer/RuntimeAudioClipAssetCreatorで行っていた処理をここにそのまま移植
        // 歴史的経緯によってmp3ファイルを一旦wavファイルに変換して、そこから音声サンプルを得ている
        // TODO: mp3ファイルを直接デコードして音声サンプルを得る
        public async Task<Mp3DecodeResult> DecodeFromRawBytesAsync(byte[] mp3Bytes, IAwaitCaller awaitCaller, CancellationToken ct = default)
        {
            try
            {
                // NOTE: 本来はここにCancellationTokenを差すべき
                var wavBytes = await awaitCaller.Run(() => ConvertMp3ToWav(mp3Bytes));

                // NOTE: ditto
                var wavReadResult = await awaitCaller.Run(() =>
                    WavFileReader.ReadSamplesFromRawBytes(wavBytes));

                return new Mp3DecodeResult(wavReadResult.Samples, wavReadResult.SamplingFrequency, wavReadResult.Channels);
            }
            catch (Exception e) when (!(e is OperationCanceledException))
            {
                throw new VciMp3FileDecodeException(e.Message);
            }
        }

        // mp3ファイルバイナリをwavファイルバイナリに変換する
        private static byte[] ConvertMp3ToWav(byte[] mp3Bytes)
        {
            using var ms = new MemoryStream(mp3Bytes);
            using var reader = new Mp3FileReader(ms, wf => new Mp3FrameDecompressor(wf));
            using var outStream = new MemoryStream();

            WaveFileWriter.WriteWavFileToStream(outStream, new WaveFloatTo16Provider(reader));
            return outStream.ToArray();
        }
    }
}
