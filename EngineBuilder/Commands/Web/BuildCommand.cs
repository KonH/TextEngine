using System;
using System.IO;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands.Web {
	class BuildCommand_Web : BuildCommand {

		string[] GetFilesToBuild(string path) {
			return Directory.GetFiles(path, "*.cpp", SearchOption.AllDirectories);
		}

		public override void Run(Configuration config, CommandArguments args) {
			base.Run(config, args);

			// Copy to builds later
			//var target      = args.GetTarget(this);
			//var outputPath  = Path.Combine(Directory.GetCurrentDirectory(), config.BuildsDirectory, target);

			var workDir = Path.Combine(Directory.GetCurrentDirectory(), config.StagingDirectory, Configuration.WebTarget);
			var buildArgs = string.Join(" ", GetFilesToBuild(workDir));
			Console.WriteLine($"Start Emscripten build with args: '{buildArgs}'");
			ProcessTools.RunProcessAndEnsureSuccess(this, "Emscripten Build", config.Web.EmccPath, buildArgs, workDir);
		}
	}
}
