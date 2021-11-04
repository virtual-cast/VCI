using System.Threading.Tasks;
using UniGLTF;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public static class MetaImporter
    {
        public static async Task<VCIMeta> LoadMetaAsync(
            GltfData gltfData,
            glTF_VCAST_vci_meta meta,
            TextureFactory textureFactory,
            IAwaitCaller awaitCaller)
        {
            Texture2D thumbTexture = default;
            if (meta.thumbnail != -1)
            {
                var (_, param) = GltfTextureImporter.CreateSRGB(gltfData, meta.thumbnail, Vector2.zero, Vector2.one);
                thumbTexture = await textureFactory.GetTextureAsync(param, awaitCaller) as Texture2D;
            }

            return new VCIMeta
            {
                exporterVersion = meta.exporterVCIVersion,
                specVersion = meta.specVersion,

                title = meta.title,
                version = meta.version,
                author = meta.author,
                contactInformation = meta.contactInformation,
                reference = meta.reference,
                description = meta.description,
                thumbnail = thumbTexture,

                modelDataLicenseType = LoadLicenseType(meta.modelDataLicenseType),
                modelDataOtherLicenseUrl = meta.modelDataOtherLicenseUrl,
                scriptLicenseType = LoadLicenseType(meta.scriptLicenseType),
                scriptOtherLicenseUrl = meta.scriptOtherLicenseUrl,

                scriptWriteProtected = meta.scriptWriteProtected,
                scriptEnableDebugging = meta.scriptEnableDebugging,
            };
        }

        private static VciMetaLicenseType LoadLicenseType(string type)
        {
            switch (type)
            {
                case glTF_VCAST_vci_meta.RedistributionProhibitedLicenseTypeString:
                    return VciMetaLicenseType.Redistribution_Prohibited;
                case glTF_VCAST_vci_meta.Cc0LicenseTypeString:
                    return VciMetaLicenseType.CC0;
                case glTF_VCAST_vci_meta.CcByLicenseTypeString:
                    return VciMetaLicenseType.CC_BY;
                case glTF_VCAST_vci_meta.CcByNcLicenseTypeString:
                    return VciMetaLicenseType.CC_BY_NC;
                case glTF_VCAST_vci_meta.CcBySaLicenseTypeString:
                    return VciMetaLicenseType.CC_BY_SA;
                case glTF_VCAST_vci_meta.CcByNcSaLicenseTypeString:
                    return VciMetaLicenseType.CC_BY_NC_SA;
                case glTF_VCAST_vci_meta.CcByNdLicenseTypeString:
                    return VciMetaLicenseType.CC_BY_ND;
                case glTF_VCAST_vci_meta.CcByNcNdLicenseTypeString:
                    return VciMetaLicenseType.CC_BY_NC_ND;
                case glTF_VCAST_vci_meta.OtherLicenseTypeString:
                    return VciMetaLicenseType.Other;
                default: // 未定義ならデフォルトにフォールバックする.
                    return VciMetaLicenseType.Redistribution_Prohibited;
            }
        }
    }
}
