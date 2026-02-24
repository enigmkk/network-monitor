package com.astra.network;

import android.content.Context;

public class NetworkCallbackBridge
{
    private static INetworkCallback m_callback;
    public static void start(Context context, INetworkCallback callback)
    {
        m_callback = callback;
        NetworkMonitor.start(
            context.getApplicationContext(),
            status -> OnNativeChanged(status)
        );
    }

    private static void OnNativeChanged(int status)
    {
        m_callback.onNetworkChanged(status);
    }
}
