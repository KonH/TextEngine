#include <memory>

#include "TextEngine.hpp"
#include "../App/App.hpp"

static std::unique_ptr<TextEngine::AppBase>   _appHandler(new TextEngineApp::App);
static std::unique_ptr<TextEngine::Internals> _internals (new TextEngine::Internals);

void TextEngine_Init(WriteCallback writeCallback, DebugCallback debugCallback) {
	_internals->WriteCallback = writeCallback;
	_internals->DebugCallback = debugCallback;
	_appHandler->Debug("TextEngine.Init");
}

void TextEngine_OnStart() {
	_appHandler->Debug("TextEngine.OnStart");
	_appHandler->OnStart();
}

void TextEngine_OnRead(const char* msg) {
	std::string str(msg);
	_appHandler->Debug("TextEngine.OnRead: '" + str + "'");
	_appHandler->OnRead(str);
}

void TextEngine_Write(const char* msg) {
	_appHandler->Debug("TextEngine.Write: '" + std::string(msg) + "'");
	_internals->WriteCallback(msg);
}

void TextEngine_Debug(const char* msg) {
	_internals->DebugCallback(msg);
}

namespace TextEngine {
	void AppBase::Write(std::string msg) {
		TextEngine_Write(msg.c_str());
	}

	void AppBase::Debug(std::string msg) {
		TextEngine_Debug(msg.c_str());
	}
}
