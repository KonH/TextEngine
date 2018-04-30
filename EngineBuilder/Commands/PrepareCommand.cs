using System.IO;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands {
	class PrepareCommand : ICommand {
		public string Description { get; } =
			"'prepare -target={target}' - copy foundation directory to staging for given target";

		public void Run(Configuration config, CommandArguments args) {
			var target = args.GetTarget(this);
			IOTools.CopyDirectory(
				Path.Combine(config.FoundationDirectory, target),
				Path.Combine(config.StagingDirectory, target)
			);
		}
	}
}
