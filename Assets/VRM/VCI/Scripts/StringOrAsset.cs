using System;
using UnityEngine;
using UniGLTF;
using System.IO;


namespace VCI
{
    [Serializable]
    public class StringOrAsset
    {
        [SerializeField, TextArea(2, 40)]
        public string ScriptText;

        [SerializeField]
        public UnityEngine.Object ScriptAsset;

        public string Script
        {
            set
            {
#if UNITY_EDITOR
                if (ScriptAsset != null)
                {
                    var path = UnityPath.FromAsset(ScriptAsset);
                    File.WriteAllText(path.FullPath, value);
                }
                else
#endif
                {
                    ScriptText = value;
                }
            }
            get
            {
#if UNITY_EDITOR
                if (ScriptAsset != null)
                {
                    var path = UnityPath.FromAsset(ScriptAsset);
                    return File.ReadAllText(path.FullPath);
                }
                else
#endif
                {
                    return ScriptText;
                }
            }
        }

        public string DebugPath
        {
            get
            {
#if UNITY_EDITOR
                if (ScriptAsset != null)
                {
                    var path = UnityPath.FromAsset(ScriptAsset);
                    return path.FullPath;
                }
                else
#endif
                {
                    return "";
                }
            }
        }

    }
}
