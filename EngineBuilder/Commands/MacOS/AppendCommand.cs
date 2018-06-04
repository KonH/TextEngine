using System;
using UnityEditor.iOS.Xcode.Custom;

namespace EngineBuilder.Commands.MacOS {
	class AppendCommand_MacOS : AppendCommand {
		public override void Run(Configuration config, CommandArguments args) {
			base.Run(config, args);

			IntegrateXcodeProject(config.MacOS);
		}

		void IntegrateXcodeProject(Configuration.MacOSConfiguration config) {
			var project = new PBXProject();
			Console.WriteLine($"Read project file from '{config.ProjectPath}'");
			project.ReadFromFile(config.ProjectPath);

			var targetGuid = project.TargetGuidByName(config.TargetName);
			Console.WriteLine($"Add files to build for target '{config.TargetName}' ('{targetGuid}')");
			var filesToAdd = GetFilesToAdd(config.AppDirectory, config.AppDirectory);
			foreach ( var file in filesToAdd ) {
				var fileGuid = project.AddFile(file, file);
				Console.WriteLine($"Add file: '{file}' ('{fileGuid}')");
				project.AddFileToBuild(targetGuid, fileGuid);
			}
			project.WriteToFile(config.ProjectPath);
		}
	}
}
