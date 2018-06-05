#pragma once

#if defined (_MSC_VER )
	#define MS_DLL_EXPORT _declspec(dllexport)
#else
	#define MS_DLL_EXPORT
#endif


typedef void(*WriteCallback)(const char*);
typedef void(*DebugCallback)(const char*);

// Called from frontend code to initialize all required callbacks
extern void MS_DLL_EXPORT TextEngine_Init(WriteCallback writeCallback, DebugCallback debugCallback);
	
// Called from frontend code when it is ready
extern void MS_DLL_EXPORT TextEngine_OnStart();
	
// Called from frontend code on user input
extern void MS_DLL_EXPORT TextEngine_OnRead(const char* msg);
