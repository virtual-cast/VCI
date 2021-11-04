using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using VRMShaders;

namespace VCI
{
    public static class PhysicsJointImporter
    {
        // NOTE: コライダが増えてくるとかなり重いので、40 個に 1 度待てば 10ms くらいにはなる
        // FIXME あまり賢い実装ではない
        private const int AwaitIntervalCount = 40;

        public static async Task LoadAsync(VciData vciData, IReadOnlyList<Transform> unityNodes, IAwaitCaller awaitCaller)
        {
            var jointCount = 0;
            foreach (var (nodeIdx, jointExtension) in vciData.JointsNodes)
            {
                var gameObject = unityNodes[nodeIdx].gameObject;

                foreach (var joint in jointExtension.joints)
                {
                    JointJsonObject.AddJointComponent(gameObject, joint, unityNodes);
                }

                jointCount += 1;
                if (jointCount % AwaitIntervalCount == 0)
                {
                    await awaitCaller.NextFrame();
                }
            }

            await awaitCaller.NextFrame();
        }
    }
}
