using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public sealed class AudioClipFactory : IResponsibilityForDestroyObjects
    {
        private readonly IReadOnlyDictionary<SubAssetKey, AudioClip> _externalClips;
        private readonly Dictionary<SubAssetKey, AudioClip> _runtimeGeneratedClips = new Dictionary<SubAssetKey, AudioClip>();
        private readonly List<SubAssetKey> _loadedClipKeys = new List<SubAssetKey>();
        private readonly RuntimeAudioFileToClipConverter _audioFileToClipConverter;

        public IReadOnlyList<SubAssetKey> LoadedClipKeys => _loadedClipKeys;

        public AudioClipFactory(IReadOnlyDictionary<SubAssetKey, AudioClip> externalClips, IMp3FileDecoder mp3FileDecoder)
        {
            _externalClips = externalClips;
            _audioFileToClipConverter = new RuntimeAudioFileToClipConverter(mp3FileDecoder);
        }

        public void Dispose()
        {
            _runtimeGeneratedClips.Clear();
        }

        public void TransferOwnership(TakeResponsibilityForDestroyObjectFunc take)
        {
            foreach (var (k, v) in _runtimeGeneratedClips.ToArray())
            {
                take(k, v);
                _runtimeGeneratedClips.Remove(k);
            }
        }

        public AudioClip GetLoadedAudioClip(SubAssetKey key)
        {
            if (_externalClips.TryGetValue(key, out var clip)) return clip;
            if (_runtimeGeneratedClips.TryGetValue(key, out clip)) return clip;
            return default;
        }

        public async Task<AudioClip> LoadAudioClipAsync(string name, string mimeType, NativeSlice<byte> binary, IAwaitCaller awaitCaller, CancellationToken ct = default)
        {
            var key = new SubAssetKey(typeof(AudioClip), name);

            var clip = GetLoadedAudioClip(key);
            if (clip == null)
            {
                clip = await _audioFileToClipConverter.ConvertAsync(name, mimeType, binary, awaitCaller, ct);

                _runtimeGeneratedClips.Add(key, clip);
            }

            if (!_loadedClipKeys.Contains(key))
            {
                _loadedClipKeys.Add(key);
            }

            return clip;
        }
    }
}
