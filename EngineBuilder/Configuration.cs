using System.IO;
using System.Collections.Generic;

namespace EngineBuilder {
	class Configuration {
		public class WindowsConfiguration {
			public string EngineLibraryDirectory     { get; }
			public string EngineLibraryVsProjectFile { get; }
			public string FrontendDirectory          { get; }
			public string FrontendVsProjectFile      { get; }
			public string ToolsVersion               { get; } = "15.0";
			public string MSBuildPath                { get; } = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe";
			public string BuildDirectory             { get; }
			public string BuildFile                  { get; } = "WindowsClassic.exe";

			public Dictionary<string, string> ProjectProperties { get; } = new Dictionary<string, string> {
				{ "VCTargetsPath", @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\VC\VCTargets" }
			};

			public WindowsConfiguration(
				string engineLibraryDirectory, string frontendDirectory, string buildDirectory
			) {
				EngineLibraryDirectory     = engineLibraryDirectory;
				EngineLibraryVsProjectFile = Path.Combine(engineLibraryDirectory, "TextEngineLibrary.vcxproj");
				FrontendDirectory          = frontendDirectory;
				FrontendVsProjectFile      = Path.Combine(frontendDirectory, "WindowsClassic.csproj");
				BuildDirectory             = buildDirectory;
			}
		}

		public const string WindowsClassicTarget = "WindowsClassic";

		public List<string> Targets { get; } = new List<string> {
			WindowsClassicTarget
		};

		public string ProjectDirectory    { get; } = "TextEngine";
		public string FoundationDirectory { get; } = "Foundation";
		public string StagingDirectory    { get; } = "Staging";
		public string BuildsDirectory     { get; } = "Builds";

		public Dictionary<string, string> ProjectTargetDirectories { get; } = new Dictionary<string, string>();

		public WindowsConfiguration Windows { get; }

		public Configuration() {
			var windowsStagingRoot     = Path.Combine(StagingDirectory, WindowsClassicTarget);
			var windowsFrontendLibrary = Path.Combine(windowsStagingRoot, "WindowsClassic");
			var windowsEngineLibrary   = Path.Combine(windowsStagingRoot, "TextEngineLibrary");
			var buildDirectory         = Path.Combine(windowsFrontendLibrary, "bin");
			ProjectTargetDirectories.Add(
				WindowsClassicTarget, 
				Path.Combine(windowsEngineLibrary, "TextEngine")
			);
			Windows = new WindowsConfiguration(windowsEngineLibrary, windowsFrontendLibrary, buildDirectory);
		}
	}
}
