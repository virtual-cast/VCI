using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VCI
{
    public static class VCITemplateMenu
    {

        [MenuItem("GameObject/VCI/Plain VCI", false, 0)]
        public static void CreatePlainVCI()
        {
            CreateVCIObject();
        }

        [MenuItem("GameObject/VCI/Simple VCI", false, 0)]
        public static void CreateSimpleVCI()
        {
            var vciObjectComponent = CreateVCIObject();

            var vciSubItem = GameObject.CreatePrimitive(PrimitiveType.Cube);
            vciSubItem.name = "Cube";
            vciSubItem.transform.SetParent(vciObjectComponent.transform);
            var vciSubItemObjectComponent = vciSubItem.AddComponent<VCISubItem>();
            vciSubItemObjectComponent.Grabbable = true;
        }

        private static VCIObject CreateVCIObject()
        {
            var vciObject = new GameObject("VCIObject");
            var vciObjectComponent = vciObject.AddComponent<VCIObject>();

            // Meta
            var meta = new VCIImporter.Meta
            {
                title = "[Title]",
                version = "1.0",
                author = "[Author]"
            };
            vciObjectComponent.Meta = meta;

            // Script
            var script = new VCIObject.Script
            {
                source = "print(\"Hello World!\")"
            };
            vciObjectComponent.Scripts = new List<VCIObject.Script> {script};

            return vciObjectComponent;
        }

    }
}