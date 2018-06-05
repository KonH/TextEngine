using System;
using System.IO;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands.WindowsClassic {
	class BuildCommand_Android : BuildCommand {
		
		public override void Run(Configuration config, CommandArguments args) {
			base.Run(config, args);

			var configurationName = args.Get(this, "configuration");

			RunBuildFor(
				configurationName,
				Path.Combine(Directory.GetCurrentDirectory(), config.StagingDirectory, Configuration.AndroidTarget));
			var apkName = GetApkName(configurationName);
			CopyBuildResult(config, config.Android, configurationName.ToLower(), apkName);
		}

		void RunBuildFor(string configurationName, string projectPath) {
			var gradleFile = Path.Combine(projectPath, "gradlew.bat");
			var gradleArgs = $"assemble{configurationName}";

			Console.WriteLine($"Run gradle build with args: '{gradleArgs}'");
			ProcessTools.RunProcessAndEnsureSuccess(this, "Gradle Build", gradleFile, gradleArgs, projectPath);

			Console.WriteLine("Stop gradle agent");
			ProcessTools.RunProcessAndEnsureSuccess(this, "Gradle Stop", gradleFile, "--stop", projectPath);
		}

		string GetApkName(string configurationName) {
			switch ( configurationName ) {
				case "Debug"  : return "app-debug.apk";
				case "Release": return "app-release-unsigned.apk";

				default:
					throw new InvalidCommandException(
						this, $"Invalid configuration: '{configurationName}'"
					);
			}
		}

		void CopyBuildResult
		(
			Configuration                      config,
			Configuration.AndroidConfiguration androidConfig,
			string                             configurationName,
			string                             apkName
		) {
			var srcFileName = Path.Combine(androidConfig.BuildDirectory, configurationName, apkName);
			var targetDir   = Path.Combine(config.BuildsDirectory, Configuration.AndroidTarget);
			var dstFileName = Path.Combine(targetDir, configurationName + ".apk");
			if ( !Directory.Exists(targetDir)) {
				Directory.CreateDirectory(targetDir);
			}
			File.Copy(srcFileName, dstFileName, true);
		}
	}
}
