#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine;

namespace Astra.Network
{
    public class NetworkMonitorAndroid : NetworkMonitorPlatform
    {
        private static AndroidJavaClass _bridge;
        public override void Init()
        {
            var callbackProxy = new NetworkMonitorAndroidBridge();
            AndroidJavaClass _bridge = new AndroidJavaClass("com.astra.network.NetworkCallbackBridge");
            _bridge.CallStatic("start", callbackProxy);
        }
    }
}
#endif
