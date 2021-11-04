using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public sealed class AudioClipFactory : IResponsibilityForDestroyObjects
    {
        private readonly IReadOnlyDictionary<SubAssetKey, AudioClip> _externalClips;
        private readonly Dictionary<SubAssetKey, AudioClip> _runtimeGeneratedClips = new Dictionary<SubAssetKey, AudioClip>();
        private readonly List<SubAssetKey> _loadedClipKeys = new List<SubAssetKey>();

        public IReadOnlyList<SubAssetKey> LoadedClipKeys => _loadedClipKeys;

        public AudioClipFactory(IReadOnlyDictionary<SubAssetKey, AudioClip> externalClips)
        {
            _externalClips = externalClips;
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

        public async Task<AudioClip> LoadAudioClipAsync(string name, string mimeType, ArraySegment<byte> binary, IAwaitCaller awaitCaller)
        {
            var key = new SubAssetKey(typeof(AudioClip), name);

            var clip = GetLoadedAudioClip(key);
            if (clip == null)
            {
                clip = await RuntimeAudioClipDeserializer.ImportAsync(name, mimeType, binary, awaitCaller);

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
