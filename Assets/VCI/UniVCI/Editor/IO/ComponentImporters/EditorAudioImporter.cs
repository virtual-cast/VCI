using System;
using System.Collections.Generic;
using System.IO;
using UniGLTF;

namespace VCI
{
    /// <summary>
    /// AudioClip は Runtime にロードした AudioClip アセットを保存する実装では、mp3 ファイルの情報が失われてしまう。
    /// VCI ファイルに書き込まれたバイト列をそのままアセットとして展開しなければならない。
    /// </summary>
    public static class EditorAudioImporter
    {
        public static List<UnityPath> ExtractAssetFiles(VciData data, UnityPath prefabFilePath)
        {
            var extractedClips = new List<UnityPath>();

            var audios = AudioImporter.Deserialize(data);
            if (audios.Count == 0)
            {
                return extractedClips;
            }

            var audioDir = prefabFilePath.GetAssetFolder(".Audios");
            audioDir.EnsureFolder();

            foreach (var (name, mimeType, binary) in audios)
            {
                var audioBuffer = new byte[binary.Count];
                Buffer.BlockCopy(binary.Array, binary.Offset, audioBuffer, 0, audioBuffer.Length);

                var ext = DeserializeExtensionFromMimeTypeString(mimeType);
                if (!string.IsNullOrEmpty(ext))
                {
                    var fileName = $"{name}.{ext}";
                    var audioFilePath = audioDir.Child(fileName);
                    File.WriteAllBytes(audioFilePath.FullPath, audioBuffer);
                    audioFilePath.ImportAsset();

                    extractedClips.Add(audioFilePath);
                }
            }

            return extractedClips;
        }

        private static string DeserializeExtensionFromMimeTypeString(string mimeType)
        {
            switch (mimeType)
            {
                case AudioJsonObject.WavMimeType:
                    return "wav";
                case AudioJsonObject.Mp3MimeType:
                    return "mp3";
            }

            // NOTE: 不明な mimeType の場合、mimetype から拡張子を取り出すことを試みる.
            var r = new System.Text.RegularExpressions.Regex(
                @"(?<type>.*)/(?<ext>.*)",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase
                | System.Text.RegularExpressions.RegexOptions.Singleline);
            var match = r.Match(mimeType);
            if (match.Success)
            {
                return match.Groups["ext"].Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
