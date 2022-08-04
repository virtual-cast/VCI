using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VCI
{
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
            if (Scripts.Any()) Scripts.First().name = "main";
        }
    }
}
