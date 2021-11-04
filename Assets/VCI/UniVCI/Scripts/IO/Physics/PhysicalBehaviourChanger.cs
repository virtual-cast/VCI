using UnityEngine;

namespace VCI
{
    public static class PhysicalBehaviourChanger
    {
        /// <summary>
        /// コライダーを有効化し、物理世界に存在させる.
        /// </summary>
        public static void EnableCollider(Collider collider)
        {
            if (collider == null) return;

            collider.enabled = true;
        }

        /// <summary>
        /// コライダーを無効化し、物理世界の存在から消す.
        /// </summary>
        public static void DisableCollider(Collider collider)
        {
            if (collider == null) return;

            collider.enabled = false;
        }

        /// <summary>
        /// Rigidbody の挙動を VCI ファイルの定義通りに動作させる.
        /// </summary>
        public static void EnableRigidbody(Rigidbody rigidbody, RigidbodySetting fileDefaultSetting)
        {
            if (rigidbody == null) return;

            // NOTE: IsKinematic は VC 側から操作される可能性が高いため、その変更を避ける.
            rigidbody.constraints = fileDefaultSetting.Constraints;
        }

        /// <summary>
        /// Rigidbody の挙動を静止させる.
        /// 物理世界の存在としては存続する.
        /// </summary>
        public static void DisableRigidbody(Rigidbody rigidbody)
        {
            if (rigidbody == null) return;

            // NOTE: IsKinematic は VC 側から操作される可能性が高いため、その変更を避ける.
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
