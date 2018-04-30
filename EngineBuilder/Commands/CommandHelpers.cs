namespace EngineBuilder.Commands {
	static class CommandHelpers {
		public static void EnsureTarget(ICommand command, Configuration config, string target) {
			if ( string.IsNullOrEmpty(target) ) {
				throw new InvalidCommandException(command, "Target not specified");
			}
			if ( !config.Targets.Contains(target) ) {
				throw new InvalidCommandException(command, $"Unknown target '{target}'");
			}
		}
	}
}
