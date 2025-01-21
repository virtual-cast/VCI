using System.Linq;
using NUnit.Framework;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    [TestFixture]
    public sealed class VciPhysicsMigratorTests
    {
        private VCIObject _vciObject;
        private VCIObject _migrated;

        [SetUp]
        public void Setup()
        {
            _vciObject = new GameObject("Root").AddComponent<VCIObject>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_vciObject.gameObject);
            Object.DestroyImmediate(_migrated.gameObject);
        }

        [Test]
        public void MigrateSubItemsWithoutRigidbody()
        {
            // RigidbodyがないSubItemにRigidbodyが追加される
            var sub0 = AddSubItem("sub0", _vciObject.transform);
            Assert.That(sub0.GetComponent<Rigidbody>() == null, Is.True);

            // 子オブジェクトがある状態でも正しく動作する
            var sub1 = AddSubItem("sub1", _vciObject.transform);
            GameObject.CreatePrimitive(PrimitiveType.Cube).transform.SetParent(sub1.transform);
            GameObject.CreatePrimitive(PrimitiveType.Cube).transform.SetParent(sub1.transform);
            Assert.That(sub1.GetComponent<Rigidbody>() == null, Is.True);

            // 既にRigidbodyがある場合はそのまま復元される
            var sub2 = AddSubItem("sub2", _vciObject.transform);
            var sub2Rigidbody = sub2.gameObject.AddComponent<Rigidbody>();
            sub2Rigidbody.isKinematic = false;
            sub2Rigidbody.useGravity = true;

            // 結果の確認
            Migrate();

            void CheckRigidbody(string name, bool isKinematic = true, bool useGravity = false)
            {
                var rigidbodies = _migrated.transform.Find(name).GetComponents<Rigidbody>();
                Assert.That(rigidbodies.Length, Is.EqualTo(1));

                var rigidbody = rigidbodies.First();
                Assert.That(rigidbody.isKinematic, Is.EqualTo(isKinematic));
                Assert.That(rigidbody.useGravity, Is.EqualTo(useGravity));
            }

            CheckRigidbody("sub0");
            CheckRigidbody("sub1");
            CheckRigidbody("sub2", isKinematic: false, useGravity: true);
        }

        [Test]
        public void MigrateNonConvexMeshCollidersInRigidbody()
        {
            // Rigidbodyと同じ階層にMeshColliderがある場合はConvexになる
            var obj0 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj0.name = "obj0";
            obj0.transform.SetParent(_vciObject.transform);
            obj0.gameObject.AddComponent<Rigidbody>();
            obj0.gameObject.AddComponent<MeshCollider>().convex = false;

            // Rigidbodyの子にMeshColliderがある場合はConvexになる
            var obj1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj1.name = "obj1";
            obj1.transform.SetParent(_vciObject.transform);
            obj1.gameObject.AddComponent<Rigidbody>();

            var obj1Child = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj1Child.name = "obj1Child";
            obj1Child.transform.SetParent(obj1.transform);
            obj1Child.gameObject.AddComponent<MeshCollider>().convex = false;

            // Rigidbodyの孫にMeshColliderがある場合はConvexになる
            var obj2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj2.name = "obj2";
            obj2.transform.SetParent(_vciObject.transform);
            obj2.gameObject.AddComponent<Rigidbody>();

            var obj2Child = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj2Child.name = "obj2Child";
            obj2Child.transform.SetParent(obj2.transform);

            var obj2GrandChild = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj2GrandChild.name = "obj2GrandChild";
            obj2GrandChild.transform.SetParent(obj2Child.transform);
            obj2GrandChild.gameObject.AddComponent<MeshCollider>().convex = false;

            // Rigidbodyがない場合はConvexにならない
            var obj3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj3.name = "obj3";
            obj3.transform.SetParent(_vciObject.transform);
            obj3.gameObject.AddComponent<MeshCollider>().convex = false;

            // Rigidbodyの親にMeshColliderがある場合はConvexにならない
            var obj4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj4.name = "obj4";
            obj4.transform.SetParent(_vciObject.transform);
            obj4.gameObject.AddComponent<MeshCollider>().convex = false;

            var obj4Child = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj4Child.transform.SetParent(obj4.transform);
            obj4Child.gameObject.AddComponent<Rigidbody>();

            // 同じ階層に複数のMeshColliderがある場合は全てConvexになる
            var obj5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj5.name = "obj5";
            obj5.transform.SetParent(_vciObject.transform);
            obj5.gameObject.AddComponent<Rigidbody>();
            obj5.gameObject.AddComponent<MeshCollider>().convex = false;
            obj5.gameObject.AddComponent<MeshCollider>().convex = false;
            obj5.gameObject.AddComponent<MeshCollider>().convex = true;
            obj5.gameObject.AddComponent<MeshCollider>().convex = false;
            obj5.gameObject.AddComponent<MeshCollider>().convex = false;

            // 複数の階層にMeshColliderがある場合は全てConvexになる
            var obj6 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj6.name = "obj6";
            obj6.transform.SetParent(_vciObject.transform);
            obj6.gameObject.AddComponent<Rigidbody>();
            obj6.gameObject.AddComponent<MeshCollider>().convex = false;
            var obj6Child = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj6Child.name = "obj6Child";
            obj6Child.transform.SetParent(obj6.transform);
            obj6Child.gameObject.AddComponent<MeshCollider>().convex = false;
            var obj6GrandChild = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj6GrandChild.name = "obj6GrandChild";
            obj6GrandChild.transform.SetParent(obj6Child.transform);
            obj6GrandChild.gameObject.AddComponent<MeshCollider>().convex = false;

            // 結果の確認
            Migrate();

            void CheckMeshCollider(string name, bool convex = true, int numOfMeshColliders = 1)
            {
                var meshColliders = _migrated.transform.Find(name).GetComponentsInChildren<MeshCollider>();
                Assert.That(meshColliders.Length, Is.EqualTo(numOfMeshColliders));

                foreach (var meshCollider in meshColliders)
                {
                    Assert.That(meshCollider.convex, Is.EqualTo(convex));
                }
            }

            CheckMeshCollider("obj0");
            CheckMeshCollider("obj1/obj1Child");
            CheckMeshCollider("obj2/obj2Child/obj2GrandChild");
            CheckMeshCollider("obj3", convex: false);
            CheckMeshCollider("obj4", convex: false);
            CheckMeshCollider("obj5", numOfMeshColliders: 5);
            CheckMeshCollider("obj6", numOfMeshColliders: 3);
        }

        private void Migrate()
        {
            var exportingGltfData = new ExportingGltfData();
            using var exporter = new VCIExporter(exportingGltfData);
            exporter.Prepare(_vciObject.gameObject);
            exporter.Export(new DummyTextureSerializer());
            var binary = exportingGltfData.ToGlbBytes();

            // パースと同時にマイグレートされる
            using var parsed = new VciBinaryParser(binary).Parse();

            using var importer = new VCIImporter(parsed);
            _migrated = importer.Load().Root.GetComponent<VCIObject>();
        }

        private static VCISubItem AddSubItem(string name, Transform parent)
        {
            var subItem = GameObject.CreatePrimitive(PrimitiveType.Cube);
            subItem.name = name;
            subItem.transform.SetParent(parent);
            return subItem.AddComponent<VCISubItem>();
        }
    }
}
