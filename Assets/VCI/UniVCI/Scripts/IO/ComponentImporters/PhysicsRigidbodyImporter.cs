using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniGLTF;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public static class PhysicsRigidbodyImporter
    {
        // NOTE: コライダが増えてくるとかなり重いので、40 個に 1 度待てば 10ms くらいにはなる
        // FIXME あまり賢い実装ではない
        private const int AwaitIntervalCount = 40;

        /// <summary>
        /// NOTE: ロード時点では、物理演算は無効化されている必要がある.
        /// </summary>
        public static async Task<Dictionary<Rigidbody, RigidbodySetting>> LoadAsync(VciData vciData, IReadOnlyList<Transform> unityNodes, IAwaitCaller awaitCaller)
        {
            var rigidBodySettings = new Dictionary<Rigidbody, RigidbodySetting>();
            var rigidbodyNodeIndices = new HashSet<int>();

            var rigidbodyCount = 0;
            foreach (var (nodeIdx, rigidbodyExtension) in vciData.RigidbodyNodes)
            {
                rigidbodyNodeIndices.Add(nodeIdx);
                var gameObject = unityNodes[nodeIdx].gameObject;
                foreach (var rigidbodyJsonObject in rigidbodyExtension.rigidbodies)
                {
                    var rb = LoadRigidbody(gameObject, rigidbodyJsonObject);
                    rigidBodySettings.Add(rb, new RigidbodySetting(rb));

                    // NOTE: ロード中に Rigidbody が動くべきではない. ロード終了後に EnablePhysicalBehaviour で有効になる.
                    PhysicalBehaviourChanger.DisableRigidbody(rb);
                }

                rigidbodyCount += 1;
                if (rigidbodyCount % AwaitIntervalCount == 0)
                {
                    await awaitCaller.NextFrame();
                }
            }

            foreach (var (nodeIdx, subItemExtension) in vciData.SubItemNodes)
            {
                if (rigidbodyNodeIndices.Contains(nodeIdx)) continue;

                // NOTE: SubItem 拡張を持つが、Rigidbody 拡張を持たない、過去の VCI に対応する.
                // TODO: 最古 public な UniVCI v0.15 でもそのような仕様はないため、必要がない処理の可能性が高い. 消したい.
                var gameObject = unityNodes[nodeIdx].gameObject;
                var rb = gameObject.GetOrAddComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;
                rigidBodySettings.Add(rb, new RigidbodySetting(rb));

                // NOTE: ロード中に Rigidbody が動くべきではない. ロード終了後に EnablePhysicalBehaviour で有効になる.
                PhysicalBehaviourChanger.DisableRigidbody(rb);

                rigidbodyCount += 1;
                if (rigidbodyCount % AwaitIntervalCount == 0)
                {
                    await awaitCaller.NextFrame();
                }
            }

            await awaitCaller.NextFrame();

            return rigidBodySettings;
        }

        private static Rigidbody LoadRigidbody(GameObject target, RigidbodyJsonObject rigidbodyJsonObject)
        {
            var result = target.GetOrAddComponent<Rigidbody>();
            result.mass = rigidbodyJsonObject.mass;
            result.drag = rigidbodyJsonObject.drag;
            result.angularDrag = rigidbodyJsonObject.angularDrag;
            result.useGravity = rigidbodyJsonObject.useGravity;
            result.isKinematic = rigidbodyJsonObject.isKinematic;
            result.interpolation = DeserializeRigidbodyInterpolation(rigidbodyJsonObject.interpolate);
            result.collisionDetectionMode = DeserializeCollisionDetectionMode(rigidbodyJsonObject.collisionDetection);
            result.constraints = DeserializeRigidbodyConstraints(rigidbodyJsonObject);
            return result;
        }

        private static RigidbodyConstraints DeserializeRigidbodyConstraints(RigidbodyJsonObject rigidbodyJsonObject)
        {
            return
                (rigidbodyJsonObject.freezePositionX ? RigidbodyConstraints.FreezePositionX : 0) |
                (rigidbodyJsonObject.freezePositionY ? RigidbodyConstraints.FreezePositionY : 0) |
                (rigidbodyJsonObject.freezePositionZ ? RigidbodyConstraints.FreezePositionZ : 0) |
                (rigidbodyJsonObject.freezeRotationX ? RigidbodyConstraints.FreezeRotationX : 0) |
                (rigidbodyJsonObject.freezeRotationY ? RigidbodyConstraints.FreezeRotationY : 0) |
                (rigidbodyJsonObject.freezeRotationZ ? RigidbodyConstraints.FreezeRotationZ : 0)
                ;
        }

        private static RigidbodyInterpolation DeserializeRigidbodyInterpolation(string jsonString)
        {
            switch (jsonString)
            {
                case RigidbodyJsonObject.NoneInterpolateString:
                    return RigidbodyInterpolation.None;
                case RigidbodyJsonObject.InterpolateInterpolateString:
                    return RigidbodyInterpolation.Interpolate;
                case RigidbodyJsonObject.ExtrapolateInterpolateString:
                    return RigidbodyInterpolation.Extrapolate;
                default: // NOTE: 不明値は Interpolate としてロードする（なぜ？）
                    return RigidbodyInterpolation.Interpolate;
            }
        }

        private static CollisionDetectionMode DeserializeCollisionDetectionMode(string jsonString)
        {
            switch (jsonString)
            {
                case RigidbodyJsonObject.DiscreteCollisionDetectionString:
                    return CollisionDetectionMode.Discrete;
                case RigidbodyJsonObject.ContinuousCollisionDetectionString:
                    return CollisionDetectionMode.Continuous;
                case RigidbodyJsonObject.ContinuousDynamicCollisionDetectionString:
                    return CollisionDetectionMode.ContinuousDynamic;
                case RigidbodyJsonObject.ContinuousSpeculativeCollisionDetectionString:
                    return CollisionDetectionMode.ContinuousSpeculative;
                default: // 未定義な値は、デフォルト値でロードする.
                    return CollisionDetectionMode.Discrete;
            }
        }
    }
}