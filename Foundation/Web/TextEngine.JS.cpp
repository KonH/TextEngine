#include "TextEngine.JS.h"
#include <emscripten.h>

void JS_WriteCallback(const char* msg) {
	EM_ASM_({
		textEngine.write(Pointer_stringify($0));
	}, msg);
}

void JS_DebugCallback(const char* msg) {
	EM_ASM_({
		textEngine.debug(Pointer_stringify($0));
	}, msg);
}

void TextEngine_JS_Init() {
	WriteCallback writeCallback = JS_WriteCallback;
	DebugCallback debugCallback = JS_DebugCallback;
	TextEngine_Init(writeCallback, debugCallback);
}
