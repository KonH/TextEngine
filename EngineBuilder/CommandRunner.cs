using System;
using System.Collections.Generic;
using EngineBuilder.Commands;

namespace EngineBuilder {
	class CommandRunner {
		Dictionary<string, ICommand> _commandsMap = new Dictionary<string, ICommand> {
			{ "clean",   new CleanCommand  () },
			{ "prepare", new PrepareCommand() },
			{ "append",  new AppendCommand () },
		};

		public Configuration Config      { get; }
		public string        CommandName { get; }
		public List<string>  CommandArgs { get; }

		public CommandRunner(Configuration config, string commandName, List<string> commandArgs) {
			Config      = config;
			CommandName = commandName;
			CommandArgs = commandArgs;
		}

		public int Run() {
			if ( _commandsMap.TryGetValue(CommandName, out var cmdToRun) ) {
				try {
					cmdToRun.Run(Config, CommandArgs);
					Console.WriteLine($"Command '{CommandName}' completed successfully.");
					return 0;
				} catch ( InvalidCommandException e) {
					Console.WriteLine($"Command '{CommandName}' failed because: '{e.Message}'.");
					Console.WriteLine("Check description:");
					Console.WriteLine(cmdToRun.Description);
					Console.WriteLine("Supported targets:");
					foreach ( var t in Config.Targets ) {
						Console.WriteLine($" - {t}");
					}
					return 1;
				} catch ( Exception e ) {
					Console.WriteLine($"Command '{CommandName}' failed unexpectedly because:");
					Console.WriteLine(e);
					return 1;
				}
			} else {
				Console.WriteLine($"Invalid command: '{CommandName}'!");
			}
			return 1;
		}
	}
}
