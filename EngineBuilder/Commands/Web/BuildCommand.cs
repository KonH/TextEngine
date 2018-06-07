using System;
using System.IO;
using System.Linq;
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
			buildArgs += " -s EXPORTED_FUNCTIONS=[" + string.Join(",", config.Web.ExportedFuncs.Select(s => $"\"_{s}\"")) + "]" + " -s ASSERTIONS=2";
			Console.WriteLine($"Start Emscripten build with args: '{buildArgs}'");
			ProcessTools.RunProcessAndEnsureSuccess(this, "Emscripten Build", config.Web.EmccPath, buildArgs, workDir);
		}
	}
}
