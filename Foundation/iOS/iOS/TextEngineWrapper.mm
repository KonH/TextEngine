//
//  TextEngineWrapper.m
//  MacOS
//
//  Created by KonH on 05.06.2018.
//  Copyright © 2018 KonH. All rights reserved.
//

#import "TextEngineWrapper.h"
#include "TextEngine/Framework/TextEngine.Bridge.h"

@implementation TextEngineWrapper

-(void)performInitWithWriteCallback:(void (*)(const char *))writeCallback andDebugCallback:(void (*)(const char *))debugCallback
{
	TextEngine_Init(writeCallback, debugCallback);
}

-(void)onStart
{
	TextEngine_OnStart();
}

-(void)onRead:(NSString *)msg
{
	TextEngine_OnRead([msg UTF8String]);
}

@end
