using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;

namespace EngineBuilder.Commands.Android {
	class AppendCommand_Android : AppendCommand {
		public override void Run(Configuration config, CommandArguments args) {
			base.Run(config, args);

			InterateAndroidProject(config.Android);
		}

		List<string> GetFilesToAdd(string relativeTo, string path) {
			return 
				Directory.GetFiles(path, "*.cpp", SearchOption.AllDirectories).
				Select(p => Path.GetRelativePath(relativeTo, p)).
				Select(p => p.Replace('\\', '/')).
				ToList();
		}

		void InterateAndroidProject(Configuration.AndroidConfiguration config) {
			var cmakeFile    = config.CMakeFile;
			var cmakeVersion = config.CMakeVersion;
			var filesToAdd   = GetFilesToAdd(config.AppDirectory, config.CppAppDirectory);
			var allFiles     = string.Join(Environment.NewLine, filesToAdd);			
			Console.WriteLine($"Update CMake file '{cmakeFile}'");
			Console.WriteLine($"Set version to '{cmakeVersion}'");
			Console.WriteLine($"Add files:");
			Console.WriteLine(allFiles);

			var fileContent = File.ReadAllText(cmakeFile);
			var newFileContent = fileContent.
				Replace("$CMAKE_VERSION", cmakeVersion).
				Replace("$FILES_TO_ADD", allFiles);
			Console.WriteLine("New file content:");
			Console.WriteLine(newFileContent);

			File.WriteAllText(cmakeFile, newFileContent);
		}
	}
}
