using System;
using System.IO;
using Microsoft.Build.Evaluation;

namespace EngineBuilder.Commands.WindowsClassic {
	class AppendCommand_WindowsClassic : ICommand {
		AppendCommand _owner;

		public string Description => _owner.Description;

		public AppendCommand_WindowsClassic(AppendCommand owner) {
			_owner = owner;
		}

		public void Run(Configuration config, CommandArguments args) {
			_owner.Run(config, args);
		}

		public void InterateWindowsClassicProject(Configuration config, string projectTargetDir) {
			var windowsConfig = config.Windows;
			var vsProjectFile = windowsConfig.EngineLibraryVsProjectFile;
			Console.WriteLine($"Modify project file: '{vsProjectFile}'");

			Project project = null;
			try {
				project = new Project(
					vsProjectFile, windowsConfig.ProjectProperties, windowsConfig.ToolsVersion
				);
			} catch {
				Console.WriteLine("Failed to load project file, try to check WindowsConfiguration " +
					"(tools version, project properties).");
				throw;
			}

			var libDirectory = config.Windows.EngineLibraryDirectory;

			IntegrateFilesToVsProject("*.h"  , "ClInclude",  project, libDirectory, projectTargetDir);
			IntegrateFilesToVsProject("*.cpp", "ClCompile",  project, libDirectory, projectTargetDir);

			project.Save();
		}

		static void IntegrateFilesToVsProject(
			string pattern, string type, Project project, string libDirectory, string projectTargetDir
		) {
			var filesToIntegrate = Directory.GetFiles(projectTargetDir, pattern, SearchOption.AllDirectories);
			foreach ( var fullPath in filesToIntegrate ) {
				var relativePath = Path.GetRelativePath(libDirectory, fullPath);
				var alreadyAdded = false;
				foreach ( var item in project.Items ) {
					if ( (item.ItemType == type) && (item.EvaluatedInclude == relativePath) ) {
						alreadyAdded = true;
						break;
					}
				}
				if ( alreadyAdded ) {
					continue;
				}
				Console.WriteLine($"Integrate file: '{relativePath}' as '{type}'");
				project.AddItem(type, relativePath);
			}
		}
	}
}
