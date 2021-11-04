using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UniGLTF;
using UniJSON;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// Editor では、スクリプトはテキストファイルアセットとして展開しなければならない。
    /// </summary>
    public sealed class EditorScriptImporter
    {
        private readonly VciData _data;
        private readonly VCIImporter _context;
        private readonly UnityPath _prefabFilePath;

        private UnityPath ScriptDir => _prefabFilePath.GetAssetFolder(".Scripts");

        public EditorScriptImporter(VciData data, VCIImporter context, UnityPath prefabFilePath)
        {
            _data = data;
            _context = context;
            _prefabFilePath = prefabFilePath;
        }

        /// <summary>
        /// VCI ファイルを読み込んで、スクリプトのアセットパスを得て、スクリプトを書き込む。
        /// ファイルを Write するだけ。
        /// </summary>
        public void ExtractAssetFiles()
        {
            var scripts = ScriptImporter.Deserialize(_data);
            foreach (var script in scripts)
            {
                ScriptDir.EnsureFolder();

                var scriptFilePath = GetScriptAssetPath(script);
                var text = script.source;
                File.WriteAllText(scriptFilePath.FullPath, text, Encoding.UTF8);
                scriptFilePath.ImportAsset();
            }
        }

        /// <summary>
        /// もう一度 VCI ファイルを読み込んで、スクリプトのアセットパスを得て、Unity.Object Asset として取得。
        /// それを VCI の GameObject にアサインする。
        /// </summary>
        public void SetupAfterEditorDelayCall()
        {
            var vciObject = _context.RuntimeVciInstance.Root.GetOrAddComponent<VCIObject>();
            vciObject.Scripts = new List<VciScript>();

            var scripts = ScriptImporter.Deserialize(_data);
            foreach (var script in scripts)
            {
                var scriptFilePath = GetScriptAssetPath(script);
                var scriptAsset = scriptFilePath.LoadAsset<TextAsset>();
                var source = Utf8String.Encoding.GetString(scriptAsset.bytes);

                vciObject.Scripts.Add(new VciScript
                {
                    name = script.name,
                    mimeType = script.mimeType,
                    targetEngine = script.targetEngine,
                    source = source,
                    textAsset = scriptAsset,
                });
            }
        }

        private UnityPath GetScriptAssetPath(VciScript script)
        {
            return ScriptDir.Child($"{script.name}.lua");
        }
    }
}