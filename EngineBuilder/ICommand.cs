using System.Collections.Generic;

namespace EngineBuilder {
	interface ICommand {
		string Description { get; }
		void Run(Configuration config, List<string> args);
	}
}
