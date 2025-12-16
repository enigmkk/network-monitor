#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Runtime.InteropServices;
using AOT;
namespace Astra.Network
{
    public class NetworkMonitoriOS : NetworkMonitorPlatform
    {
        public delegate void NetworkChangedDelegate(int status);

        [System.Runtime.InteropServices.DllImport("__Internal")]
        public static extern void NetworkMonitor_SetCallback(NetworkChangedDelegate callback);

        [MonoPInvokeCallback(typeof(NetworkChangedDelegate))]
        private static void OnNativeChanged(int status)
        {
            NetworkMonitor.OnChanged?.Invoke((NetworkStatus)status);
        }

        public override void Init()
        {
            NetworkChangedDelegate callback = OnNativeChanged;
            NetworkMonitor_SetCallback(callback);
        }
    }
}
#endif