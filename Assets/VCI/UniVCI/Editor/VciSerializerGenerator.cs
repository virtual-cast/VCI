using System.IO;
using System.Reflection;
using System.Text;
using UniGLTF;
using UnityEditor;
using UnityEngine;


namespace VCI
{

    public static class VciSerializerGenerator
    {
        const BindingFlags FIELD_FLAGS = BindingFlags.Instance | BindingFlags.Public;

        static string GetPath(string name)
        {
            return Path.Combine(UnityEngine.Application.dataPath,
                "VCI/Runtime/Format/" + name + ".g.cs");
        }

        /// <summary>
        /// AOT向けにシリアライザを生成する
        /// </summary>
        [MenuItem(VCI.VCIVersion.MENU + "/VCI: Generate Serializer")]
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
            var info = new ObjectSerialization(type, "vci", "Serialize_");
            Debug.Log(info);

            var name = string.Format("{0}_Serialize", type.Name);
            using (var s = File.Open(GetPath(name), FileMode.Create))
            using (var w = new StreamWriter(s, new UTF8Encoding(false)))
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
" + type.Name + "_Serializer");

                w.Write(@"
{

");
                info.GenerateSerializer(w, "Serialize");
                // footer
                w.Write(@"
} // VciSerializer
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
