#pragma once

#if defined (_MSC_VER )
	#define DLL_EXPORT extern "C" _declspec(dllexport)
#elif defined ( __clang__ )
	#define DLL_EXPORT
#else
	#define DLL_EXPORT extern "C"
#endif


typedef void(*WriteCallback)(const char*);
typedef void(*DebugCallback)(const char*);

// Called from frontend code to initialize all required callbacks
//extern void MS_DLL_EXPORT TextEngine_Init(WriteCallback writeCallback, DebugCallback debugCallback);
void DLL_EXPORT TextEngine_Init(WriteCallback writeCallback, DebugCallback debugCallback);

// Called from frontend code when it is ready
//extern void MS_DLL_EXPORT TextEngine_OnStart();
void DLL_EXPORT TextEngine_OnStart();
	
// Called from frontend code on user input
//extern void MS_DLL_EXPORT TextEngine_OnRead(const char* msg);
void DLL_EXPORT TextEngine_OnRead(const char* msg);
