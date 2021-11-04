using System.Linq;
using NUnit.Framework;
using UniGLTF;
//using VCIJSON;
using UniJSON;

namespace VCI
{
    public class VciGenSerializerTests
    {
        [Test]
        public void VciMeta()
        {
            var meta = new glTF_VCAST_vci_meta
            {
                author = "AUTHOR",
            };

            //var f = new JsonFormatter();
            //f.Serialize(src);
            //var parsed = JsonParser.Parse(new Utf8String(f.GetStore().Bytes));

            //Assert.AreEqual("AUTHOR", parsed["author"].GetString());

            //var dst = new glTF_VCAST_vci_meta();
            //parsed.Deserialize(ref dst);
            //Assert.AreEqual(src.scriptFormat, dst.scriptFormat);

            var f = new UniJSON.JsonFormatter();
            glTF_VCAST_vci_meta_Serializer.Serialize(f, meta);
            var bytes = f.GetStoreBytes();
            UnityEngine.Debug.Log(System.Text.Encoding.UTF8.GetString(bytes.ToArray()));
            var parsed = bytes.ParseAsJson();

            Assert.AreEqual("AUTHOR", parsed["author"].GetString());
        }
    }
}

