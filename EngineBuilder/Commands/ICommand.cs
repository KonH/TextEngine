namespace EngineBuilder.Commands {
	interface ICommand {
		string Description { get; }
		void Run(Configuration config, CommandArguments args);
	}
}
