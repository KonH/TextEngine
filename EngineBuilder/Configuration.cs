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

				EngineLibraryDirectory     = Path.Combine(stagingRoot, "TextEngineLibrary");
				FrontendDirectory          = Path.Combine(stagingRoot, "WindowsClassic");
				EngineLibraryVsProjectFile = Path.Combine(EngineLibraryDirectory, "TextEngineLibrary.vcxproj");
				FrontendVsProjectFile      = Path.Combine(FrontendDirectory, "WindowsClassic.csproj");
				BuildDirectory             = Path.Combine(FrontendDirectory, "bin");

				config.ProjectTargetDirectories.Add(
					WindowsClassicTarget,
					Path.Combine(EngineLibraryDirectory, "TextEngine")
				);
			}
		}

		public class AndroidConfiguration {
			public string AppDirectory    { get; }
			public string BuildDirectory  { get; }
			public string BuildFile       { get; } = "app-debug.apk";
			public string ApkRunName      { get; } = "apk-runner";
			public string ApkRunCommand   { get; } = "{0}";
			public string CMakeVersion    { get; } = "3.4.1";
			public string CMakeFile       { get; }
			public string CppAppDirectory { get; }

			public AndroidConfiguration(Configuration config) {
				var stagingRoot = Path.Combine(config.StagingDirectory, AndroidTarget);
				AppDirectory    = Path.Combine(stagingRoot, "app");
				BuildDirectory  = Path.Combine(AppDirectory, "build", "outputs", "apk", "debug");
				CMakeFile       = Path.Combine(AppDirectory, "CMakeLists.txt");
				CppAppDirectory = Path.Combine(AppDirectory, "src", "main", "cpp");

				config.ProjectTargetDirectories.Add(
					AndroidTarget,
					Path.Combine(CppAppDirectory, "TextEngine")
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
		public AndroidConfiguration Android { get; }

		public Configuration() {
			Windows = new WindowsConfiguration(this);
			Android = new AndroidConfiguration(this);
		}
	}
}
