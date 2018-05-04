using System;
using System.Collections.Generic;
using EngineBuilder.Commands.WindowsClassic;

namespace EngineBuilder.Commands {
	class CommandFactory {

		Dictionary<string, ICommand> _commandsMap = new Dictionary<string, ICommand> {
			{ "clean",   new CleanCommand  () },
			{ "prepare", new PrepareCommand() },
			{ "append",  new AppendCommand () },
			{ "build",   new BuildCommand  () },
			{ "run",     new RunCommand    () },
		};

		Dictionary<Type, Dictionary<string, ICommand>> _specificCommandsMap = new Dictionary<Type, Dictionary<string, ICommand>>();

		public CommandFactory() {
			AddSpecificCommand<AppendCommand>(Configuration.WindowsClassicTarget, new AppendCommand_WindowsClassic());
			AddSpecificCommand<BuildCommand> (Configuration.WindowsClassicTarget, new BuildCommand_WindowsClassic());
		}

		void AddSpecificCommand<T>(string target, ICommand cmd) {
			Dictionary<string, ICommand> cmds;
			if ( !_specificCommandsMap.TryGetValue(typeof(T), out cmds) ) {
				cmds = new Dictionary<string, ICommand>();
				_specificCommandsMap.Add(typeof(T), cmds);
			}
			cmds.Add(target, cmd);
		}

		public ICommand GetCommand(string commandName, string target) {
			if ( _commandsMap.TryGetValue(commandName, out var cmd) ) {
				var specificCmd = TryCreatePlatformSpecificCommand(cmd, target);
				if ( specificCmd != null ) {
					return specificCmd;
				}
				return cmd;
			}
			return null;
		}

		ICommand TryCreatePlatformSpecificCommand(ICommand commonCmd, string target) {
			if ( _specificCommandsMap.TryGetValue(commonCmd.GetType(), out var cmds) && cmds.TryGetValue(target, out var value) ) {
				return value;
			}
			return null;
		}
	}
}
