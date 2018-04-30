using System.IO;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands {
	class CleanCommand : ICommand {
		public string Description { get; } = 
			"'clean -target={target}' - clean staging & build directories for given target";

		public void Run(Configuration config, CommandArguments args) {
			var target = args.GetTarget(this);
			IOTools.DeleteDirectory(Path.Combine(config.StagingDirectory, target), false);
			IOTools.DeleteDirectory(Path.Combine(config.BuildsDirectory,  target), false);
		}
	}
}
