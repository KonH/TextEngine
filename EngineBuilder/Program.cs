using System;
using System.Linq;
using System.Collections.Generic;

namespace EngineBuilder {
	class Program {
		static void Main(string[] args) {
			var config = new Configuration();
			var cmdName = args.FirstOrDefault();
			if ( !string.IsNullOrEmpty(cmdName) ) {
				var cmdArgs = args.Skip(1).ToList();
				var runner = new CommandRunner(config, cmdName, cmdArgs);
				runner.Run();
			} else {
				Console.WriteLine($"No command provided!");
			}
		}
	}
}
