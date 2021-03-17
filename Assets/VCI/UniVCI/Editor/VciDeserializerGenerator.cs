using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UniGLTF;
using UnityEditor;
using UnityEngine;

namespace VCI
{
    public static class VciDeserializerGenerator
    {
        public const BindingFlags FIELD_FLAGS = BindingFlags.Instance | BindingFlags.Public;

        static string GetPath(string name)
        {
            return Path.Combine(UnityEngine.Application.dataPath,
                "VCI/Runtime/Format/"+ name + ".g.cs");
        }

        /// <summary>
        /// AOT向けにデシリアライザを生成する
        /// </summary>
        [MenuItem(VCI.VCIVersion.MENU + "/VCI: Generate Deserializer")]
        static void GenerateSerializer()
        {
            var extensions = new glTF_VCI_extensions();
            var extensionsTypes = extensions.GetType();
            foreach (var info in extensionsTypes.GetFields())
            {
                Debug.Log(info.Name);
                GenerateSerializerTypes(info.FieldType);
            }
        }

        static void GenerateSerializerTypes(System.Type type)
        {
            var name = string.Format("{0}_Deserialize", type.Name);
            var info = new UniGLTF.ObjectSerialization(type, "vci", name);
            Debug.Log(info);

            using (var s = File.Open(GetPath(name), FileMode.Create))
            using (var w = new StreamWriter(s, Encoding.UTF8))
            {
                w.NewLine = "\n";
                w.Write(@"
using System;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UniGLTF;
using VCI;
using Object = System.Object;

namespace VCI {

public static class ");
                w.Write(@"
" + type.Name + "_Deserializer");

                w.Write(@"
{

");

                info.GenerateDeserializer(w, "Deserialize");

                // footer
                w.Write(@"
} // VciDeserializer
} // VCI
");

            }

            // CRLF を LF に変換して再保存
            File.WriteAllText(GetPath(name), File.ReadAllText(GetPath(name), Encoding.UTF8).Replace("\r\n", "\n"), Encoding.UTF8);

            Debug.LogFormat("write: {0}", GetPath(name));
            UnityPath.FromFullpath(GetPath(name)).ImportAsset();
        }
    }
}
