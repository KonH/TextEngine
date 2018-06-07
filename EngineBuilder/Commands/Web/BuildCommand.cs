using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands.Web {
	class BuildCommand_Web : BuildCommand {

		string[] GetFilesToBuild(string path) {
			return Directory.GetFiles(path, "*.cpp", SearchOption.AllDirectories);
		}

		public override void Run(Configuration config, CommandArguments args) {
			base.Run(config, args);

			var workDir = Path.Combine(Directory.GetCurrentDirectory(), config.StagingDirectory, Configuration.WebTarget);
			var buildArgs = string.Join(" ", GetFilesToBuild(workDir));
			buildArgs += " -s EXPORTED_FUNCTIONS=[" + string.Join(",", config.Web.ExportedFuncs.Select(s => $"\"_{s}\"")) + "]" + " -s ASSERTIONS=2";
			Console.WriteLine($"Start Emscripten build with args: '{buildArgs}'");
			ProcessTools.RunProcessAndEnsureSuccess(this, "Emscripten Build", config.Web.EmccPath, buildArgs, workDir);

			var outputPath = Path.Combine(config.BuildsDirectory, Configuration.WebTarget);
			Console.WriteLine($"Copy build to '{outputPath}'");
			IOTools.CopyDirectory(workDir, outputPath);

			Console.WriteLine($"Strip sources from '{outputPath}'");
			var filesToStrip = new List<string>();
			filesToStrip.AddRange(Directory.GetFiles(outputPath, "*.cpp", SearchOption.AllDirectories));
			filesToStrip.AddRange(Directory.GetFiles(outputPath, "*.h", SearchOption.AllDirectories));
			foreach ( var file in filesToStrip ) {
				Console.WriteLine($"Stripping '{file}'");
				File.Delete(file);
			}
		}
	}
}
