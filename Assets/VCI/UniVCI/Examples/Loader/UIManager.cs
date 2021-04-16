using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniGLTF;
using UniGLTF.Legacy;
#if UNITY_EDITOR
using UnityEditor;

#endif


namespace VCI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button m_open;

        private void OnEnable()
        {
            m_open.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            m_open.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
#if UNITY_EDITOR
            var path = EditorUtility.OpenFilePanel("open vci", null, "Model;*.vci;*.glb;*.gltf;*.zip;");
            if (string.IsNullOrEmpty(path)) return;

            StartCoroutine(Load(path));
#endif
        }

        private LegacyImporterContext m_loaded;

        private IEnumerator Load(string path)
        {
            Debug.LogFormat("load: {0}", path);

            if (m_loaded != null) m_loaded.Dispose();

            var loader = new VCIImporter();
            loader.Parse(path);
            yield return loader.LoadCoroutine();
            yield return loader.SetupCoroutine();

            loader.ShowMeshes();

            m_loaded = loader;
        }
    }
}