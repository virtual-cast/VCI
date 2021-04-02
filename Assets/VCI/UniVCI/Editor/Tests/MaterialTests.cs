using NUnit.Framework;
using UnityEngine;
using UniGLTF;
using UniJSON;

namespace VCI
{
    public class MaterialTests
    {
        [Test]
        public void VciMaterialUnity()
        {
            var gltf = new glTF();
            var src = new glTF_VCAST_vci_material_unity
            {
                materials = new System.Collections.Generic.List<glTF_VCI_Material>
                {
                    new glTF_VCI_Material
                    {
                    }
                }
            };

            var f = new UniJSON.JsonFormatter();
            glTF_VCAST_vci_material_unity_Serializer.Serialize(f, src);
            glTFExtensionExport.GetOrCreate(ref gltf.extensions).Add(glTF_VCAST_vci_material_unity.ExtensionName, f.GetStore().Bytes);

            var gltf2 = new glTF();
            gltf2.extensions = (gltf.extensions as glTFExtensionExport).Deserialize();
            if (gltf2.extensions.TryDeserializeExtensions(glTF_VCAST_vci_material_unity.ExtensionName, glTF_VCAST_vci_material_unity_Deserializer.Deserialize, out glTF_VCAST_vci_material_unity dst))
            {
                Assert.AreEqual(1, dst.materials.Count);
            }
            else
            {
                Assert.Fail();
            }
        }

        private static Material ExportThenImport(Material src)
        {
            //
            // export
            //
            GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
            GameObject root = new GameObject("root");
            cube.transform.SetParent(root.transform);
            var vciObject = root.AddComponent<VCIObject>();
            vciObject.Meta.author = "AUTHOR";
            vciObject.Meta.title = "TITLE";
            var renderer = cube.GetComponent<Renderer>();
            renderer.sharedMaterial = src;
            var gltf = new glTF();
            var exporter = new VCIExporter(gltf);
            exporter.Prepare(root);
            exporter.Export(default, VRMShaders.AssetTextureUtil.UseAsset);
            var bytes = gltf.ToGlbBytes();

            //
            // importer
            //
            var importer = new VCIImporter();
            importer.ParseGlb(bytes);
            importer.Load();
            var dst = importer.GetMaterial(0);

            return dst;
        }

        [Test]
        public void Material_Standard_ExportImportTest()
        {
            // Standard
            {
                var src = new Material(Shader.Find("Standard"));
                //src.SetColor("_EmissionColor", new Color(0, 1, 2, 1));
                var dst = ExportThenImport(src);
                Assert.AreEqual(src.shader.name, dst.shader.name);
            }
        }

        [Test]
        public void Material_Unlit_ExportImportTest()
        {
            // Unlit
            {
                var src = new Material(Shader.Find("Unlit/Texture"));
                var dst = ExportThenImport(src);
                Assert.AreEqual("UniGLTF/UniUnlit", dst.shader.name);
            }
        }

        [Test]
        public void Material_MToon_ExportImportTest()
        {
            // MToon
            {
                var src = new Material(Shader.Find("VRM/MToon"));
                var dst = ExportThenImport(src);
                Assert.AreEqual("VRM/MToon", dst.shader.name);
            }
        }
    }
}