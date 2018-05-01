using EngineBuilder.Commands.WindowsClassic;

namespace EngineBuilder.Commands {
	class BuildCommand : ICommand {
		public string Description { get; } =
			"'build -target={target} -configuration={configuration} -platform={platform}'" +
			" - perform build and place results in Builds directory " +
			"(configuration: {Debug,Release}, [WindowsClassic] platform (multiple): {x86,x64})";

		public void Run(Configuration config, CommandArguments args) {
			var target = args.GetTarget(this);
			var configurationName = args.Get(this, "configuration");

			BuildCommandTargetSpecific(target, configurationName, config, args);
		}

		void BuildCommandTargetSpecific(
			string target, string configurationName, Configuration config, CommandArguments args
		) {
			switch ( target ) {
				case Configuration.WindowsClassicTarget: {
						new BuildCommand_WindowsClassic(this).Run(target, configurationName, config, args);
					}
					break;
			}
		}
	}
}
