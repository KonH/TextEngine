#pragma once

#include <string>

typedef void(*WriteCallback)(const char*);
typedef void(*DebugCallback)(const char*);

extern "C" {
	
	// Called from frontend code to initialize all required callbacks
	void TextEngine_Init(WriteCallback writeCallback, DebugCallback debugCallback);
	
	// Called from frontend code when it is ready
	void TextEngine_OnStart();
	
	// Called from frontend code on user input
	void TextEngine_OnRead(const char* msg);
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