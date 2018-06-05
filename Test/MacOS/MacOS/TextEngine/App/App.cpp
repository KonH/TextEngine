// temp
#include <iostream>

#include "App.hpp"

using namespace TextEngineApp;

void App::OnStart() {
	std::cout << "App.OnStart" << std::endl;
}

void App::OnRead(std::string msg) {
	std::cout << "App.OnRead: '" << msg  << "'" << std::endl;
	Write("You entered (from C++ backend): '" + msg + "'");
}
