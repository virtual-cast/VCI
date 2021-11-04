using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    public static class AudioExporter
    {
        /// <summary>
        /// AudioSource に関する "Root 拡張" glTF_VCAST_vci_audios を Export する。
        ///
        /// glTF_VCAST_vci_audios は VCI 内に付与されたすべての AudioSource の音声ファイルの buffer index を持つ.
        ///
        /// NOTE: Root に存在する AudioSource を出力するという意味ではない。
        /// </summary>
        public static glTF_VCAST_vci_audios ExportAudioSourcesOnRoot(ExportingGltfData exportingData, GameObject Copy)
        {
            // Audio
            // FIXME: Root に存在する AudioSource は除外しなければならない.
            var clips = Copy.GetComponentsInChildren<AudioSource>()
                .Select(x => x.clip)
                .Where(x => x != null)
                .ToArray();
            if (clips.Any())
            {
                var audios = new List<AudioJsonObject>();
                foreach (var clip in clips)
                {
                    if (audios.Exists(x => x.name == clip.name))
                    {
                        continue;
                    }

                    var audio = ExportAudioExtension(exportingData, clip);
                    if (audio != null)
                    {
                        audios.Add(audio);
                    }
                }

                return new glTF_VCAST_vci_audios
                {
                    audios = audios
                };
            }
            return null;
        }

        public static glTF_VCAST_vci_audio_sources ExportAudioSourcesOnNode(Transform node, glTF_VCAST_vci_audios audiosRootExtension)
        {
            var audioSources = node.GetComponents<AudioSource>()
                .Where(audioSource => audioSource.clip != null)
                .ToArray();

            if (audioSources.Length == 0)
            {
                return null;
            }

            var audioSourceExtensions = new List<AudioSourceJsonObject>();
            foreach (var audioSource in audioSources)
            {
                audioSourceExtensions.Add(ExportAudioSource(audioSource, audiosRootExtension));
            }

            return new glTF_VCAST_vci_audio_sources
            {
                audioSources = audioSourceExtensions,
            };
        }

        private static AudioJsonObject ExportAudioExtension(ExportingGltfData exportingData, AudioClip clip)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
            {
                var bytes = WaveUtil.GetWaveBinary(clip);
                var viewIndex = exportingData.ExtendBufferAndGetViewIndex(bytes);
                return new AudioJsonObject
                {
                    name = clip.name,
                    mimeType = AudioJsonObject.WavMimeType,
                    bufferView = viewIndex,
                };
            }
#if UNITY_EDITOR
            else
            {
                var path = UnityPath.FromAsset(clip);
                if (!path.IsUnderAssetsFolder) return null;
                if (path.Extension.ToLower() == ".wav")
                {
                    var bytes = File.ReadAllBytes(path.FullPath);
                    var viewIndex = exportingData.ExtendBufferAndGetViewIndex(bytes);
                    return new AudioJsonObject
                    {
                        name = clip.name,
                        mimeType = AudioJsonObject.WavMimeType,
                        bufferView = viewIndex,
                    };
                }
                else if (path.Extension.ToLower() == ".mp3")
                {
                    var bytes = File.ReadAllBytes(path.FullPath);
                    var viewIndex = exportingData.ExtendBufferAndGetViewIndex(bytes);
                    return new AudioJsonObject
                    {
                        name = clip.name,
                        mimeType = AudioJsonObject.Mp3MimeType,
                        bufferView = viewIndex,
                    };
                }
                else
                {
                    // Convert to wav
                    var bytes = WaveUtil.GetWaveBinary(clip);
                    var viewIndex = exportingData.ExtendBufferAndGetViewIndex(bytes);
                    return new AudioJsonObject
                    {
                        name = clip.name,
                        mimeType = AudioJsonObject.WavMimeType,
                        bufferView = viewIndex,
                    };
                }
            }
#endif
        }

        private static AudioSourceJsonObject ExportAudioSource(AudioSource audioSource, glTF_VCAST_vci_audios audios)
        {
            var result = new AudioSourceJsonObject
            {
                audio = audios.audios.FindIndex(x => x.name == audioSource.clip.name),
                spatialBlend = audioSource.spatialBlend
            };

            return result;
        }

    }
}
