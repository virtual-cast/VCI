using System;
using System.Collections.Generic;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_attachable
    {
        public List<string> attachableHumanBodyBones = new List<string>();
        public float attachableDistance;
        public bool scalable;
        public Vector3 offset;

        public static string ExtensionName => "VCAST_vci_attachable";
    }
}
