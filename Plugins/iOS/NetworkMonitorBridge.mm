#import "NetworkMonitor.h"

typedef void (*NetworkChangedCallback)(int status);
static NetworkChangedCallback gCallback = NULL;

extern "C"
{
    void NetworkMonitor_SetCallback(NetworkChangedCallback callback)
    {
        gCallback = callback;
        [[NetworkMonitor shared] start];
    }

    void NetworkMonitor_Notify(int status)
    {
        if (gCallback)
            gCallback(status);
    }
}
