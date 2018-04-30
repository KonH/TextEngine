using System.IO;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands {
	class RunCommand : ICommand {
		public string Description { get; } =
			"'run -target={target}' - run build from Builds directory for given target";

		public void Run(Configuration config, CommandArguments args) {
			var target = args.GetTarget(this);
			var buildFileName = Path.Combine(config.BuildsDirectory, target, config.Windows.BuildFile);
			ProcessTools.RunProcessAndEnsureSuccess(this, $"Run('{target}')", buildFileName, "");
		}
	}
}
