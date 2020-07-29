using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using VCIGLTF;

namespace VCIGLTF
{
    public class AnimationTest
    {
        [Test]
        public void RelativePathFromTest()
        {
            var nodes = new List<glTFNode>();
            for (var i = 0; i < 10; i++)
            {
                nodes.Add(new glTFNode()
                {
                    name = $"node{i}"
                });
            }

            nodes[0].children = new[] {1, 2};
            nodes[2].children = new[] {3, 4};
            nodes[4].children = new[] {5, 6, 7};

            string res = null;

            res = AnimationImporterUtil.RelativePathFrom(nodes, null, nodes[0]);
            Assert.AreEqual("node0", res);

            res = AnimationImporterUtil.RelativePathFrom(nodes, null, nodes[8]);
            Assert.AreEqual("node8", res);

            res = AnimationImporterUtil.RelativePathFrom(nodes, null, nodes[4]);
            Assert.AreEqual("node0/node2/node4", res);

            res = AnimationImporterUtil.RelativePathFrom(nodes, null, nodes[7]);
            Assert.AreEqual("node0/node2/node4/node7", res);

            res = AnimationImporterUtil.RelativePathFrom(nodes, nodes[0], nodes[4]);
            Assert.AreEqual("node2/node4", res);

            res = AnimationImporterUtil.RelativePathFrom(nodes, nodes[0], nodes[7]);
            Assert.AreEqual("node2/node4/node7", res);

            res = AnimationImporterUtil.RelativePathFrom(nodes, nodes[2], nodes[7]);
            Assert.AreEqual("node4/node7", res);

            // rootとtargetが同じ場合は空になる。
            res = AnimationImporterUtil.RelativePathFrom(nodes, nodes[4], nodes[4]);
            Assert.AreEqual("", res);

            // rootとtargetがnullの場合は空になる。
            res = AnimationImporterUtil.RelativePathFrom(nodes, null, null);
            Assert.AreEqual("", res);

            // rootよりもtargetが下層の場合は、rootを無視する。
            res = AnimationImporterUtil.RelativePathFrom(nodes, nodes[7], nodes[4]);
            Assert.AreEqual("node0/node2/node4", res);
        }
    }
}