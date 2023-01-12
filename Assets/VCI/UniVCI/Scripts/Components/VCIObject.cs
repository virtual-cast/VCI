using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace VCI
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public sealed class VCIObject : MonoBehaviour
    {
        [SerializeField]
        public VCIMeta Meta;

        [SerializeField]
        public List<VciScript> Scripts = new List<VciScript>();

        private void Reset()
        {
            Meta.title = name;
        }

        private void OnValidate()
        {
            if (Scripts.Any()) { Scripts.First().name = "main"; }
        }

        [Conditional("UNITY_EDITOR")]
        private void Awake()
        {
            // NOTE: VCI コンテンツ作成時、 SubItem が先に存在し、後から VCIObject が付与された場合に
            //       Key が初期化されない場合があるため、 VCIObject が生まれたタイミングで一度 SubItem の Key を生成するようにする
            var subItems = GetComponentsInChildren<VCISubItem>(true)
                .Where(subItem => subItem.Key == 0);
            foreach (var subItem in subItems)
            {
                subItem.InitializeKey();
            }
        }

        /// <summary>
        /// 現状どの SubItem でも利用されていないユニークな key を生成して返す
        /// </summary>
        public int GenerateSubItemKey()
        {
            // NOTE: 現在の UnixTime 秒を key とする
            var newKey = unchecked((int)DateTimeOffset.Now.ToUnixTimeSeconds());

            // NOTE: 既に利用されている key だったら 1 ずつ increment して空いている key を探す
            var existsKeys = GetComponentsInChildren<VCISubItem>(true)
                .Select(subItem => subItem.Key)
                .ToArray();
            while (newKey == 0 || existsKeys.Contains(newKey))
            {
                unchecked { ++newKey; }
            }

            return newKey;
        }

        /// <summary>
        /// key が複数の SubItem で使われているかどうか
        /// </summary>
        public bool IsDuplicatedSubItemKey(int key)
        {
            var count = GetComponentsInChildren<VCISubItem>(true).Count(subItem => subItem.Key == key);
            return count >= 2;
        }
    }
}
