using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace EngineBuilder.Commands {
	class BuildCommand : ICommand {
		public string Description { get; } =
			"'build {target} {configuration} {platforms}' - perform build and place results in Builds directory";

		public void Run(Configuration config, List<string> args) {
			var target = args.FirstOrDefault();
			CommandHelpers.EnsureTarget(config, target);

			var configurationName = args.Skip(1).FirstOrDefault();
			if ( string.IsNullOrEmpty(configurationName) ) {
				throw new InvalidCommandException("No configuration specified");
			}

			var platforms = args.Skip(2).ToList();
			if ( platforms.Count == 0 ) {
				throw new InvalidCommandException("No platforms specified");
			}

			RunBuildFor(config, configurationName, platforms);
		}

		string GetProcessArgs(Configuration config, string configurationName, string platform) {
			var winConfig = config.Windows;
			
			var task         = "build";
			var toolsVersion = winConfig.ToolsVersion;
			var projFile     = winConfig.EngineLibraryVsProjectFile;

			var properties = "";
			foreach ( var p in winConfig.ProjectProperties ) {
				properties = AddProperty(p.Key, p.Value, properties);
			}
			properties = AddProperty("Configuration", configurationName, properties);
			properties = AddProperty("Platform", platform, properties);

			var processArgs = $"/t:{task} {properties} /toolsversion:{toolsVersion} {projFile}";
			return processArgs;
		}

		string AddProperty(string name, string value, string line) {
			return line += $"/property:{name}=\"{value}\" ";
		}

		void RunBuildFor(Configuration config, string configurationName, List<string> platforms) {
			var msBuildPath = config.Windows.MSBuildPath;

			foreach ( var platform in platforms ) {
				Console.WriteLine(
					$"Perform library project build for '{platform}' (configuration: '{configurationName}')"
				);

				var processArgs = GetProcessArgs(config, configurationName, platform);
				Console.WriteLine($"Run '{msBuildPath}' with args: '{processArgs}'");
				var proc = Process.Start(msBuildPath, processArgs);
				proc.WaitForExit();

				Console.WriteLine($"Process exited with code '{proc.ExitCode}'");
				var isSuccess = proc.ExitCode == 0;
				if ( !isSuccess ) {
					throw new InvalidCommandException("Build process failed");
				}
			}
		}
	}
}
