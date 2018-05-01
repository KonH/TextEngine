﻿using System;

namespace EngineBuilder {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine($"Starting with arguments: '{string.Join("; ", args)}'");
			Console.WriteLine();

			var config = new Configuration();
			var parser = new ArgumentParser(config);
			parser.Parse(args);
			if ( parser.Commands.Count > 0 ) {
				Console.WriteLine($"Execute commands: '{string.Join("; ", parser.Commands)}'");
				Console.WriteLine();

				var runner = new CommandRunner(config, parser.Commands, parser.Arguments);
				runner.RunAll();
			} else {
				Console.WriteLine($"No command provided!");
			}
		}
	}
}
