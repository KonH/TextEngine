#pragma once

#include "TextEngine.Bridge.h"

#include <string>

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
