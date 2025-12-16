package com.astra.network;

import com.unity3d.player.UnityPlayer;

import com.astra.network.INetworkCallback;

public class NetworkCallbackBridge
{
    private static INetworkCallback m_callback;
    public static void start(INetworkCallback callback)
    {
        m_callback = callback;
        NetworkMonitor.start(
            UnityPlayer.currentActivity.getApplicationContext(),
            status -> OnNativeChanged(status)
        );
    }

    private static void OnNativeChanged(int status)
    {
        m_callback.onNetworkChanged(status);
    }
}
