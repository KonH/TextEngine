#include "App.h"

using namespace TextEngineApp;

void App::OnStart() {
}

void App::OnRead(std::string msg) {
	Write("You entered (from C++ backend): '" + msg + "'");
}
