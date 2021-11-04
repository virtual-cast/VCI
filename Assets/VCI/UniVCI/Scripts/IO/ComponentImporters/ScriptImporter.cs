using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniGLTF;
using UniJSON;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public static class ScriptImporter
    {
        public static async Task LoadAsync(VciData data, VCIObject unityVciObject, IAwaitCaller awaitCaller)
        {
            if (!Application.isPlaying)
            {
                // Editor Import の場合は EditorScriptImporter が走る。
                return;
            }

            unityVciObject.Scripts = await awaitCaller.Run(() => Deserialize(data));
        }

        public static List<VciScript> Deserialize(VciData data)
        {
            var scripts = new List<VciScript>();

            if (data.Script == null)
            {
                return scripts;
            }

            foreach (var x in data.Script.scripts)
            {
                var source = "";
                try
                {
                    var bytes = data.GltfData.GetBytesFromBufferView(x.source);
                    source = Utf8String.Encoding.GetString(bytes.Array, bytes.Offset, bytes.Count);
                }
                catch (Exception)
                {
                    // 握りつぶし
                }

                scripts.Add(new VciScript
                {
                    name = x.name,
                    mimeType = DeserializeScriptMimeType(x.mimeType),
                    targetEngine = DeserializerScriptTargetEngine(x.targetEngine),
                    source = source
                });
            }

            return scripts;
        }

        private static VciScriptMimeType DeserializeScriptMimeType(string jsonString)
        {
            switch (jsonString)
            {
                case EmbeddedScriptJsonObject.LuaMimeTypeString:
                    return VciScriptMimeType.Lua;
                case "x_application_lua": // 過去の VCI バージョン v0.33 未満で存在していたシリアライズ文字列.
                    return VciScriptMimeType.Lua;
                default: // 未定義なら Lua にフォールバックする.
                    return VciScriptMimeType.Lua;
            }
        }

        private static VciScriptTargetEngine DeserializerScriptTargetEngine(string jsonString)
        {
            switch (jsonString)
            {
                case EmbeddedScriptJsonObject.MoonSharpTargetEngineString:
                    return VciScriptTargetEngine.MoonSharp;
                case "moonsharp": // 過去の VCI バージョン v0.33 未満で存在していたシリアライズ文字列.
                    return VciScriptTargetEngine.MoonSharp;
                default: // 未定義なら MoonSharp にフォールバックする.
                    return VciScriptTargetEngine.MoonSharp;
            }
        }
    }
}