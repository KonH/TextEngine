using System;
using System.Diagnostics;

namespace EngineBuilder.Tools {
	static class ProcessTools {
		public static Process RunProcess(string fileName, string args) {
			Console.WriteLine($"Run '{fileName}' with args: '{args}'");
			var proc = Process.Start(fileName, args);
			return proc;
		}

		public static Process RunProcessAndWait(string fileName, string args) {
			var proc = RunProcess(fileName, args);
			proc.WaitForExit();
			return proc;
		}

		public static void EnsureProcessSuccess(Process proc, string operationName) {
			Console.WriteLine($"Process {operationName} exited with code '{proc.ExitCode}'");
			var isSuccess = proc.ExitCode == 0;
			if ( !isSuccess ) {
				throw new InvalidCommandException($"Process {operationName} failed");
			}
		}
		
		public static void RunProcessAndEnsureSuccess(string operationName, string fileName, string args) {
			var proc = RunProcessAndWait(fileName, args);
			EnsureProcessSuccess(proc, operationName);
		}
	}
}
