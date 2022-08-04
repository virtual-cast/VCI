using System;
using UniGLTF;

namespace VCI
{
    public sealed class VciEditorImporter
    {
        private readonly UnityPath _filePath;
        private readonly UnityPath _prefabPath;

        public VciEditorImporter(UnityPath filePath, UnityPath prefabPath)
        {
            _filePath = filePath;
            _prefabPath = prefabPath;
        }

        public void Load()
        {
            var data = new VciFileParser(_filePath.FullPath).Parse();

            using (var firstPassImporter = new VciEditorImporterFirstPass(data, _prefabPath))
            {
                firstPassImporter.Load((paths) =>
                {
                    using (var secondPassImporter = new VciEditorImporterSecondPass(data, _prefabPath, paths))
                    {
                        secondPassImporter.Load();
                    }

                    // NOTE: すべてのロード処理が終わったあとに、VciData を破棄する.
                    data.Dispose();
                });
            }
        }
    }
}
