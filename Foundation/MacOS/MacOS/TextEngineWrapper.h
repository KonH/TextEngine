//
//  TextEngineWrapper.h
//  MacOS
//
//  Created by KonH on 05.06.2018.
//  Copyright © 2018 KonH. All rights reserved.
//

#ifndef TextEngineWrapper_h
#define TextEngineWrapper_h

#import <Foundation/Foundation.h>


@interface TextEngineWrapper : NSObject

-(void)performInitWithWriteCallback:(void (*)(const char *))writeCallback andDebugCallback:(void (*)(const char *))debugCallback;
-(void)onStart;
-(void)onRead:(NSString*)msg;

@end

#endif /* TextEngineWrapper_h */
