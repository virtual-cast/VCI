using NUnit.Framework;
using UniGLTF;
using UniJSON;
using UnityEngine;


namespace VCI
{
    public class FormatTests
    {
        [Test]
        public void FormatTest()
        {
            //var f = new GUILayer
            var data = new glTF_VCAST_vci_embedded_script
            {
                scripts = new System.Collections.Generic.List<glTF_VCAST_vci_embedded_script_source>
                {
                    new glTF_VCAST_vci_embedded_script_source
                    {
                        name="main.lua",
                    },
                }
            };

            var f = new JsonFormatter();
            f.Serialize(data);

            var parsed = JsonParser.Parse(new Utf8String(f.GetStore().Bytes));

            Debug.LogFormat("{0}", parsed);
        }
    }
}
