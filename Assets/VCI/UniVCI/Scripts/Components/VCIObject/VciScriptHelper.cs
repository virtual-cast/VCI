using System.IO;
using UnityEngine;

namespace VCI
{
    public static class VciScriptHelper
    {
        public static string GetSourceTextForExecution(VciScript script)
        {
            // Build
            if (!Application.isEditor)
            {
                return script.source;
            }

            // Play Mode
            return GetSourceTextFromExternalFile(script);
        }

        public static string GetSourceTextForExport(VciScript script)
        {
            // Runtime Export (Build)
            if (Application.isPlaying && !Application.isEditor)
            {
                return script.source;
            }

            // Runtime Export (Play Mode)
            if (Application.isPlaying && Application.isEditor)
            {
                return GetSourceTextFromManagedAsset(script);
            }

            // Editor Export
            return GetSourceTextFromExternalFile(script);
        }

        public static bool HasInvalidFilePath(VciScript script)
        {
            return !script.textAsset && !string.IsNullOrEmpty(script.filePath) && !File.Exists(script.filePath);
        }

        private static string GetSourceTextFromManagedAsset(VciScript script)
        {
            if (script.textAsset)
            {
                return script.textAsset.text;
            }
            return script.source;
        }

        private static string GetSourceTextFromExternalFile(VciScript script)
        {
            if (script.textAsset)
            {
                return script.textAsset.text;
            }
            if (!string.IsNullOrEmpty(script.filePath))
            {
                if (!File.Exists(script.filePath)) throw new FileNotFoundException("File not found. " + script.filePath);
                using (var reader = new StreamReader(script.filePath)) return reader.ReadToEnd();
            }
            return script.source;
        }
    }
}
