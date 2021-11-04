using System.IO;
using System.Text;
using UniGLTF;
using UnityEditor;
using UnityEngine;


namespace VCI
{
    /// <summary>
    /// Copy from UniGLTF.SerializerGenerator
    /// </summary>
    public static class VciSerializerGenerator
    {
        private static string OutputDirectory => Path.Combine(Application.dataPath, "VCI/UniVCI/Scripts/Format/Serializer/");

        public static string GetOutputFilePath(string name)
        {
            return Path.Combine(OutputDirectory, name + ".g.cs");
        }

        [MenuItem(Constants.MenuPrefix + "/VCI: Generate Serializer", priority = 2)]
        static void GenerateSerializer()
        {
            var extensions = new VciAllExtensions();
            var extensionsTypes = extensions.GetType();
            foreach (var info in extensionsTypes.GetFields())
            {
                Debug.Log(info.Name);
                GenerateSerializerEachType(info.FieldType);
            }
        }

        static void GenerateSerializerEachType(System.Type type)
        {
            var name = string.Format("{0}_Serialize", type.Name);
            var info = new UniGLTF.ObjectSerialization(type, "vci", name);
            Debug.Log(info);

            var outPath = GetOutputFilePath(name);

            using (var s = File.Open(outPath, FileMode.Create))
            using (var w = new StreamWriter(s, new UTF8Encoding(false)))
            {
                w.Write($@"using System;
using System.Collections.Generic;
using UniJSON;
using UniGLTF;
using UnityEngine;

namespace VCI
{{
    public static class {type.Name}_Serializer
    {{

"
                );
                info.GenerateSerializer(w, "Serialize");
                w.Write(@"
    } // class
} // namespace
"
                );
            }

            Debug.LogFormat("write: {0}", outPath);
            UnityPath.FromFullpath(outPath).ImportAsset();
        }
    }
}
