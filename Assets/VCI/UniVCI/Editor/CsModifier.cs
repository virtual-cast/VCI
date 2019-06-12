using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class CsModifier
{
    private const string Key = "Assets/CsModifier";

    /// <summary>
    /// C#の文字コードは、BOM付きUTF-8でないとだめ
    /// https://www.buildinsider.net/language/csharpunicode/02
    /// ソースコードの文字コード判定節参照
    /// </summary>
    static readonly Encoding CsEncoding = new UTF8Encoding(true);

    /// <summary>
    /// Windowsで開発するのならCRLFがデフォルトだが・・・
    /// </summary>
    static readonly string CsNewLine = "\n";

    [MenuItem(Key, true)]
    private static bool Enable()
    {
        return Selection.activeObject is DefaultAsset;
    }

    [MenuItem(Key)]
    private static void DoSomethingWithVariable()
    {
        var asset = Selection.activeObject as DefaultAsset;
        var path = AssetDatabase.GetAssetPath(asset);

        var p = Path.GetFullPath(Path.Combine(Application.dataPath, "../" + path));

        var i = 0;
        foreach (var cs in Traverse(new DirectoryInfo(p)).Where(x => x.Extension.ToLower() == ".cs"))
        {
            Filter(cs);
            ++i;
        }

        Debug.Log($"Process {p} => {i} files ");
    }

    private static void Filter(FileInfo cs)
    {
        var source = File.ReadAllText(cs.FullName);

        source = string.Join(CsNewLine,
            source
                .Split('\n')
                .Select(x => x.TrimEnd())
        );

        File.WriteAllText(cs.FullName, source, CsEncoding);
    }

    private static IEnumerable<FileInfo> Traverse(DirectoryInfo dir)
    {
        foreach (var f in dir.EnumerateFiles()) yield return f;

        foreach (var d in dir.EnumerateDirectories())
        foreach (var x in Traverse(d))
            yield return x;
    }
}