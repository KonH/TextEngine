using System;
using System.IO;

namespace EngineBuilder.Tools {
	static class IOTools {
		public static void DeleteDirectory(string path, bool throwOnNotFound) {
			if ( Directory.Exists(path) ) {
				Console.WriteLine($"Delete directory '{path}'");
				Directory.Delete(path, true);
			} else if ( throwOnNotFound ) {
				throw new InvalidOperationException($"Can't delete directory, which isn't exist: '{path}'");
			}
		}

		public static void CopyDirectory(string sourceDirectory, string targetDirectory) {
			Console.WriteLine($"Copy '{sourceDirectory}' to '{targetDirectory}'");
			var dirSource = new DirectoryInfo(sourceDirectory);
			var dirTarget = new DirectoryInfo(targetDirectory);
			CopyAllContents(sourceDirectory, targetDirectory, dirSource, dirTarget);
		}

		static void CopyAllContents(string sourceRoot, string targetRoot, DirectoryInfo source, DirectoryInfo target) {
			Directory.CreateDirectory(target.FullName);

			foreach ( var fi in source.GetFiles() ) {
				var targetPath = Path.Combine(target.FullName, fi.Name);
				Console.WriteLine($"Copying '{Path.GetRelativePath(targetRoot, targetPath)}'");
				fi.CopyTo(targetPath, true);
			}

			foreach ( var diSourceSubDir in source.GetDirectories() ) {
				var nextTargetSubDir =
					target.CreateSubdirectory(diSourceSubDir.Name);
				CopyAllContents(sourceRoot, targetRoot, diSourceSubDir, nextTargetSubDir);
			}
		}
	}
}
