using System;
using UnityEditor;
using UnityEngine;

namespace VCI
{
    [InitializeOnLoad]
    public sealed class VCIUnitySettingsWindow : EditorWindow
    {
        static VCIUnitySettingsWindow()
        {
            EditorApplication.update += Validate;
        }

        private void OnProjectChange()
        {
            Validate();
        }

        private static void Validate()
        {
            if (!UnitySettingsValidator.IsValidAll())
            {
                var window = GetWindow<VCIUnitySettingsWindow>(true, null, false);
                window.minSize = new Vector2(320, 300);
            }
        }

        private void OnGUI()
        {
            if (UnitySettingsValidator.IsValidAll())
            {
                GUILayout.FlexibleSpace();

                GUILayout.Label("Thank you!", new GUIStyle(EditorStyles.boldLabel)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 20,
                });

                GUILayout.FlexibleSpace();

                GUILayout.BeginVertical(GUI.skin.box);
                GUILayout.Label("設定が完了しました。このウインドウを閉じてください");
                if (GUILayout.Button("Close"))
                {
                    Close();
                }
                GUILayout.EndVertical();

                return;
            }

            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Space(5);
            GUILayout.Label("Project Settings を UniVCI 推奨環境に設定します");
            GUILayout.Space(5);
            GUILayout.EndVertical();

            GUILayout.Space(10);

            foreach (var validator in UnitySettingsValidator.Validators)
            {
                GUILayout.Label(validator.ValidationDescription);
                var oldEnabled = GUI.enabled;
                GUI.enabled = !validator.IsValid;
                if (GUILayout.Button(validator.ValidationButtonText))
                {
                    validator.OnValidate();
                }
                GUI.enabled = oldEnabled;

                GUILayout.Space(5);
            }

            GUILayout.FlexibleSpace();

            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label("よくわからない方はこのボタンを押してください");
            if (GUILayout.Button("Accept All"))
            {
                foreach (var validator in UnitySettingsValidator.Validators)
                {
                    if (!validator.IsValid)
                    {
                        validator.OnValidate();
                    }
                }
            }
            GUILayout.EndVertical();
        }
    }
}
