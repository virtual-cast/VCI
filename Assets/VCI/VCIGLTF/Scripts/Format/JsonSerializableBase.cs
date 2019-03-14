using System;


namespace VCIGLTF
{
    [Serializable]
    public abstract class JsonSerializableBase
    {
        protected abstract void SerializeMembers(GLTFJsonFormatter f);

        public string ToJson()
        {
            var f = new GLTFJsonFormatter();
            f.BeginMap();

            SerializeMembers(f);

            f.EndMap();
            return f.ToString();
        }
    }
}
