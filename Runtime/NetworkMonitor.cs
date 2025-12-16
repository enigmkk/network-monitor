using System;
using UnityEngine;

namespace Astra.Network
{
    public static class NetworkMonitor
    {
        public static Action<NetworkStatus> OnChanged;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Init()
        {
                UnityEngine.Debug.Log("NetworkMonitor Init");
                NetworkMonitorPlatform platform = null;
#if UNITY_IOS && !UNITY_EDITOR
                platform = new NetworkMonitoriOS();
#elif UNITY_ANDROID && !UNITY_EDITOR
                platform = new NetworkMonitorAndroid();
#endif
                if(null != platform)
                {
                        platform.Init();
                }
        }
    }
}
