#include "App.h"

using namespace TextEngineApp;

void App::OnStart() {
	std::cout << "App.OnStart" << std::endl;
}

void App::OnRead(std::string msg) {
	Write("You entered (from C++ backend): '" + msg + "'");
}
