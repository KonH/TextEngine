namespace EngineBuilder.Commands {
	class RunCommand : ICommand {
		public string Description { get; } =
			"'run -target={target}' - run build from Builds directory for given target";

		public virtual void Run(Configuration config, CommandArguments args) { }
	}
}
