using System.Collections.Generic;
using UniGLTF;

namespace VCI
{
    /// <summary>
    /// VCIImporter に External Asset として渡すオブジェクトリスト
    /// </summary>
    internal sealed class VciEditorImporterExternalAssetPathList
    {
        public List<UnityPath> TexturePaths { get; }
        public List<UnityPath> AudioClipPaths { get; }

        public VciEditorImporterExternalAssetPathList(List<UnityPath> texturePaths, List<UnityPath> audioClipPaths)
        {
            TexturePaths = texturePaths;
            AudioClipPaths = audioClipPaths;
        }
    }
}
