using UnityEngine;

namespace VCI
{
    // バリデーションのうち、Runtimeで実行可能なものをまとめたクラス
    // Editorでのみ実行可能なものはEditorVciValidatorに追加する
    // TODO: すべてのバリデーションをRuntimeで実行できるようにする
    public static class RuntimeVciValidator
    {
        public static void ValidateVciRequirements(GameObject gameObject)
        {
            VciPhysicsValidator.Validate(gameObject);
        }
    }
}
