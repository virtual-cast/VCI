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

        private static string GetPath(string folder)
        {
            //var date = DateTime.Today.ToString(DATE_FORMAT);

            var path = string.Format("{0}/{1}.unitypackage",
                folder,
                VCIVersion.VCI_VERSION
            ).Replace("\\", "/");

            return path;
        }

        private static IEnumerable<string> EnumerateFiles(string path, Func<string, bool> isExclude = null)
        {
            path = path.Replace("\\", "/");

            if (Directory.Exists(path))
            {
                foreach (var child in Directory.GetFileSystemEntries(path))
                foreach (var x in EnumerateFiles(child, isExclude))
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

        private static bool IsExclude(string path)
        {
            if (!IncluceFiles.Any(x => path.StartsWith(x))) return true;

            return false;
        }

        private static string[] IncluceFiles = new string[]
        {
            "Assets/VCI/UniVCI",
            "Assets/VCI/VCIGLTF",
            "Assets/VCI/VCIJSON",
            "Assets/VCI/VCIDepthFirstScheduler",

            "Assets/VRM/MToon",
            "Assets/VRM/ShaderProperty",
            "Assets/VRM/UniGLTF",
            "Assets/VRM/UniUnlit",

            "Assets/Effekseer/Editor",
            "Assets/Effekseer/Materials",
            "Assets/Effekseer/Plugins",
            "Assets/Effekseer/Resources",
            "Assets/Effekseer/Scripts",

            "Assets/NAudio",

            // DigitalSignature files
            "Assets/VCI-DigitalSignature",
        };


        private static string[] adfFiles = new string[]
        {
            "Assets/VCI/VCI.asmdef",
            "Assets/VRM/VRM.asmdef",
            "Assets/Effekseer/EffekseerAssemblyDefinition.asmdef",
        };




#if VCI_DEVELOP
        [MenuItem(VCIVersion.MENU + "/Export unitypackage")]
#endif
        public static void CreateUnityPackage()
        {
            var folder = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));

            var path = GetPath(folder);
            /*
            if (File.Exists(path))
            {
                Debug.LogErrorFormat("{0} is already exists", path);
                return;
            }
            */

            {
                var filesA = EnumerateFiles("Assets/VCI", IsExclude).ToArray();
                var filesB = EnumerateFiles("Assets/VRM", IsExclude).ToArray();
                var filesC = EnumerateFiles("Assets/Effekseer", IsExclude).ToArray();
                var filesD = EnumerateFiles("Assets/NAudio", IsExclude).ToArray();
                var files = adfFiles.Concat(filesA.Concat(filesB.Concat(filesC.Concat(filesD)))).ToArray();

                // Default Package
                Debug.LogFormat("{0}",
                    string.Join("", files.Select((x, i) => string.Format("[{0:##0}] {1}\n", i, x)).ToArray()));
                AssetDatabase.ExportPackage(files
                    , path,
                    ExportPackageOptions.Default);

                // DigitalSignature Package
                var digitalSignatureFiles = EnumerateFiles("Assets/VCI-DigitalSignature", IsExclude).ToArray();
                var fileName = Path.GetFileNameWithoutExtension(path);
                var ext = Path.GetExtension(path);
                path = Path.GetDirectoryName(path) + "\\" + fileName + "_digitalSignature" + ext;
                files = files.Concat(digitalSignatureFiles).ToArray();
                AssetDatabase.ExportPackage(files
                    , path,
                    ExportPackageOptions.Default);
            }

            Debug.LogFormat("exported: {0}", path);
        }
    }
}