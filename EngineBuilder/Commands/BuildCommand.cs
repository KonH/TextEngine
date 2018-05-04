namespace EngineBuilder.Commands {
	class BuildCommand : ICommand {
		public string Description { get; } =
			"'build -target={target} -configuration={configuration} -platform={platform}'" +
			" - perform build and place results in Builds directory " +
			"(configuration: {Debug,Release}, [WindowsClassic] platform (multiple): {x86,x64})";

		public virtual void Run(Configuration config, CommandArguments args) { }
	}
}
