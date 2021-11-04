using UnityEditor;
using UnityEngine;

namespace VCI
{
    public sealed class UnitySettingsLayerValidator : IUnitySettingsValidator
    {
        public bool IsValid => IsCompleteLayers();
        public string ValidationDescription => string.Format("Layer Settings (current = {0})", (IsValid) ? "Valid" : "Invalid");
        public string ValidationButtonText => "Use recommended Layer Settings";
        public void OnValidate()
        {
            var tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            OverwriteLayers(tagManager);
            tagManager.ApplyModifiedProperties();
        }

        private static readonly string[] requiredLayers =
        {
            VciDefaultLayerSettings.LocationLayerName,
            VciDefaultLayerSettings.PickUpLayerName,
            VciDefaultLayerSettings.AccessoryLayerName,
            VciDefaultLayerSettings.ItemLayerName,
        };

        private static readonly int[] requiredLayerIds =
        {
            24,
            25,
            26,
            27,
        };

        private static void OverwriteLayers(SerializedObject tagManager)
        {
            var layersProp = tagManager.FindProperty("layers");
            var index = 0;
            foreach (var layerId in requiredLayerIds)
            {
                if (layersProp.arraySize > layerId)
                {
                    var sp = layersProp.GetArrayElementAtIndex(layerId);
                    if (sp != null && sp.stringValue != requiredLayers[index])
                    {
                        sp.stringValue = requiredLayers[index];
                        Debug.Log("Adding layer " + requiredLayers[index]);
                    }
                }

                index++;
            }
        }

        private static bool IsCompleteLayers()
        {
            for (int i = 0; i < requiredLayers.Length; i++)
            {
                if (LayerMask.NameToLayer(requiredLayers[i]) == -1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}