using System.Linq;
using NUnit.Framework;
using VCIGLTF;
using VCIJSON;

namespace VCI
{
    public class FormatTests
    {
        [Test]
        public void VciMeta()
        {
            var src = new glTF_VCAST_vci_meta
            {
                author = "AUTHOR",
                scriptFormat = glTF_VCAST_vci_meta.ScriptFormat.luaBinary,
            };

            var f = new JsonFormatter();
            f.Serialize(src);
            var parsed = JsonParser.Parse(new Utf8String(f.GetStore().Bytes));

            Assert.AreEqual("AUTHOR", parsed["author"].GetString());
            Assert.AreEqual("luaBinary", parsed["scriptFormat"].GetString());

            var dst = new glTF_VCAST_vci_meta();
            parsed.Deserialize(ref dst);
            Assert.AreEqual(src.scriptFormat, dst.scriptFormat);
        }

        [Test]
        public void VciAudios()
        {
            var src = new glTF_VCAST_vci_audios
            {
                audios = new System.Collections.Generic.List<glTF_VCAST_vci_audio>
                {
                    new glTF_VCAST_vci_audio
                    {
                        name = "hoge.wav",
                    },
                }
            };

            var f = new JsonFormatter();
            f.Serialize(src);
            var parsed = JsonParser.Parse(new Utf8String(f.GetStore().Bytes));

            Assert.AreEqual(1, parsed["audios"].GetArrayCount());
            Assert.AreEqual("hoge.wav", parsed["audios"][0]["name"].GetString());
        }

        [Test]
        public void VciEmbeddedScript()
        {
            var src = new glTF_VCAST_vci_embedded_script
            {
                scripts = new System.Collections.Generic.List<glTF_VCAST_vci_embedded_script_source>
                {
                    new glTF_VCAST_vci_embedded_script_source
                    {
                        name = "main.lua",
                    },
                }
            };

            var f = new JsonFormatter();
            f.Serialize(src);
            var parsed = JsonParser.Parse(new Utf8String(f.GetStore().Bytes));

            Assert.AreEqual(1, parsed["scripts"].ArrayItems().Count());
            Assert.AreEqual("main.lua", parsed["scripts"][0]["name"].GetString());
        }

        [Test]
        public void VciColliders()
        {
            var src = new glTF_VCAST_vci_colliders
            {
                colliders = new System.Collections.Generic.List<glTF_VCAST_vci_Collider>
                {
                    new glTF_VCAST_vci_Collider
                    {
                        center = new float[] {1, 2, 3},
                    }
                }
            };

            var f = new JsonFormatter();
            f.Serialize(src);
            var parsed = JsonParser.Parse(new Utf8String(f.GetStore().Bytes));

            Assert.AreEqual(1, parsed["colliders"].ArrayItems().Count());
            Assert.AreEqual(3, parsed["colliders"][0]["center"].GetArrayCount());
        }

        [Test]
        public void VciRigidbodies()
        {
            var src = new glTF_VCAST_vci_rigidbody
            {
                rigidbodies = new System.Collections.Generic.List<glTF_VCAST_vci_Rigidbody>
                {
                    new glTF_VCAST_vci_Rigidbody
                    {
                        mass = 9.8f,
                    }
                }
            };

            var f = new JsonFormatter();
            f.Serialize(src);
            var parsed = JsonParser.Parse(new Utf8String(f.GetStore().Bytes));

            Assert.AreEqual(1, parsed["rigidbodies"].ArrayItems().Count());
            Assert.AreEqual(9.8f, parsed["rigidbodies"][0]["mass"].GetSingle());
        }

        [Test]
        public void VciJoints()
        {
            var src = new glTF_VCAST_vci_joints
            {
                joints = new System.Collections.Generic.List<glTF_VCAST_vci_joint>
                {
                    new glTF_VCAST_vci_joint
                    {
                        spring = new glTF_VCAST_vci_joint.Spring
                        {
                            tolerance = 10.5f
                        }
                    }
                }
            };

            var f = new JsonFormatter();
            f.Serialize(src);
            var parsed = JsonParser.Parse(new Utf8String(f.GetStore().Bytes));

            Assert.AreEqual(1, parsed["joints"].ArrayItems().Count());
            Assert.AreEqual(10.5f, parsed["joints"][0]["spring"]["tolerance"].GetSingle());
        }

        [Test]
        public void VciItem()
        {
            var src = new glTF_VCAST_vci_item
            {
                grabbable = true,
            };

            var f = new JsonFormatter();
            f.Serialize(src);
            var parsed = JsonParser.Parse(new Utf8String(f.GetStore().Bytes));

            Assert.AreEqual(true, parsed["grabbable"].GetBoolean());
        }
    }
}