using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    internal sealed class PhysicMaterialFactory : IResponsibilityForDestroyObjects
    {
        private readonly IReadOnlyDictionary<SubAssetKey, PhysicMaterial> _externalMaterials;
        private readonly Dictionary<SubAssetKey, PhysicMaterial> _runtimeGeneratedMaterials = new Dictionary<SubAssetKey, PhysicMaterial>();

        public PhysicMaterialFactory(IReadOnlyDictionary<SubAssetKey, PhysicMaterial> externalMaterials)
        {
            _externalMaterials = externalMaterials;
        }

        public void Dispose()
        {
            foreach (var kv in _runtimeGeneratedMaterials)
            {
                UnityEngine.Object.Destroy(kv.Value);
            }
            _runtimeGeneratedMaterials.Clear();
        }

        /// <summary>
        /// 与えられたパラメータに対応する PhysicMaterial を返す.
        ///
        /// 外部から与えられたアセット (例: Editor Import の SecondPass では、 FirstPass で生成したアセットを ExternalObject として受け取っている) があればそれを返す.
        /// またすでに同じパラメータで Runtime に生成済みであればそれを返す.
        /// 生成済みでなければ、生成して返す.
        /// </summary>
        public PhysicMaterial LoadPhysicMaterial(float dynamicFriction, float staticFriction, float bounciness, PhysicMaterialCombine frictionCombine, PhysicMaterialCombine bounceCombine)
        {
            var key = new SubAssetKey(
                typeof(PhysicMaterial),
                GenerateId(dynamicFriction, staticFriction, bounciness, frictionCombine, bounceCombine));

            var loadedMaterial = GetLoadedPhysicMaterial(key);
            if (loadedMaterial != null) return loadedMaterial;

            var material = new PhysicMaterial(key.Name)
            {
                dynamicFriction = dynamicFriction,
                staticFriction = staticFriction,
                bounciness = bounciness,
                frictionCombine = frictionCombine,
                bounceCombine = bounceCombine,
            };
            _runtimeGeneratedMaterials.Add(key, material);
            return material;
        }

        private PhysicMaterial GetLoadedPhysicMaterial(SubAssetKey key)
        {
            if (_externalMaterials.TryGetValue(key, out var material)) return material;
            if (_runtimeGeneratedMaterials.TryGetValue(key, out material)) return material;
            return null;
        }

        public void TransferOwnership(TakeResponsibilityForDestroyObjectFunc take)
        {
            foreach (var (k, v) in _runtimeGeneratedMaterials.ToArray())
            {
                take(k, v);
                _runtimeGeneratedMaterials.Remove(k);
            }
        }

        /// <summary>
        /// PhysicMaterial は VCI のファイル上、ただのパラメータ集合概念であり、名前も存在しない。
        /// したがってパラメータから ID を生成する。
        /// </summary>
        private static string GenerateId(float dynamicFriction, float staticFriction, float bounciness, PhysicMaterialCombine frictionCombine, PhysicMaterialCombine bounceCombine)
        {
            var code = GetHashCode(dynamicFriction, staticFriction, bounciness, frictionCombine, bounceCombine);
            return code.ToString(CultureInfo.InvariantCulture);
        }

        private static int GetHashCode(float dynamicFriction, float staticFriction, float bounciness, PhysicMaterialCombine frictionCombine, PhysicMaterialCombine bounceCombine)
        {
            unchecked
            {
                var hashCode = dynamicFriction.GetHashCode();
                hashCode = (hashCode * 397) ^ staticFriction.GetHashCode();
                hashCode = (hashCode * 397) ^ bounciness.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)frictionCombine;
                hashCode = (hashCode * 397) ^ (int)bounceCombine;
                return hashCode;
            }
        }
    }
}
