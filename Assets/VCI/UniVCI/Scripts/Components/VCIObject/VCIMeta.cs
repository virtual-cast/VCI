using System;
using UnityEngine;

namespace VCI
{
    [Serializable]
    public struct VCIMeta
    {
        public string title;
        public string author;
        public string contactInformation;
        public string reference;
        public Texture2D thumbnail;
        public string version;
        [TextArea(1, 16)] public string description;
        public string exporterVersion;
        public string specVersion;

        [Header("Model Data License")]
        public VciMetaLicenseType modelDataLicenseType;
        public string modelDataOtherLicenseUrl;

        [Header("Script License")]
        public VciMetaLicenseType scriptLicenseType;
        public string scriptOtherLicenseUrl;

        [Header("Script Settings")]
        public bool scriptWriteProtected;
        public bool scriptEnableDebugging;
    }
}