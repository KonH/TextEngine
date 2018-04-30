using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using EngineBuilder.Tools;
using Microsoft.Build.Evaluation;

namespace EngineBuilder.Commands {
	class AppendCommand : ICommand {
		public string Description { get; } =
			"'append {target}' - integrate project files to starging directory";

		public void Run(Configuration config, List<string> args) {
			var target = args.FirstOrDefault();
			CommandHelpers.EnsureTarget(config, target);

			var projectTargetDir = CommonCopyProjectDirectory(config, target);
			IntegrateProjectTargetSpecific(config, target, projectTargetDir);
		}

		string CommonCopyProjectDirectory(Configuration config, string target) {
			if ( config.ProjectTargetDirectories.TryGetValue(target, out var projectTargetDir) ) {
				IOTools.CopyDirectory(config.ProjectDirectory, projectTargetDir);
				return projectTargetDir;
			} else {
				throw new InvalidCommandException($"Project target directory for target '{target}' isn't specified");
			}
		}

		void IntegrateProjectTargetSpecific(Configuration config, string target, string projectTargetDir) {
			switch ( target ) {
				case Configuration.WindowsClassicTarget: {
						InterateWindowsClassicProject(config, projectTargetDir);
					}
					break;
			}
		}

		void InterateWindowsClassicProject(Configuration config, string projectTargetDir) {
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

		void IntegrateFilesToVsProject(
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
