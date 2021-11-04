using System;
using VRMShaders;

namespace VCI
{
    public static class MetaExporter
    {
        public static glTF_VCAST_vci_meta ExportMeta(VCIObject vciObject, ITextureExporter textureExporter)
        {
            if (vciObject == null)
            {
                return null;
            }

            var meta = vciObject.Meta;
            var metaExtension = new glTF_VCAST_vci_meta
            {
                exporterVCIVersion = VCIVersion.VCI_VERSION,
                specVersion = VCISpecVersion.Version,

                title = meta.title,

                version = meta.version,
                author = meta.author,
                contactInformation = meta.contactInformation,
                reference = meta.reference,
                description = meta.description,

                modelDataLicenseType = ExportLicenseType(meta.modelDataLicenseType),
                modelDataOtherLicenseUrl = meta.modelDataOtherLicenseUrl,
                scriptLicenseType = ExportLicenseType(meta.scriptLicenseType),
                scriptOtherLicenseUrl = meta.scriptOtherLicenseUrl,

                scriptWriteProtected = meta.scriptWriteProtected,
                scriptEnableDebugging = meta.scriptEnableDebugging,
            };

            if (meta.thumbnail != null)
            {
                metaExtension.thumbnail = textureExporter.RegisterExportingAsSRgb(meta.thumbnail, needsAlpha: true);
            }

            return metaExtension;
        }

        private static string ExportLicenseType(VciMetaLicenseType type)
        {
            switch (type)
            {
                case VciMetaLicenseType.Redistribution_Prohibited:
                    return glTF_VCAST_vci_meta.RedistributionProhibitedLicenseTypeString;
                case VciMetaLicenseType.CC0:
                    return glTF_VCAST_vci_meta.Cc0LicenseTypeString;
                case VciMetaLicenseType.CC_BY:
                    return glTF_VCAST_vci_meta.CcByLicenseTypeString;
                case VciMetaLicenseType.CC_BY_NC:
                    return glTF_VCAST_vci_meta.CcByNcLicenseTypeString;
                case VciMetaLicenseType.CC_BY_SA:
                    return glTF_VCAST_vci_meta.CcBySaLicenseTypeString;
                case VciMetaLicenseType.CC_BY_NC_SA:
                    return glTF_VCAST_vci_meta.CcByNcSaLicenseTypeString;
                case VciMetaLicenseType.CC_BY_ND:
                    return glTF_VCAST_vci_meta.CcByNdLicenseTypeString;
                case VciMetaLicenseType.CC_BY_NC_ND:
                    return glTF_VCAST_vci_meta.CcByNcNdLicenseTypeString;
                case VciMetaLicenseType.Other:
                    return glTF_VCAST_vci_meta.OtherLicenseTypeString;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}