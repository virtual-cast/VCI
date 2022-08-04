using System.IO;
using UnityEditor;

namespace VCI
{
    public static class VCIVersionMenu
    {
        private const string IncrementVersionMenuName = "/Increment Version";
        private const string IncrementMajorVersionMenuName = Constants.DevelopMenuPrefix + IncrementVersionMenuName + "/Increment Major Version";
        private const string IncrementMinorVersionMenuName = Constants.DevelopMenuPrefix + IncrementVersionMenuName + "/Increment Minor Version";
        private const string IncrementPatchVersionMenuName = Constants.DevelopMenuPrefix + IncrementVersionMenuName + "/Increment Patch Version";
        private const string VersionMenuName = Constants.MenuPrefix + "/Version: " + VCIVersion.VCI_VERSION + "." + VCIVersion.PATCH_NUMBER;

        [MenuItem(VersionMenuName, priority = int.MaxValue)]
        private static void VersionMenu() { }

        [MenuItem(VersionMenuName, true)]
        private static bool VersionMenuValidation() { return false; }

#if VCI_DEVELOP
        [MenuItem(IncrementMajorVersionMenuName)]
#endif
        private static void IncrementMajorVersion()
        {
            UpdateVersion(VCIVersion.MAJOR + 1, 0, 0);
        }

#if VCI_DEVELOP
        [MenuItem(IncrementMinorVersionMenuName)]
#endif
        private static void IncrementMinorVersion()
        {
            UpdateVersion(VCIVersion.MAJOR, VCIVersion.MINOR + 1, 0);
        }

#if VCI_DEVELOP
        [MenuItem(IncrementPatchVersionMenuName)]
#endif
        private static void IncrementPatchVersion()
        {
            UpdateVersion(VCIVersion.MAJOR, VCIVersion.MINOR, VCIVersion.PATCH + 1);
        }

        private static void UpdateVersion(int majorVersion, int minorVersion, int patchVersion)
        {
            VciVersionClassWriter.Write(majorVersion, minorVersion, patchVersion);
            VciVersionPackageJsonWriter.Write(majorVersion, minorVersion, patchVersion);
            AssetDatabase.Refresh();
        }
    }
}
