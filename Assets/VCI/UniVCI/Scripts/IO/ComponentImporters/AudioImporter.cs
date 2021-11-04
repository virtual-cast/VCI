using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public static class AudioImporter
    {
        public static async Task LoadAsync(
            VciData data,
            IReadOnlyList<Transform> unityNodes,
            GameObject unityRoot,
            AudioClipFactory audioClipFactory,
            IAwaitCaller awaitCaller)
        {
            // Load
            var audios = Deserialize(data);
            var clips = new Dictionary<SubAssetKey, AudioClip>();
            foreach (var (name, mimeType, binary) in audios)
            {
                var clip = await audioClipFactory.LoadAudioClipAsync(name, mimeType, binary, awaitCaller);
                clips.Add(new SubAssetKey(typeof(AudioClip), name), clip);
            }

            // Setup
            SetupComponents(data, unityNodes, unityRoot, data.VciMigrationFlags, clips);
        }

        public static List<(string name, string mimeType, ArraySegment<byte> binary)> Deserialize(VciData data)
        {
            var audios = new List<(string name, string mimeType, ArraySegment<byte> binary)>();

            if (data.Audios == null)
            {
                return audios;
            }

            foreach (var audio in data.Audios.audios)
            {
                audios.Add((audio.name, audio.mimeType, data.GltfData.GetBytesFromBufferView(audio.bufferView)));
            }

            return audios;
        }

        public static void SetupComponents(
            VciData data,
            IReadOnlyList<Transform> unityNodes,
            GameObject unityRoot,
            VciMigrationFlags vciMigrationFlags,
            IReadOnlyDictionary<SubAssetKey, AudioClip> clips)
        {
            // * ver 0.32でAudioSource拡張が追加
            // - それ以前のバージョンで出力されている場合、
            // - すべてのAudioClip, AudioSourceはRootにアタッチされる
            if (vciMigrationFlags.IsAudioClipAttachPointUndefined)
            {
                foreach (var (key, clip) in clips)
                {
                    var audioSource = unityRoot.AddComponent<AudioSource>();
                    audioSource.clip = clip;
                    audioSource.playOnAwake = false;
                    audioSource.loop = false;
                    audioSource.spatialBlend = 0;
                    audioSource.dopplerLevel = 0;
                }
            }
            else
            {
                foreach (var (nodeIdx, audioSourceExt) in data.AudioSourcesNodes)
                {
                    foreach (var source in audioSourceExt.audioSources)
                    {
                        var audioIdx = source.audio;
                        var audio = data.Audios.audios[audioIdx];
                        var key = new SubAssetKey(typeof(AudioClip), audio.name);
                        var audioClip = clips[key];

                        if (audioClip != null)
                        {
                            var audioSource = unityNodes[nodeIdx].gameObject.AddComponent<AudioSource>();
                            audioSource.clip = audioClip;
                            audioSource.playOnAwake = false;
                            audioSource.loop = false;
                            audioSource.spatialBlend = source.spatialBlend;
                            audioSource.dopplerLevel = 0;
                        }
                        else
                        {
                            Debug.LogWarning($"Audio file at index {source.audio} was not found.");
                        }
                    }
                }
            }
        }
    }
}