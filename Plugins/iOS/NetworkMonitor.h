#import <Foundation/Foundation.h>

@interface NetworkMonitor : NSObject
+ (instancetype)shared;
- (void)start;
@end
