using UnityEditor;

namespace VCI
{
    public static class VciPackageJsonMenu
    {
        private const string GeneratePackageJsonMenuName = Constants.DevelopMenuPrefix + "/Generate package.json";

#if VCI_DEVELOP
        [MenuItem(GeneratePackageJsonMenuName)]
#endif
        private static void GeneratePackageJson()
        {
            WriteVciPackageJsonService.WritePackageJsonFile();
        }
    }
}
