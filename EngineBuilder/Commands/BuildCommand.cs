using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using EngineBuilder.Tools;

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

			RunLibraryBuildFor(config, configurationName, platforms);
			RunFrontendBuildFor(config, configurationName);

			CopyBuildResult(config, target, configurationName);
		}

		string GetCommonProperties(Configuration.WindowsConfiguration winConfig, string configurationName) {
			var properties = "";
			foreach ( var p in winConfig.ProjectProperties ) {
				properties = AddProperty(p.Key, p.Value, properties);
			}
			properties = AddProperty("Configuration", configurationName, properties);
			return properties;
		}

		string GetProcessArgs(Configuration config, string properties, string projectFile) {
			var winConfig    = config.Windows;
			var task         = "build";
			var toolsVersion = winConfig.ToolsVersion;
			var processArgs  = $"/t:{task} {properties} /toolsversion:{toolsVersion} {projectFile}";
			return processArgs;
		}

		string GetLibraryProcessArgs(Configuration config, string configurationName, string platform) {
			var winConfig   = config.Windows;
			var properties  = GetCommonProperties(winConfig, configurationName);
			properties      = AddProperty("Platform", platform, properties);
			var projFile    = winConfig.EngineLibraryVsProjectFile;
			var processArgs = GetProcessArgs(config, properties, projFile);
			return processArgs;
		}

		string GetFrontendProcessArgs(Configuration config, string configurationName) {
			var winConfig   = config.Windows;
			var properties  = GetCommonProperties(winConfig, configurationName);
			var projFile    = winConfig.FrontendVsProjectFile;
			var processArgs = GetProcessArgs(config, properties, projFile);
			return processArgs;
		}

		string AddProperty(string name, string value, string line) {
			return line += $"/property:{name}=\"{value}\" ";
		}

		void RunLibraryBuildFor(Configuration config, string configurationName, List<string> platforms) {
			var msBuildPath = config.Windows.MSBuildPath;

			foreach ( var platform in platforms ) {
				Console.WriteLine(
					$"Perform library project build for '{platform}' (configuration: '{configurationName}')"
				);

				var processArgs = GetLibraryProcessArgs(config, configurationName, platform);
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

		void RunFrontendBuildFor(Configuration config, string configurationName) {
			var msBuildPath = config.Windows.MSBuildPath;
			Console.WriteLine(
				$"Perform frontend project build (configuration: '{configurationName}')"
			);

			var processArgs = GetFrontendProcessArgs(config, configurationName);
			Console.WriteLine($"Run '{msBuildPath}' with args: '{processArgs}'");
			var proc = Process.Start(msBuildPath, processArgs);
			proc.WaitForExit();

			Console.WriteLine($"Process exited with code '{proc.ExitCode}'");
			var isSuccess = proc.ExitCode == 0;
			if ( !isSuccess ) {
				throw new InvalidCommandException("Build process failed");
			}
		}

		void CopyBuildResult(Configuration config, string target, string configurationName) {
			var buildDir = Path.Combine(config.Windows.BuildDirectory, configurationName);
			var outputDir = Path.Combine(config.BuildsDirectory, target);
			IOTools.CopyDirectory(buildDir, outputDir);
		}
	}
}
