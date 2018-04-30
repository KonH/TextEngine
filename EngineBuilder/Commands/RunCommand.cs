using System.IO;
using System.Linq;
using System.Collections.Generic;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands {
	class RunCommand : ICommand {
		public string Description { get; } =
			"'run {target}' - run build from Builds directory for given target";

		public void Run(Configuration config, List<string> args) {
			var target = args.FirstOrDefault();
			CommandHelpers.EnsureTarget(config, target);

			var buildFileName = Path.Combine(config.BuildsDirectory, target, config.Windows.BuildFile);
			ProcessTools.RunProcessAndEnsureSuccess($"Run('{target}')", buildFileName, "");
		}
	}
}
