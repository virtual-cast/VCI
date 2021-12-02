using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
#if UNITY_2018_1_OR_NEWER
using UnityEditor.Build.Reporting;
#endif


namespace VCI
{
    public static class VCIExportUnityPackage
    {
        private const string DATE_FORMAT = "yyyyMMdd";
        private const string PREFIX = "UniVCI";
        private const string PACKAGE_DIR = @"BuildPackages";
        private const string ExportUnityPackageMenuName = Constants.DevelopMenuPrefix + "/Export unitypackage";

        private static string System(string dir, string fileName, string args)
        {
            // Start the child process.
            var p = new System.Diagnostics.Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = args;
            p.StartInfo.WorkingDirectory = dir;
            if (!p.Start()) return "ERROR";
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            var output = p.StandardOutput.ReadToEnd();
            var err = p.StandardError.ReadToEnd();
            p.WaitForExit();

            if (string.IsNullOrEmpty(output)) return err;
            return output;
        }

        //const string GIT_PATH = "C:\\Program Files\\Git\\mingw64\\bin\\git.exe";
        private const string GIT_PATH = "C:\\Program Files\\Git\\bin\\git.exe";

        private static string GetGitHash(string path)
        {
            return System(path, "git.exe", "rev-parse HEAD").Trim();
        }

        private static string GetPackagePath(string folder)
        {
            //var date = DateTime.Today.ToString(DATE_FORMAT);

            var path =
                $"{folder}/{VCIVersion.VCI_VERSION}.{VCIVersion.PATCH_NUMBER}.unitypackage"
                    .Replace("\\", "/");

            return path;
        }

        private static void CleanUpDirectory(string targetDirectoryPath)
        {
            if (Directory.Exists(targetDirectoryPath))
            {
                string[] filePaths = Directory.GetFiles(targetDirectoryPath);
                foreach (string filePath in filePaths)
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }
            }
        }

        // path 以下に存在する、パッケージ含めるべきすべてのファイルを enumerate する
        // - 以下のファイルは enumerate 対象から除外する
        //   - .git ファイル
        //   - isExclude の条件に引っ掛かったファイル
        //   - .meta ファイル
        private static IEnumerable<string> EnumeratePackageFiles(string path, Func<string, bool> isExclude = null)
        {
            path = path.Replace("\\", "/");

            if (Directory.Exists(path))
            {
                foreach (var child in Directory.GetFileSystemEntries(path))
                foreach (var x in EnumeratePackageFiles(child, isExclude))
                    yield return x;
            }
            else
            {
                if (Path.GetFileName(path).StartsWith(".git")) yield break;

                if (isExclude != null && isExclude(path)) yield break;

                if (Path.GetExtension(path).ToLower() == ".meta") yield break;

                yield return path;
            }
        }

        public static bool Build(string[] levels)
        {
            var buildPath = Path.GetFullPath(Application.dataPath + "/../build/build.exe");
            Debug.LogFormat("{0}", buildPath);
            var build = BuildPipeline.BuildPlayer(levels,
                buildPath,
                BuildTarget.StandaloneWindows,
                BuildOptions.None
            );
#if UNITY_2018_1_OR_NEWER
            var iSuccess = build.summary.result != BuildResult.Succeeded;
#else
            var iSuccess = string.IsNullOrEmpty(build);
#endif
            return iSuccess;
        }

        public static bool BuildTestScene()
        {
            var levels = new string[] {"Assets/VCI/UniVCI/Examples/vci_setup.unity"};
            return Build(levels);
        }

        private static bool EndsWith(string path, params string[] exts)
        {
            foreach (var ext in exts)
            {
                if (path.EndsWith(ext)) return true;
                if (path.EndsWith(ext + ".meta")) return true;
            }

            return false;
        }

        // path が ExportToPackageDirectories に含まれていないフォルダ以下のフォルダ/ファイルであった場合、trueを返す
        private static bool IsExcludeFromPackage(string path)
        {
            if (!ExportToPackageDirectories.Any(path.StartsWith)) return true;

            return false;
        }

        private static readonly string[] ExportToPackageDirectories = new string[]
        {
            // VCI のメイン実装
            "Assets/VCI/UniVCI",

            // Effekseer
            // - Assets/ScriptsExternalのみビルド対象から外す
            "Assets/Effekseer/Editor",
            "Assets/Effekseer/Materials",
            "Assets/Effekseer/Plugins",
            "Assets/Effekseer/Resources",
            "Assets/Effekseer/Scripts",

            // NAudio
            "Assets/NAudio",

            // 公式サンプルVCI
            "Assets/VCI-Official-Samples",

            // DigitalSignature files
            "Assets/VCI-DigitalSignature",
        };

        // 明示的に指定して出力するasmdefファイル群
        private static readonly string[] AdfFiles = new string[]
        {
            // Assets/Effekseerディレクトリ内のファイルはパッケージ出力対象になっていない
            "Assets/Effekseer/EffekseerAssemblyDefinition.asmdef",
        };

        // 明示的に指定して出力するpackage.jsonファイル群
        // - unitypackageをインポートするだけでユーザー公開のUniVCIリポジトリをUPM Readyにするために必要
        private static readonly string[] PackageJsonFiles = new string[]
        {
            "Assets/VCI/package.json",
            "Assets/Effekseer/package.json"
            // NAudioのpackage.jsonは、EnumeratePackageFilesの対象であるためここで指定する必要はない
        };

#if VCI_DEVELOP
        [MenuItem(ExportUnityPackageMenuName)]
#endif
        public static void CreateUnityPackage()
        {
            var packageExportDirectoryPath = Path.Combine(Path.GetFullPath(Path.Combine(Application.dataPath, "..")), PACKAGE_DIR);

            if (!Directory.Exists(packageExportDirectoryPath))
            {
                Directory.CreateDirectory(packageExportDirectoryPath);
            }

            CleanUpDirectory(packageExportDirectoryPath);

            var defaultPackagePath = GetPackagePath(packageExportDirectoryPath);
            {
                var vciFiles = EnumeratePackageFiles("Assets/VCI", IsExcludeFromPackage).ToArray();
                var effekseerFiles = EnumeratePackageFiles("Assets/Effekseer", IsExcludeFromPackage).ToArray();
                var nAudioFiles = EnumeratePackageFiles("Assets/NAudio", IsExcludeFromPackage).ToArray();
                var vciSampleFiles = EnumeratePackageFiles("Assets/VCI-Official-Samples", IsExcludeFromPackage).ToArray();
                var additionalFiles = AdfFiles.Concat(PackageJsonFiles).ToArray();
                var defaultPackageFiles = additionalFiles.Concat(vciFiles.Concat(effekseerFiles.Concat(nAudioFiles.Concat(vciSampleFiles)))).ToArray();

                // Default Package
                Debug.LogFormat("{0}",
                    string.Join("", defaultPackageFiles.Select((x, i) => $"[{i:##0}] {x}\n").ToArray()));
                AssetDatabase.ExportPackage(defaultPackageFiles
                    , defaultPackagePath,
                    ExportPackageOptions.Default);

                // DigitalSignature Package
                {
                    var digitalSignatureFiles = EnumeratePackageFiles("Assets/VCI-DigitalSignature", IsExcludeFromPackage).ToArray();
                    var defaultPackageFileName = Path.GetFileNameWithoutExtension(defaultPackagePath);
                    var packageExtension = Path.GetExtension(defaultPackagePath);
                    var digitalSignaturePackagePath = Path.GetDirectoryName(defaultPackagePath) + "\\" + defaultPackageFileName + "_digitalSignature" + packageExtension;
                    var digitalSignaturePackageFiles = defaultPackageFiles.Concat(digitalSignatureFiles).ToArray();
                    AssetDatabase.ExportPackage(digitalSignaturePackageFiles
                        , digitalSignaturePackagePath,
                        ExportPackageOptions.Default);
                }
            }

            Debug.LogFormat("exported: {0}", defaultPackagePath);
        }
    }
}