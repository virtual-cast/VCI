using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using VRMShaders;
using Debug = UnityEngine.Debug;

namespace VCI
{
    public sealed class Mp3DecodeStrategyAdapter : IMp3FileDecoder
    {
        [NotNull]
        private readonly IMp3FileDecoder _defaultDecoder;
        [CanBeNull]
        private readonly IMp3FileDecoder _customDecoder;

        public Mp3DecodeStrategyAdapter(IMp3FileDecoder defaultDecoder, IMp3FileDecoder customDecoder = null)
        {
            _defaultDecoder = defaultDecoder ?? throw new ArgumentException("Default decode method cannot be null.");
            _customDecoder = customDecoder;
        }

        public async Task<Mp3DecodeResult> DecodeFromRawBytesAsync(byte[] mp3Bytes, IAwaitCaller awaitCaller, CancellationToken ct = default)
        {
            // ユーザー指定のデコーダーが渡されている場合、それを使ってデコードを試みる
            if (_customDecoder != null)
            {
                try
                {
                    return await _customDecoder.DecodeFromRawBytesAsync(mp3Bytes, awaitCaller, ct);
                }
                catch (Exception e) when (!(e is OperationCanceledException))
                {
                    // デコードに失敗した旨をログに出力し、処理を中断せずにデフォルトのデコーダーでのデコードを試みる
                    Debug.LogWarning($"Failed to decode mp3 with custom decoder: cause -> {e.Message}");
                }
            }

            // デフォルトのデコーダーを用いてデコードを試みる
            try
            {
                return await _defaultDecoder.DecodeFromRawBytesAsync(mp3Bytes, awaitCaller, ct);
            }
            catch (Exception e) when (!(e is OperationCanceledException))
            {
                // デコードに失敗した旨をログに出力する
                Debug.LogWarning($"Failed to decode mp3 with default decoder: cause -> {e.Message}");
            }

            // すべてのデコードに失敗、exceptionを投げて終了する
            throw new VciMp3FileDecodeException("Failed to decode mp3.");
        }
    }
}
