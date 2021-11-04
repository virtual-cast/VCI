using System;
using UniGLTF;
using UniJSON;

namespace VCI
{
    public static class VciSerializerExtensions
    {
        public static bool TryDeserializeExtensions<T>(this UniGLTF.glTFExtension extension, string extensionName,  Func<JsonNode, T> deserializer, out T vci)
        {
            if (extension is glTFExtensionImport import)
            {
                foreach (var kv in import.ObjectItems())
                {
                    if (kv.Key.GetString() == extensionName)
                    {
                        vci = deserializer(kv.Value);
                        return true;
                    }
                }
            }

            vci = default;
            return false;
        }
    }
}