using System;
using System.IO;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands.MacOS {
	class BuildCommand_MacOS : BuildCommand {

		public override void Run(Configuration config, CommandArguments args) {
			base.Run(config, args);

			var projectPath       = config.MacOS.XcodeProjectPath;
			var target            = args.GetTarget(this);
			var configurationName = args.Get(this, "configuration");
			var outputPath        = Path.Combine(Directory.GetCurrentDirectory(), config.BuildsDirectory, target);

			var buildArgs = 
				$"-project {projectPath} -scheme {target} -configuration {configurationName} build " +
				$"SYMROOT=\"{outputPath}\"";
			Console.WriteLine("Start Xcode build with args: " + buildArgs);
			ProcessTools.RunProcessAndEnsureSuccess(this, "XCode Build", "xcodebuild", buildArgs);
		}
	}
}
