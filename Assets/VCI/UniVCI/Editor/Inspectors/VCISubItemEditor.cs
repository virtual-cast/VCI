using UnityEditor;

namespace VCI
{
    [CustomEditor(typeof(VCISubItem))]
    [CanEditMultipleObjects]
    public sealed class VCISubItemEditor : Editor
    {
        private static readonly string[] CameraMaskOptions = { "Visible to Camera Items", "Invisible to Camera Items" };
        private SerializedProperty grabbable;
        private SerializedProperty scalable;
        private SerializedProperty uniform;
        private SerializedProperty attractable;
        private SerializedProperty attractableDistance;
        private SerializedProperty group;
        private SerializedProperty isVisibleToCamera;

        private void OnEnable()
        {
            grabbable = serializedObject.FindProperty("Grabbable");
            scalable = serializedObject.FindProperty("Scalable");
            uniform = serializedObject.FindProperty("UniformScaling");
            attractable = serializedObject.FindProperty("Attractable");
            attractableDistance = serializedObject.FindProperty("AttractableDistance");
            group = serializedObject.FindProperty("GroupId");
            isVisibleToCamera = serializedObject.FindProperty("IsVisibleToCamera");
        }

        public override void OnInspectorGUI()
        {
            var subItem = (VCISubItem)target;
            var subItemParent = subItem.transform.parent;
            if (subItem.GetComponent<VCIObject>() != null ||
                subItemParent == null ||
                subItemParent.GetComponent<VCIObject>() == null ||
                subItemParent.parent != null
               )
            {
                EditorGUILayout.HelpBox(VCIConfig.GetText("warning_subitem_not_under_vciobject"), MessageType.Error);
            }


            serializedObject.Update();
            {
                // 利用環境によらない設定
                EditorGUILayout.LabelField("Common", EditorStyles.boldLabel);

                using (var check = new EditorGUI.ChangeCheckScope())
                {
                    EditorGUILayout.PropertyField(grabbable);
                    if (check.changed)
                    {
                        attractable.boolValue = grabbable.boolValue;
                    }
                }
                using (new EditorGUI.IndentLevelScope())
                {
                    using (new EditorGUI.DisabledGroupScope(!grabbable.boolValue))
                    {
                        EditorGUILayout.PropertyField(scalable);
                        using (new EditorGUI.DisabledGroupScope(!scalable.boolValue))
                        using (new EditorGUI.IndentLevelScope())
                        {
                            EditorGUILayout.PropertyField(uniform);
                        }
                        EditorGUILayout.PropertyField(attractable);
                        // TODO: クライアントがまだ実装されていないので、一旦設定 UI を塞ぐ。対応されたらコメントアウトを外す
                        /*
                        using (new EditorGUI.DisabledGroupScope(!attractable.boolValue))
                        using (new EditorGUI.IndentLevelScope())
                        {
                            attractableDistance.floatValue = EditorGUILayout.Slider("Attractable Distance", attractableDistance.floatValue, 0, 20);
                        }
                        */
                    }
                }

                if (!grabbable.boolValue)
                    scalable.boolValue = uniform.boolValue = attractable.boolValue = false;
                else if (!scalable.boolValue) uniform.boolValue = false;

                EditorGUILayout.PropertyField(group);

                // ルーム専用の設定
                EditorGUILayout.LabelField("For Room", EditorStyles.boldLabel);

                // カメラマスク
                var selectedCameraMaskOption = isVisibleToCamera.boolValue ? 0 : 1;
                selectedCameraMaskOption = EditorGUILayout.Popup("Camera Mask", selectedCameraMaskOption, CameraMaskOptions);
                isVisibleToCamera.boolValue = selectedCameraMaskOption == 0; // 0: Visible, 1: Invisible
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
