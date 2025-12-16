package com.astra.network;

import android.content.Context;
import android.net.*;

public class NetworkMonitor
{
    public interface Callback
    {
        void onChanged(int status);
    }

    private static Callback callback;
    private static ConnectivityManager.NetworkCallback networkCallback;

    public static void start(Context context, Callback cb)
    {
        callback = cb;

        ConnectivityManager cm =
            (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);

        if (networkCallback != null) return;

        networkCallback = new ConnectivityManager.NetworkCallback()
        {
            @Override
            public void onAvailable(Network network)
            {
                notifyStatus(cm);
            }

            @Override
            public void onLost(Network network)
            {
                notifyStatus(cm);
            }
        };

        cm.registerDefaultNetworkCallback(networkCallback);
        notifyStatus(cm);
    }

    private static void notifyStatus(ConnectivityManager cm)
    {
        Network network = cm.getActiveNetwork();
        if (network == null)
        {
            callback.onChanged(0);
            return;
        }

        NetworkCapabilities caps = cm.getNetworkCapabilities(network);
        if (caps == null)
        {
            callback.onChanged(0);
            return;
        }

        if (caps.hasTransport(NetworkCapabilities.TRANSPORT_WIFI))
            callback.onChanged(2);
        else if (caps.hasTransport(NetworkCapabilities.TRANSPORT_CELLULAR))
            callback.onChanged(1);
        else
            callback.onChanged(0);
    }
}
