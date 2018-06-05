using System.IO;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands.WindowsClassic {
	class RunCommand_WindowsClassic : RunCommand {
		public override void Run(Configuration config, CommandArguments args) {
			base.Run(config, args);

			var target = args.GetTarget(this);
			var buildFileName = Path.Combine(config.BuildsDirectory, target, config.Windows.BuildFile);
			ProcessTools.RunProcessAndEnsureSuccess(this, $"Run('{target}')", buildFileName, "");
		}
	}
}
