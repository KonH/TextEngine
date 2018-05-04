using System;
using System.IO;
using System.Collections.Generic;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands.WindowsClassic {
	class BuildCommand_WindowsClassic : BuildCommand {
		
		public override void Run(Configuration config, CommandArguments args) {
			base.Run(config, args);

			var target            = args.GetTarget(this);
			var configurationName = args.Get(this, "configuration");
			var platforms         = args.GetAll(this, "platform");

			RunLibraryBuildFor (config, configurationName, platforms);
			RunFrontendBuildFor(config, configurationName);
			CopyBuildResult    (config, target, configurationName);
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

		void RunLibraryBuildFor(Configuration config, string configurationName, ICollection<string> platforms) {
			var msBuildPath = config.Windows.MSBuildPath;

			foreach ( var platform in platforms ) {
				Console.WriteLine(
					$"Perform library project build for '{platform}' (configuration: '{configurationName}')"
				);

				var processArgs = GetLibraryProcessArgs(config, configurationName, platform);
				ProcessTools.RunProcessAndEnsureSuccess(
					this, $"Build ('{configurationName}', '{platform}')", msBuildPath, processArgs
				);
			}
		}

		void RunFrontendBuildFor(Configuration config, string configurationName) {
			var msBuildPath = config.Windows.MSBuildPath;
			Console.WriteLine(
				$"Perform frontend project build (configuration: '{configurationName}')"
			);

			var processArgs = GetFrontendProcessArgs(config, configurationName);
			ProcessTools.RunProcessAndEnsureSuccess(
				this, $"Build ('{configurationName}')", msBuildPath, processArgs
			);
		}

		void CopyBuildResult(Configuration config, string target, string configurationName) {
			var buildDir = Path.Combine(config.Windows.BuildDirectory, configurationName);
			var outputDir = Path.Combine(config.BuildsDirectory, target);
			IOTools.CopyDirectory(buildDir, outputDir);
		}
	}
}
