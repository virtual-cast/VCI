using System.Collections.Generic;
using UnityEngine;

namespace VCI
{
    public interface ISpringBoneImporter
    {
        public void Load(VciData vciData, IReadOnlyList<Transform> unityNodes, GameObject unityRoot);
    }
}
