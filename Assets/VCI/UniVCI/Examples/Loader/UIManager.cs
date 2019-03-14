#pragma warning disable
using UnityEngine;
using UnityEngine.UI;
using VCIGLTF;
using System.Collections;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace VCI
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField]
        Button m_open;

        private void OnEnable()
        {
            m_open.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            m_open.onClick.RemoveListener(OnClick);
        }

        void OnClick()
        {
#if UNITY_EDITOR
            var path = EditorUtility.OpenFilePanel("open vci", null, "Model;*.vci;*.glb;*.gltf;*.zip;");
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            StartCoroutine(Load(path));
#endif
        }

        ImporterContext m_loaded;

        private IEnumerator Load(string path)
        {
            Debug.LogFormat("load: {0}", path);

            if (m_loaded != null)
            {
                m_loaded.Destroy(true);
            }

            var loader = new VCIImporter();
            loader.Parse(path);
            yield return loader.LoadCoroutine();
            yield return loader.SetupCorutine();

            loader.ShowMeshes();

            m_loaded = loader;
        }
    }
}
