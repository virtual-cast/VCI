using System.Collections.Generic;
using NUnit.Framework;
using UniGLTF;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public sealed class UnityFormatTests
    {
        private VCIObject CreatePrimitiveVciObject()
        {
            var obj = new GameObject("Root");
            var vci = obj.AddComponent<VCIObject>();

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(obj.transform);
            cube.AddComponent<VCISubItem>();

            vci.Meta = new VCIMeta
            {
                title = "Foo",
                author = "Bar",
                version = "1",
            };

            return vci;
        }

        private VciData ExportAndParseVciObject(VCIObject obj)
        {
            // Export
            var exportingGltfData = new ExportingGltfData();
            using (var exporter = new VCIExporter(exportingGltfData))
            {
                exporter.Prepare(obj.gameObject);
                exporter.Export(new RuntimeTextureSerializer());
            }
            var bytes = exportingGltfData.ToGlbBytes();

            // Parse
            return new VciBinaryParser(bytes).Parse();
        }

        [Test]
        public void EmbeddedScript()
        {
            var vci = CreatePrimitiveVciObject();
            vci.Scripts = new List<VciScript>
            {
                new VciScript
                {
                    name = "main",
                    source = "print('hello')",
                    mimeType = VciScriptMimeType.Lua,
                    targetEngine = VciScriptTargetEngine.MoonSharp,
                },
            };

            var vciData = ExportAndParseVciObject(vci);

            Assert.AreEqual(1, vciData.Script.scripts.Count);
            Assert.AreEqual("main", vciData.Script.scripts[0].name);
            Assert.AreEqual("application/x-lua", vciData.Script.scripts[0].mimeType);
            Assert.AreEqual("moonSharp", vciData.Script.scripts[0].targetEngine);

            UnityEngine.Object.DestroyImmediate(vci.gameObject);
        }
    }
}
