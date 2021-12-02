using System;
using System.Linq;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// Editor で 1 回目の VCI インポートを行う.
    /// 1 回目は、VCI に含まれるアセットのうち、Runtime 上の UnityEngine.Object のシリアライズドオブジェクト "ではない" 形式で Project 上に保存したいものを、展開する責務を持つ.
    ///
    /// たとえば Texture は VCI ファイルに含まれる PNG や JPG 画像のバイナリデータをそのまま Project 上にアセットとして保存したい.
    /// この PNG 画像のアセットに対応する Texture2D は UnityEngine が自動生成するインスタンスである.
    /// つまり Runtime 上のユーザコードで new Texture2D().LoadImage(binary) したインスタンスとは無関係である.
    /// したがって 1 回目で事前に Editor Asset として展開したものを 2 回目の Importer に渡して参照させる必要がある.
    ///
    /// 一方で Mesh ファイルは Runtime 上のユーザコードで new Mesh() したものを Project 上にアセットとして保存するだけで済む.
    /// この Mesh アセットは、Mesh オブジェクトそのもののシリアライズドオブジェクトである.
    /// つまり Runtime 上のユーザコードで new Mesh() したインスタンスと一致する.
    /// したがって 1 回目の Import では扱わない.
    /// </summary>
    internal sealed class VciEditorImporterFirstPass : IDisposable
    {
        private readonly VciData _data;
        private readonly UnityPath _prefabPath;
        private readonly VCIImporter _importer;

        public VciEditorImporterFirstPass(VciData data, UnityPath prefabPath)
        {
            _data = data;
            _prefabPath = prefabPath;

            _importer = new VCIImporter(_data);
        }

        public void Dispose()
        {
            _importer?.Dispose();
        }

        public void Load(Action<VciEditorImporterExternalAssetPathList> continuation)
        {
            using (var gltfInstance = _importer.Load())
            {
                var loadedTextures = gltfInstance.RuntimeResources
                    .Where(x => x.Item1.Type == typeof(Texture))
                    .ToDictionary(x => x.Item1, x => x.Item2 as Texture);

                // NOTE: VCI 用のアセットを Extract する.
                var audioClipPaths = EditorAudioImporter.ExtractAssetFiles(_data, _prefabPath);
                ExtractEffekseerAsEditorAssets();
                ExtractScriptAsEditorAssets();
                ExtractColliderMeshAsEditorAssets();

                // NOTE: GLTF の Texture アセット を Extract する.
                var dirName = $"{_prefabPath.FileNameWithoutExtension}.Textures";
                TextureExtractor.ExtractTextures(
                    _importer.Data,
                    _prefabPath.Parent.Child(dirName),
                    _importer.TextureDescriptorGenerator,
                    loadedTextures,
                    (x, y) => { },
                    (textureAssetPaths) =>
                    {
                        continuation(new VciEditorImporterExternalAssetPathList(
                            textureAssetPaths.ToList(),
                            audioClipPaths.ToList()
                        ));
                    }
                );
            }
        }

        private void ExtractEffekseerAsEditorAssets()
        {
            new EditorEffekseerImporter(_data, _importer, _prefabPath).ExtractAssetFiles();
        }

        private void ExtractScriptAsEditorAssets()
        {
            new EditorScriptImporter(_data, _importer, _prefabPath).ExtractAssetFiles();
        }

        private void ExtractColliderMeshAsEditorAssets()
        {
            new EditorPhysicsColliderImporter(_data, _importer, _prefabPath).ExtractAssetFiles();
        }
    }
}
