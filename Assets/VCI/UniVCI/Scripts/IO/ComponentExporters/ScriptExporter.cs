using System;
using System.Linq;
using UniGLTF;
using UniJSON;
using UnityEngine;

namespace VCI
{
    public static class ScriptExporter
    {
        public static glTF_VCAST_vci_embedded_script ExportScript(ExportingGltfData data, VCIObject vciObject)
        {
            if (vciObject.Scripts == null || vciObject.Scripts.Count == 0)
            {
                return null;
            }

            return new glTF_VCAST_vci_embedded_script
            {
                scripts = vciObject.Scripts.Select(x =>
                    {
                        var viewIndex = data.ExtendBufferAndGetViewIndex(
                            Utf8String.Encoding.GetBytes(ExportScriptText(x))
                        );
                        return new EmbeddedScriptJsonObject
                        {
                            name = x.name,
                            mimeType = ExportScriptMimeType(x.mimeType),
                            targetEngine = ExportScriptTargetEngine(x.targetEngine),
                            source = viewIndex,
                        };
                    })
                    .ToList()
            };
        }

        private static string ExportScriptText(VciScript script)
        {
            if (Application.isPlaying)
            {
                // Runtime
                return script.source;
            }
            else
            {
                // Editor
#if UNITY_EDITOR
                if (script.textAsset)
                {
                    return script.textAsset.text;
                }
                else
#endif
                {
                    return script.source;
                }
            }
        }

        private static string ExportScriptMimeType(VciScriptMimeType type)
        {
            switch (type)
            {
                case VciScriptMimeType.Lua:
                    return EmbeddedScriptJsonObject.LuaMimeTypeString;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static string ExportScriptTargetEngine(VciScriptTargetEngine type)
        {
            switch (type)
            {
                case VciScriptTargetEngine.MoonSharp:
                    return EmbeddedScriptJsonObject.MoonSharpTargetEngineString;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}