#import "NetworkMonitor.h"
#import <Network/Network.h>

extern void NetworkMonitor_Notify(int status);

@implementation NetworkMonitor
{
    nw_path_monitor_t _monitor;
}

+ (instancetype)shared
{
    static NetworkMonitor *instance;
    static dispatch_once_t once;
    dispatch_once(&once, ^{
        instance = [NetworkMonitor new];
    });
    return instance;
}

- (void)start
{
    if (_monitor) return;

    _monitor = nw_path_monitor_create();

    nw_path_monitor_set_update_handler(_monitor, ^(nw_path_t path) {
        int status = 0;
        if (nw_path_get_status(path) == nw_path_status_satisfied)
        {
            // 区分蜂窝 / Wi-Fi
            status = nw_path_is_expensive(path) ? 1 : 2;
        }

        // 回到主线程通知 Unity
        dispatch_async(dispatch_get_main_queue(), ^{
            NetworkMonitor_Notify(status);
        });
    });

    // 单独创建队列
    dispatch_queue_t queue = dispatch_queue_create("com.astra.network.monitor", DISPATCH_QUEUE_SERIAL);
    nw_path_monitor_set_queue(_monitor, queue);

    // iOS 16+ start 只接受 monitor
    nw_path_monitor_start(_monitor);
}
@end
