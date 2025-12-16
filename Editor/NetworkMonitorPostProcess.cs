using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace Astra.Network.Editor
{
    public static class NetworkMonitorPostProcess
    {
        [PostProcessBuild(100)]
        public static void OnPostBuild(BuildTarget target, string path)
        {
            if (target != BuildTarget.iOS) return;

            string pbxPath = PBXProject.GetPBXProjectPath(path);
            var pbx = new PBXProject();
            pbx.ReadFromFile(pbxPath);

            string mainTarget = pbx.GetUnityMainTargetGuid();

            pbx.AddFrameworkToProject(mainTarget, "Network.framework", false);
            pbx.AddFrameworkToProject(mainTarget, "SystemConfiguration.framework", false);

            pbx.WriteToFile(pbxPath);
        }
    }
}
