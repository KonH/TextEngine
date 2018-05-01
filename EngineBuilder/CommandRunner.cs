using System;
using System.Collections.Generic;
using EngineBuilder.Commands;

namespace EngineBuilder {
	class CommandRunner {
		Dictionary<string, ICommand> _commandsMap = new Dictionary<string, ICommand> {
			{ "clean",   new CleanCommand  () },
			{ "prepare", new PrepareCommand() },
			{ "append",  new AppendCommand () },
			{ "build",   new BuildCommand()   },
			{ "run",     new RunCommand()     },
		};

		public Configuration    Config   { get; }
		public List<string>     Commands { get; }
		public CommandArguments Args     { get; }

		public CommandRunner(Configuration config, List<string> commandName, CommandArguments commandArgs) {
			Config   = config;
			Commands = commandName;
			Args     = commandArgs;
		}

		public int RunAll() {
			foreach ( var commandName in Commands ) {
				Console.WriteLine($"Starting command: '{commandName}'");
				try {
					RunCommand(commandName);
				} catch ( InvalidCommandException e ) {
					Console.WriteLine($"Command '{commandName}' failed because: '{e.Message}'.");
					if ( e.Command != null ) {
						Console.WriteLine("Check description:");
						Console.WriteLine(e.Command.Description);
						Console.WriteLine("Supported targets:");
					}
					foreach ( var t in Config.Targets ) {
						Console.WriteLine($" - {t}");
					}
					return 1;
				} catch ( Exception e ) {
					Console.WriteLine($"Command '{commandName}' failed unexpectedly because:");
					Console.WriteLine(e);
					return 1;
				}
				Console.WriteLine();
			}
			Console.WriteLine("All commands completed successfully.");
			return 0;
		}

		void RunCommand(string commandName) {
			if ( _commandsMap.TryGetValue(commandName, out var cmdToRun) ) {
				cmdToRun.Run(Config, Args);
				Console.WriteLine($"Command '{commandName}' completed successfully.");
			} else {
				throw new InvalidCommandException(null, $"Invalid command: '{commandName}'!");
			}
		}
	}
}
