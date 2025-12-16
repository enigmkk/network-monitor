#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine;

namespace Astra.Network
{
    public class NetworkMonitorAndroidBridge : AndroidJavaProxy
    {
        public NetworkMonitorAndroidBridge() : base("com.astra.network.INetworkCallback") { }
        // 这个方法名必须和 Java 接口方法一致
        public void onNetworkChanged(int status)
        {
            NetworkMonitor.OnChanged?.Invoke((NetworkStatus)status);
        }
    }
}
#endif
