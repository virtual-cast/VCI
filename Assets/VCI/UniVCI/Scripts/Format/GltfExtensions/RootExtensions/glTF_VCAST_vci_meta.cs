using System;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_meta
    {
        public static string ExtensionName => "VCAST_vci_meta";

        public const string RedistributionProhibitedLicenseTypeString = "redistribution_prohibited";
        public const string Cc0LicenseTypeString = "cc0";
        public const string CcByLicenseTypeString = "cc_by";
        public const string CcByNcLicenseTypeString = "cc_by_nc";
        public const string CcBySaLicenseTypeString = "cc_by_sa";
        public const string CcByNcSaLicenseTypeString = "cc_by_nc_sa";
        public const string CcByNdLicenseTypeString = "cc_by_nd";
        public const string CcByNcNdLicenseTypeString = "cc_by_nc_nd";
        public const string OtherLicenseTypeString = "other";

        // from UniVCI-0.5
        public string exporterVCIVersion;

        public string specVersion;

        [UniGLTF.JsonSchema(Description = "Title of VCI model")]
        public string title;

        [UniGLTF.JsonSchema(Description = "Version of VCI model")]
        public string version;

        [UniGLTF.JsonSchema(Description = "Author of VCI model")]
        public string author;

        [UniGLTF.JsonSchema(Description = "Contact Information of VCI model author")]
        public string contactInformation;

        [UniGLTF.JsonSchema(Description = "Reference of VCI model")]
        public string reference;

        [UniGLTF.JsonSchema(Description = "description")]
        public string description;

        [UniGLTF.JsonSchema(Description = "Thumbnail of VCI model")]
        public int thumbnail = -1;

        [UniGLTF.JsonSchema(Description = "Model Data License type")]
        public string modelDataLicenseType;

        [UniGLTF.JsonSchema(Description = "If Other is selected, put the URL link of the license document here.")]
        public string modelDataOtherLicenseUrl;

        [UniGLTF.JsonSchema(Description = "Script License type")]
        public string scriptLicenseType;

        [UniGLTF.JsonSchema(Description = "If Other is selected, put the URL link of the license document here.")]
        public string scriptOtherLicenseUrl;

        [UniGLTF.JsonSchema(Description = "Script write protected")]
        public bool scriptWriteProtected;

        [UniGLTF.JsonSchema(Description = "Script enable debugging")]
        public bool scriptEnableDebugging;
    }
}
