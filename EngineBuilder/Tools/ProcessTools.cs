using System;
using System.IO;
using System.Diagnostics;
using EngineBuilder.Commands;

namespace EngineBuilder.Tools {
	static class ProcessTools {
		public static Process RunProcess(string fileName, string args, string workingDir = "") {
			var actualWorkingDir = string.IsNullOrEmpty(workingDir) ? Directory.GetCurrentDirectory() : workingDir;
			Console.WriteLine($"Run '{fileName}' with args: '{args}' at '{actualWorkingDir}'");
			var startInfo = new ProcessStartInfo {
				FileName         = fileName,
				Arguments        = args,
				WorkingDirectory = workingDir
			};
			var proc = Process.Start(startInfo);
			return proc;
		}

		static Process RunProcessAndWait(string fileName, string args, string workingDir = "") {
			var proc = RunProcess(fileName, args, workingDir);
			proc.WaitForExit();
			return proc;
		}

		static void EnsureProcessSuccess(ICommand command, Process proc, string operationName) {
			Console.WriteLine($"Process {operationName} exited with code '{proc.ExitCode}'");
			var isSuccess = proc.ExitCode == 0;
			if ( !isSuccess ) {
				throw new InvalidCommandException(command, $"Process {operationName} failed");
			}
		}
		
		public static void RunProcessAndEnsureSuccess(
			ICommand command, string operationName, string fileName, string args, string workingDir = ""
		) {
			using ( var proc = RunProcessAndWait(fileName, args, workingDir) ) {
				EnsureProcessSuccess(command, proc, operationName);
			}
		}
	}
}
