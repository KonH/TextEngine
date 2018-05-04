using System;
using System.IO;
using System.Collections.Generic;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands.WindowsClassic {
	class BuildCommand_Android : BuildCommand {
		
		public override void Run(Configuration config, CommandArguments args) {
			base.Run(config, args);

			RunBuildFor();
			//CopyBuildResult(config, target, configurationName);
		}

		void RunBuildFor() {
			var gradleFile = "";
			var gradleArgs = "";
			ProcessTools.RunProcessAndEnsureSuccess(this, "Build", gradleFile, gradleArgs);
		}
	}
}
