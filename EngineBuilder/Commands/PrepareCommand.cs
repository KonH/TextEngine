using System.IO;
using System.Linq;
using System.Collections.Generic;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands {
	class PrepareCommand : ICommand {
		public string Description { get; } =
			"'prepare {target}' - copy foundation directory to staging for given target";

		public void Run(Configuration config, List<string> args) {
			var target = args.FirstOrDefault();
			CommandHelpers.EnsureTarget(config, target);

			IOTools.CopyDirectory(
				Path.Combine(config.FoundationDirectory, target),
				Path.Combine(config.StagingDirectory, target)
			);
		}
	}
}
