using System.IO;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands.MacOS {
	class RunCommand_MacOS : RunCommand {

		public override void Run(Configuration config, CommandArguments args) {
			base.Run(config, args);

			var target            = args.GetTarget(this);
			var configurationName = args.Get(this, "configuration");
			var buildFileName = 
				Path.Combine(config.BuildsDirectory, target, configurationName, config.MacOS.BuildFile);
			ProcessTools.RunProcessAndEnsureSuccess(this, $"Run('{target}')", "open", buildFileName);
		}
	}
}
