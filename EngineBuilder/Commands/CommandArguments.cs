using System.Linq;
using System.Collections.Generic;

namespace EngineBuilder.Commands {
	class CommandArguments {
		public class CommandArgument {
			public HashSet<string> All { get; } = new HashSet<string>();

			public override string ToString() {
				return All.FirstOrDefault();
			}

			public CommandArgument(string value) {
				All.Add(value);
			}
		}

		Configuration _config;

		Dictionary<string, CommandArgument> _args = new Dictionary<string, CommandArgument>();

		public CommandArguments(Configuration config) {
			_config = config;
		}

		CommandArgument GetCommand(ICommand command, string key) {
			if ( _args.TryGetValue(key, out var value) ) {
				return value;
			}
			throw new InvalidCommandException(command, $"'{key}' isn't specified");
		}

		public string Get(ICommand command, string key) {
			return GetCommand(command, key).ToString();
		}

		public ICollection<string> GetAll(ICommand command, string key) {
			return GetCommand(command, key).All;
		}

		public string GetTarget(ICommand command) {
			return Get(command, "target");
		}

		public void Add(string key, string value) {
			if ( _args.TryGetValue(key, out var arg) ) {
				arg.All.Add(value);
			} else {
				_args.Add(key, new CommandArgument(value));
			}
		}
	}
}
