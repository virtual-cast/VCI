using UniGLTF;

namespace VCI
{
    public sealed class VciEditorImporter
    {
        private readonly VciData _data;
        private readonly UnityPath _prefabPath;

        public VciEditorImporter(VciData data, UnityPath prefabPath)
        {
            _data = data;
            _prefabPath = prefabPath;
        }

        public void Load()
        {
            using (var firstPassImporter = new VciEditorImporterFirstPass(_data, _prefabPath))
            {
                firstPassImporter.Load((paths) =>
                {
                    using (var secondPassImporter = new VciEditorImporterSecondPass(_data, _prefabPath, paths))
                    {
                        secondPassImporter.Load();
                    }
                });
            }
        }
    }
}
