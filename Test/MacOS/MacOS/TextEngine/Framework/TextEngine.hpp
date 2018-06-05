#pragma once

#include <string>

#if defined (_MSC_VER )
	#define MS_DLL_EXPORT _declspec(dllexport)
#else
	#define MS_DLL_EXPORT
#endif


typedef void(*WriteCallback)(const char*);
typedef void(*DebugCallback)(const char*);

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
