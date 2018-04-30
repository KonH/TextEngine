using System.IO;
using System.Collections.Generic;

namespace EngineBuilder {
	class Configuration {
		public class WindowsConfiguration {
			public string EngineLibraryDirectory     { get; }
			public string EngineLibraryVsProjectFile { get; }
			public string ToolsVersion               { get; } = "15.0";

			public Dictionary<string, string> ProjectProperties { get; } = new Dictionary<string, string> {
				{ "VCTargetsPath", @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\VC\VCTargets" }
			};

			public WindowsConfiguration(string engineLibrary) {
				EngineLibraryDirectory     = engineLibrary;
				EngineLibraryVsProjectFile = Path.Combine(engineLibrary, "TextEngineLibrary.vcxproj");
			}
		}

		public const string WindowsClassicTarget = "WindowsClassic";

		public List<string> Targets { get; } = new List<string> {
			WindowsClassicTarget
		};

		public string ProjectDirectory    { get; } = "TextEngine";
		public string FoundationDirectory { get; } = "Foundation";
		public string StagingDirectory    { get; } = "Staging";

		public Dictionary<string, string> ProjectTargetDirectories { get; } = new Dictionary<string, string>();

		public WindowsConfiguration Windows { get; }

		public Configuration() {
			var windowsEngineLibrary = Path.Combine(StagingDirectory, WindowsClassicTarget, "TextEngineLibrary");
			ProjectTargetDirectories.Add(
				WindowsClassicTarget, 
				Path.Combine(windowsEngineLibrary, "TextEngine")
			);
			Windows = new WindowsConfiguration(windowsEngineLibrary);
		}
	}
}
