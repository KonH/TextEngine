namespace EngineBuilder.Commands {
	static class CommandHelpers {
		public static void EnsureTarget(Configuration config, string target) {
			if ( string.IsNullOrEmpty(target) ) {
				throw new InvalidCommandException("Target not specified");
			}
			if ( !config.Targets.Contains(target) ) {
				throw new InvalidCommandException($"Unknown target '{target}'");
			}
		}
	}
}
