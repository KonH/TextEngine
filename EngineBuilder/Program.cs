using System;

namespace EngineBuilder {
	class Program {
		static void Main(string[] args) {
			var config = new Configuration();
			var parser = new ArgumentParser(config);
			parser.Parse(args);
			if ( parser.Commands.Count > 0 ) {
				var runner = new CommandRunner(config, parser.Commands, parser.Arguments);
				runner.RunAll();
			} else {
				Console.WriteLine($"No command provided!");
			}
		}
	}
}
