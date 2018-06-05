using System;
using UnityEditor.iOS.Xcode.Custom;

namespace EngineBuilder.Commands.iOS {
	class AppendCommand_iOS : AppendCommand {
		public override void Run(Configuration config, CommandArguments args) {
			base.Run(config, args);

			IntegrateXcodeProject(config.iOS);
		}

		void IntegrateXcodeProject(Configuration.iOSConfiguration config) {
			var project = new PBXProject();
			Console.WriteLine($"Read project file from '{config.PbxProjectPath}'");
			project.ReadFromFile(config.PbxProjectPath);

			var targetGuid = project.TargetGuidByName(config.TargetName);
			Console.WriteLine($"Add files to build for target '{config.TargetName}' ('{targetGuid}')");
			var filesToAdd = GetFilesToAdd(config.AppDirectory, config.AppDirectory);
			foreach ( var file in filesToAdd ) {
				var fileGuid = project.AddFile(file, file);
				Console.WriteLine($"Add file: '{file}' ('{fileGuid}')");
				project.AddFileToBuild(targetGuid, fileGuid);
			}
			project.WriteToFile(config.PbxProjectPath);
		}
	}
}
