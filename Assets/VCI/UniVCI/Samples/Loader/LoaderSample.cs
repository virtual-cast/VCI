using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace VCI.Samples
{
    public class LoaderSample : MonoBehaviour
    {
        [SerializeField]
        private Button _openButton;

        private RuntimeVciInstance _instance;

        private void OnEnable()
        {
            _openButton.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            var path = GetPath();
            if (string.IsNullOrEmpty(path)) return;

            var task = LoadAsync(path);
        }

        private string GetPath()
        {
#if UNITY_EDITOR
            return EditorUtility.OpenFilePanel("open vci", null, "Model;*.vci;");
#else
            return string.Empty;
#endif
        }

        private async Task LoadAsync(string path)
        {
            try
            {
                _openButton.interactable = false;

                Debug.LogFormat("load: {0}", path);

                // Dispose old one
                _instance?.Dispose();
                _instance = null;

                var data = await Task.Run(() => new VciFileParser(path).Parse());
                using (var loader = new VCIImporter(data))
                {
                    await loader.LoadAsync();

                    _instance = loader.RuntimeVciInstance;
                    _instance.ShowMeshes();
                    _instance.EnablePhysicalBehaviour(true);
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            finally
            {
                _openButton.interactable = true;
            }
        }
    }
}