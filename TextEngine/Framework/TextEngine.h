#pragma once

#include <string>

#if defined (_MSC_VER )
	#define MS_DLL_EXPORT _declspec(dllexport)
#else
	#define MS_DLL_EXPORT
#endif


typedef void(*WriteCallback)(const char*);
typedef void(*DebugCallback)(const char*);

extern "C" {
	
	// Called from frontend code to initialize all required callbacks
	void MS_DLL_EXPORT TextEngine_Init(WriteCallback writeCallback, DebugCallback debugCallback);
	
	// Called from frontend code when it is ready
	void MS_DLL_EXPORT TextEngine_OnStart();
	
	// Called from frontend code on user input
	void MS_DLL_EXPORT TextEngine_OnRead(const char* msg);
}

// Called by user library, forwarded to frontend
void TextEngine_Write(const char* msg);

// Called by user library, forwarded to frontend
void TextEngine_Debug(const char* msg);

namespace TextEngine {
	class AppBase {
	public:
		virtual void OnStart() = 0;
		virtual void OnRead(std::string msg) = 0;
		void Write(std::string msg);
		void Debug(std::string msg);
	};

	class Internals {
	public:
		WriteCallback WriteCallback;
		DebugCallback DebugCallback;
	};
}