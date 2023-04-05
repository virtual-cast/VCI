using System;
using UniGLTF;

namespace VCI
{
    [Serializable]
    public sealed class AudioSourceJsonObject
    {
        [JsonSchema(Required = true)]
        public int audio;
        public float spatialBlend;

        // NOTE: デフォルト値をここで設定する。JSONデータに pitch が...
        //   - ...存在するなら、この値が上書きされる
        //   - ...存在しないなら、このデフォルト値がそのまま残る
        public float pitch = 1f;

        // NOTE: Unity のデフォルト値を VCI でも使用
        public float minDistance = 1f;
        public float maxDistance = 500f;
    }
}
