using System;
using VCIJSON;


namespace VCIGLTF
{
    [Serializable]
    public partial class glTFPrimitives_extensions : ExtensionsBase<glTFPrimitives_extensions>
    {
        [JsonSerializeMembers]
        void SerializeMembers_draco(GLTFJsonFormatter f)
        {
            //throw new NotImplementedException();
        }
    }
}
