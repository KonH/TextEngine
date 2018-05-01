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

			public WindowsConfiguration(Configuration config) {
				var stagingRoot     = Path.Combine(config.StagingDirectory, WindowsClassicTarget);
				var frontendLibrary = Path.Combine(stagingRoot, "WindowsClassic");
				var engineLibrary   = Path.Combine(stagingRoot, "TextEngineLibrary");

				EngineLibraryVsProjectFile = Path.Combine(engineLibrary, "TextEngineLibrary.vcxproj");
				FrontendVsProjectFile      = Path.Combine(frontendLibrary, "WindowsClassic.csproj");
				BuildDirectory             = Path.Combine(frontendLibrary, "bin");

				config.ProjectTargetDirectories.Add(
					WindowsClassicTarget,
					Path.Combine(engineLibrary, "TextEngine")
				);
			}
		}

		public const string WindowsClassicTarget = "WindowsClassic";
		public const string AndroidTarget        = "Android";

		public List<string> Targets { get; } = new List<string> {
			WindowsClassicTarget,
			AndroidTarget,
		};

		public string ProjectDirectory    { get; } = "TextEngine";
		public string FoundationDirectory { get; } = "Foundation";
		public string StagingDirectory    { get; } = "Staging";
		public string BuildsDirectory     { get; } = "Builds";

		public Dictionary<string, string> ProjectTargetDirectories { get; } = new Dictionary<string, string>();

		public WindowsConfiguration Windows { get; }

		public Configuration() {
			Windows = new WindowsConfiguration(this);
		}
	}
}
