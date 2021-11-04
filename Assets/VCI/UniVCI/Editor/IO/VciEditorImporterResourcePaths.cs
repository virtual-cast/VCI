using System.Collections.Generic;
using UniGLTF;

namespace VCI
{
    /// <summary>
    /// VCIImporter に External Asset として渡すオブジェクトリスト
    /// </summary>
    public sealed class VciEditorImporterResourcePaths
    {
        public List<UnityPath> TexturePaths = new List<UnityPath>();
        public List<UnityPath> AudioClipPaths = new List<UnityPath>();
    }
}
