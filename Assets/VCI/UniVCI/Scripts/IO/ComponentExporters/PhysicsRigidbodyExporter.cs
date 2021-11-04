using System;
using System.Collections.Generic;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// Rigidbody を Export できる
    /// </summary>
    public static class PhysicsRigidbodyExporter
    {
        public static glTF_VCAST_vci_rigidbody ExportRigidbodies(Transform node)
        {
            // NOTE: なぜか Rigidbody が複数存在することを想定した拡張定義になっている.
            var rigidbodies = node.GetComponents<Rigidbody>();
            if (rigidbodies.Length == 0)
            {
                return null;
            }

            var rigidbodiesExtension = new glTF_VCAST_vci_rigidbody();
            rigidbodiesExtension.rigidbodies = new List<RigidbodyJsonObject>();

            foreach (var rigidbody in rigidbodies)
            {
                rigidbodiesExtension.rigidbodies.Add(ExportRigidbody(rigidbody));
            }

            return rigidbodiesExtension;
        }

        private static RigidbodyJsonObject ExportRigidbody(Rigidbody rigidbody)
        {
            return new RigidbodyJsonObject
            {
                mass = rigidbody.mass,
                drag = rigidbody.drag,
                angularDrag = rigidbody.angularDrag,
                useGravity = rigidbody.useGravity,
                isKinematic = rigidbody.isKinematic,
                interpolate = SerializeRigidbodyInterpolation(rigidbody.interpolation),
                collisionDetection = SerializeCollisionDetectionMode(rigidbody.collisionDetectionMode),
                freezePositionX = rigidbody.constraints.HasFlag(RigidbodyConstraints.FreezePositionX),
                freezePositionY = rigidbody.constraints.HasFlag(RigidbodyConstraints.FreezePositionY),
                freezePositionZ = rigidbody.constraints.HasFlag(RigidbodyConstraints.FreezePositionZ),
                freezeRotationX = rigidbody.constraints.HasFlag(RigidbodyConstraints.FreezeRotationX),
                freezeRotationY = rigidbody.constraints.HasFlag(RigidbodyConstraints.FreezeRotationY),
                freezeRotationZ = rigidbody.constraints.HasFlag(RigidbodyConstraints.FreezeRotationZ)
            };
        }

        private static string SerializeRigidbodyInterpolation(RigidbodyInterpolation mode)
        {
            switch (mode)
            {
                case RigidbodyInterpolation.None:
                    return RigidbodyJsonObject.NoneInterpolateString;
                case RigidbodyInterpolation.Interpolate:
                    return RigidbodyJsonObject.InterpolateInterpolateString;
                case RigidbodyInterpolation.Extrapolate:
                    return RigidbodyJsonObject.ExtrapolateInterpolateString;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        private static string SerializeCollisionDetectionMode(CollisionDetectionMode mode)
        {
            switch (mode)
            {
                case CollisionDetectionMode.Discrete:
                    return RigidbodyJsonObject.DiscreteCollisionDetectionString;
                case CollisionDetectionMode.Continuous:
                    return RigidbodyJsonObject.ContinuousCollisionDetectionString;
                case CollisionDetectionMode.ContinuousDynamic:
                    return RigidbodyJsonObject.ContinuousDynamicCollisionDetectionString;
                case CollisionDetectionMode.ContinuousSpeculative:
                    return RigidbodyJsonObject.ContinuousSpeculativeCollisionDetectionString;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}