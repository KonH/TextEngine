#pragma once

#if defined (_MSC_VER )
	#define DLL_EXPORT extern "C" _declspec(dllexport)
#elif defined ( __APPLE__ )
	#define DLL_EXPORT
#else
	#define DLL_EXPORT extern "C"
#endif


typedef void(*WriteCallback)(const char*);
typedef void(*DebugCallback)(const char*);

// Called from frontend code to initialize all required callbacks
DLL_EXPORT void TextEngine_Init(WriteCallback writeCallback, DebugCallback debugCallback);

// Called from frontend code when it is ready
DLL_EXPORT void TextEngine_OnStart();

// Called from frontend code on user input
DLL_EXPORT void TextEngine_OnRead(const char* msg);
