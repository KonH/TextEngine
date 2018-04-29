using System.IO;
using System.Linq;
using System.Collections.Generic;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands {
	class CleanCommand : ICommand {
		public string Description { get; } = 
			"'clean {target}' - clean staging directory for given target";

		public void Run(Configuration config, List<string> args) {
			var target = args.FirstOrDefault();
			CommandHelpers.EnsureTarget(config, target);
			IOTools.DeleteDirectory(Path.Combine(config.StagingDirectory, target), false);
		}
	}
}
