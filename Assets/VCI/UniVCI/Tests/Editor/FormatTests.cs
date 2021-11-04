using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UniGLTF;
using UniJSON;

namespace VCI
{
    public class FormatTests
    {
        private T RootExtensionSerializeAndDeserialize<T>(T src, string extensionName, Action<JsonFormatter, T> serializer, Func<JsonNode, T> deserializer) where T: class
        {
            var gltf = new glTF();
            var f = new UniJSON.JsonFormatter();
            serializer(f, src);
            glTFExtensionExport.GetOrCreate(ref gltf.extensions).Add(extensionName, f.GetStore().Bytes);

            var gltf2 = new glTF();
            gltf2.extensions = (gltf.extensions as glTFExtensionExport).Deserialize();

            if (gltf2.extensions.TryDeserializeExtensions(extensionName, deserializer, out T dst))
            {
                return dst;
            }
            else
            {
                return null;
            }
        }

        private T NodeExtensionSerializeAndDeserialize<T>(T src, string extensionName, Action<JsonFormatter, T> serializer, Func<JsonNode, T> deserializer) where T : class
        {
            var node = new glTFNode();
            node.name = "testNode";
            var f = new UniJSON.JsonFormatter();
            serializer(f, src);
            glTFExtensionExport.GetOrCreate(ref node.extensions).Add(extensionName, f.GetStore().Bytes);

            var node2 = new glTFNode();
            node2.extensions = (node.extensions as glTFExtensionExport).Deserialize();

            if (node2.extensions.TryDeserializeExtensions(extensionName, deserializer, out T dst))
            {
                return dst;
            }
            else
            {
                return null;
            }
        }

        [Test]
        public void VciMeta()
        {
            var src = new glTF_VCAST_vci_meta
            {
                author = "AUTHOR",
            };

            var dst = RootExtensionSerializeAndDeserialize(
                src,
                glTF_VCAST_vci_meta.ExtensionName,
                glTF_VCAST_vci_meta_Serializer.Serialize,
                glTF_VCAST_vci_meta_Deserializer.Deserialize
                );

            if (dst != null)
            {
                Assert.AreEqual(src.author, dst.author);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void VciAudios()
        {
            var src = new glTF_VCAST_vci_audios
            {
                audios = new System.Collections.Generic.List<AudioJsonObject>
                {
                    new AudioJsonObject
                    {
                        name = "hoge.wav",
                    },
                }
            };

            var dst = RootExtensionSerializeAndDeserialize(
                src,
                glTF_VCAST_vci_audios.ExtensionName,
                glTF_VCAST_vci_audios_Serializer.Serialize,
                glTF_VCAST_vci_audios_Deserializer.Deserialize
                );

            if (dst != null)
            {
                Assert.AreEqual(src.audios.Count, dst.audios.Count);
                Assert.AreEqual(src.audios[0].name, dst.audios[0].name);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void VciEmbeddedScript()
        {
            var src = new glTF_VCAST_vci_embedded_script
            {
                scripts = new System.Collections.Generic.List<EmbeddedScriptJsonObject>
                {
                    new EmbeddedScriptJsonObject
                    {
                        name = "main.lua",
                    },
                }
            };

            var dst = RootExtensionSerializeAndDeserialize(
                src,
                glTF_VCAST_vci_embedded_script.ExtensionName,
                glTF_VCAST_vci_embedded_script_Serializer.Serialize,
                glTF_VCAST_vci_embedded_script_Deserializer.Deserialize
                );

            if (dst != null)
            {
                Assert.AreEqual(src.scripts.Count, dst.scripts.Count);
                Assert.AreEqual(src.scripts[0].name, dst.scripts[0].name);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void VciColliders()
        {
            var src = new glTF_VCAST_vci_colliders
            {
                colliders = new System.Collections.Generic.List<ColliderJsonObject>
                {
                    new ColliderJsonObject
                    {
                        center = new float[] {1, 2, 3},
                    }
                }
            };

            var dst = NodeExtensionSerializeAndDeserialize(
                src,
                glTF_VCAST_vci_colliders.ExtensionName,
                glTF_VCAST_vci_colliders_Serializer.Serialize,
                glTF_VCAST_vci_colliders_Deserializer.Deserialize
                );

            if (dst != null)
            {
                Assert.AreEqual(1, dst.colliders.Count());
                Assert.AreEqual(3, dst.colliders[0].center.Count());
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void VciRigidbodies()
        {
            var src = new glTF_VCAST_vci_rigidbody
            {
                rigidbodies = new System.Collections.Generic.List<RigidbodyJsonObject>
                {
                    new RigidbodyJsonObject
                    {
                        mass = 9.8f,
                    }
                }
            };

            var dst = NodeExtensionSerializeAndDeserialize(
                src,
                glTF_VCAST_vci_rigidbody.ExtensionName,
                glTF_VCAST_vci_rigidbody_Serializer.Serialize,
                glTF_VCAST_vci_rigidbody_Deserializer.Deserialize
                );

            if (dst != null)
            {
                Assert.AreEqual(1, dst.rigidbodies.Count());
                Assert.AreEqual(9.8f, dst.rigidbodies[0].mass);
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test]
        public void VciJoints()
        {
            var src = new glTF_VCAST_vci_joints
            {
                joints = new System.Collections.Generic.List<JointJsonObject>
                {
                    new JointJsonObject
                    {
                        spring = new SpringJsonObject
                        {
                            tolerance = 10.5f
                        }
                    }
                }
            };

            var dst = NodeExtensionSerializeAndDeserialize(
                src,
                glTF_VCAST_vci_joints.ExtensionName,
                glTF_VCAST_vci_joints_Serializer.Serialize,
                glTF_VCAST_vci_joints_Deserializer.Deserialize
                );

            if (dst != null)
            {
                Assert.AreEqual(1, dst.joints.Count());
                Assert.AreEqual(10.5f, dst.joints[0].spring.tolerance);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void VciAudioSources()
        {
            var src = new glTF_VCAST_vci_audio_sources
            {
                audioSources = new List<AudioSourceJsonObject>()
                {
                    new AudioSourceJsonObject()
                    {
                        audio = 1,
                        spatialBlend = 0.5f
                    }
                }
            };

            var dst = NodeExtensionSerializeAndDeserialize(
                src,
                glTF_VCAST_vci_audio_sources.ExtensionName,
                glTF_VCAST_vci_audio_sources_Serializer.Serialize,
                glTF_VCAST_vci_audio_sources_Deserializer.Deserialize
                );

            if (dst != null)
            {
                Assert.AreEqual(1, dst.audioSources.Count());
                Assert.AreEqual(1, dst.audioSources[0].audio);
                Assert.AreEqual(0.5f, dst.audioSources[0].spatialBlend);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public void VciItem()
        {
            var src = new glTF_VCAST_vci_item
            {
                grabbable = true,
            };

            var dst = NodeExtensionSerializeAndDeserialize(
                src,
                glTF_VCAST_vci_item.ExtensionName,
                glTF_VCAST_vci_item_Serializer.Serialize,
                glTF_VCAST_vci_item_Deserializer.Deserialize
                );

            if (dst != null)
            {
                Assert.AreEqual(true, dst.grabbable);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}