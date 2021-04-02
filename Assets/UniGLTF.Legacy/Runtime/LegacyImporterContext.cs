using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using DepthFirstScheduler;
using UniJSON;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if ((NET_4_6 || NET_STANDARD_2_0) && UNITY_2017_1_OR_NEWER)
using System.Threading.Tasks;
#endif


namespace UniGLTF.Legacy
{
    /// <summary>
    /// GLTF importer
    /// </summary>
    public class LegacyImporterContext : ImporterContext
    {
    }
}
