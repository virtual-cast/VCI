using NUnit.Framework;
using UnityEngine;
using VCIGLTF;
using VCIJSON;

namespace VCI
{
    public class MaterialTests
    {
        [Test]
        public void VciMaterialUnity()
        {
            var src = new glTF_VCAST_vci_material_unity
            {
                materials = new System.Collections.Generic.List<glTF_VCI_Material>
                {
                    new glTF_VCI_Material
                    {
                    }
                }
            };

            var f = new JsonFormatter();
            f.Serialize(src);
            var parsed = JsonParser.Parse(new Utf8String(f.GetStore().Bytes));

            Assert.AreEqual(1, parsed["materials"].GetArrayCount());
        }

        [Test]
        public void UnlitShaderImportTest()
        {
            var shaderStore = new ShaderStore(null);

            {
                // OPAQUE/Color
                var shader = shaderStore.GetShader(new glTFMaterial
                {
                    alphaMode = "OPAQUE",
                    pbrMetallicRoughness = new glTFPbrMetallicRoughness
                    {
                        baseColorFactor = new float[] {1, 0, 0, 1},
                    },
                    extensions = new glTFMaterial_extensions
                    {
                        KHR_materials_unlit = new glTF_KHR_materials_unlit { }
                    }
                });
                Assert.AreEqual("UniGLTF/UniUnlit", shader.name);
            }

            {
                // OPAQUE/Texture
                var shader = shaderStore.GetShader(new glTFMaterial
                {
                    alphaMode = "OPAQUE",
                    pbrMetallicRoughness = new glTFPbrMetallicRoughness
                    {
                        baseColorTexture = new glTFMaterialBaseColorTextureInfo(),
                    },
                    extensions = new glTFMaterial_extensions
                    {
                        KHR_materials_unlit = new glTF_KHR_materials_unlit { }
                    }
                });
                Assert.AreEqual("UniGLTF/UniUnlit", shader.name);
            }

            {
                // OPAQUE/Color/Texture
                var shader = shaderStore.GetShader(new glTFMaterial
                {
                    alphaMode = "OPAQUE",
                    pbrMetallicRoughness = new glTFPbrMetallicRoughness
                    {
                        baseColorFactor = new float[] {1, 0, 0, 1},
                        baseColorTexture = new glTFMaterialBaseColorTextureInfo(),
                    },
                    extensions = new glTFMaterial_extensions
                    {
                        KHR_materials_unlit = new glTF_KHR_materials_unlit { }
                    }
                });
                Assert.AreEqual("UniGLTF/UniUnlit", shader.name);
            }

            {
                // BLEND/Color
                var shader = shaderStore.GetShader(new glTFMaterial
                {
                    alphaMode = "BLEND",
                    pbrMetallicRoughness = new glTFPbrMetallicRoughness
                    {
                        baseColorFactor = new float[] {1, 0, 0, 1},
                    },
                    extensions = new glTFMaterial_extensions
                    {
                        KHR_materials_unlit = new glTF_KHR_materials_unlit { }
                    }
                });
                Assert.AreEqual("UniGLTF/UniUnlit", shader.name);
            }

            {
                // BLEND/Texture
                var shader = shaderStore.GetShader(new glTFMaterial
                {
                    alphaMode = "BLEND",
                    pbrMetallicRoughness = new glTFPbrMetallicRoughness
                    {
                        baseColorTexture = new glTFMaterialBaseColorTextureInfo(),
                    },
                    extensions = new glTFMaterial_extensions
                    {
                        KHR_materials_unlit = new glTF_KHR_materials_unlit { }
                    }
                });
                Assert.AreEqual("UniGLTF/UniUnlit", shader.name);
            }

            {
                // BLEND/Color/Texture
                var shader = shaderStore.GetShader(new glTFMaterial
                {
                    alphaMode = "BLEND",
                    pbrMetallicRoughness = new glTFPbrMetallicRoughness
                    {
                        baseColorFactor = new float[] {1, 0, 0, 1},
                        baseColorTexture = new glTFMaterialBaseColorTextureInfo(),
                    },
                    extensions = new glTFMaterial_extensions
                    {
                        KHR_materials_unlit = new glTF_KHR_materials_unlit { }
                    }
                });
                Assert.AreEqual("UniGLTF/UniUnlit", shader.name);
            }

            {
                // MASK/Texture
                var shader = shaderStore.GetShader(new glTFMaterial
                {
                    alphaMode = "MASK",
                    pbrMetallicRoughness = new glTFPbrMetallicRoughness
                    {
                        baseColorTexture = new glTFMaterialBaseColorTextureInfo(),
                    },
                    extensions = new glTFMaterial_extensions
                    {
                        KHR_materials_unlit = new glTF_KHR_materials_unlit { }
                    }
                });
                Assert.AreEqual("UniGLTF/UniUnlit", shader.name);
            }

            {
                // MASK/Color/Texture
                var shader = shaderStore.GetShader(new glTFMaterial
                {
                    alphaMode = "MASK",
                    pbrMetallicRoughness = new glTFPbrMetallicRoughness
                    {
                        baseColorFactor = new float[] {1, 0, 0, 1},
                        baseColorTexture = new glTFMaterialBaseColorTextureInfo(),
                    },
                    extensions = new glTFMaterial_extensions
                    {
                        KHR_materials_unlit = new glTF_KHR_materials_unlit { }
                    }
                });
                Assert.AreEqual("UniGLTF/UniUnlit", shader.name);
            }

            {
                // default
                var shader = shaderStore.GetShader(new glTFMaterial
                {
                    extensions = new glTFMaterial_extensions
                    {
                        KHR_materials_unlit = new glTF_KHR_materials_unlit { }
                    }
                });
                Assert.AreEqual("UniGLTF/UniUnlit", shader.name);
            }
        }

        private static Material ExportThenImport(Material src)
        {
            //
            // export
            //
            GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
            var renderer = cube.GetComponent<Renderer>();
            renderer.sharedMaterial = src;
            var gltf = new glTF();
            var exporter = new VCIExporter(gltf);
            exporter.Prepare(cube);
            exporter.Export();
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