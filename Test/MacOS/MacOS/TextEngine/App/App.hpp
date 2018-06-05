#pragma once

#include "../Framework/TextEngine.hpp"

namespace TextEngineApp {
	class App : public TextEngine::AppBase {
		void OnStart();
		void OnRead(std::string msg);
	};
}
