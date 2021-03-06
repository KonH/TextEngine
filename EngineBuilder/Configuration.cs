﻿using System.IO;
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
			public string ApkRunName      { get; } = "apk-runner";
			public string ApkRunCommand   { get; } = "{0}";
			public string CMakeVersion    { get; } = "3.4.1";
			public string CMakeFile       { get; }
			public string CppAppDirectory { get; }

			public AndroidConfiguration(Configuration config) {
				var stagingRoot = Path.Combine(config.StagingDirectory, AndroidTarget);
				AppDirectory    = Path.Combine(stagingRoot, "app");
				BuildDirectory  = Path.Combine(AppDirectory, "build", "outputs", "apk");
				CMakeFile       = Path.Combine(AppDirectory, "CMakeLists.txt");
				CppAppDirectory = Path.Combine(AppDirectory, "src", "main", "cpp");

				config.ProjectTargetDirectories.Add(
					AndroidTarget,
					Path.Combine(CppAppDirectory, "TextEngine")
				);
			}
		}

		public class iOSConfiguration {
			public string AppDirectory     { get; }
			public string ProjectFile      { get; } = "iOS.xcodeproj";
			public string XcodeProjectPath { get; }
			public string PbxProjectPath   { get; }
			public string TargetName       { get; } = "iOS";

			public iOSConfiguration(Configuration config) {
				var stagingRoot  = Path.Combine(config.StagingDirectory, iOSTarget);
				AppDirectory     = stagingRoot;
				XcodeProjectPath = Path.Combine(stagingRoot, ProjectFile);
				PbxProjectPath   = Path.Combine(stagingRoot, ProjectFile, "project.pbxproj");

				config.ProjectTargetDirectories.Add(
					iOSTarget,
					Path.Combine(stagingRoot, "iOS", "TextEngine")
				);
			}
		}

		public class MacOSConfiguration {
			public string AppDirectory     { get; }
			public string ProjectFile      { get; } = "MacOS.xcodeproj";
			public string XcodeProjectPath { get; }
			public string PbxProjectPath   { get; }
			public string TargetName       { get; } = "MacOS";
			public string BuildFile        { get; } = "MacOS.app";

			public MacOSConfiguration(Configuration config) {
				var stagingRoot  = Path.Combine(config.StagingDirectory, MacOSTarget);
				AppDirectory     = stagingRoot;
				XcodeProjectPath = Path.Combine(stagingRoot, ProjectFile);
				PbxProjectPath   = Path.Combine(stagingRoot, ProjectFile, "project.pbxproj");

				config.ProjectTargetDirectories.Add(
					MacOSTarget,
					Path.Combine(stagingRoot, "MacOS", "TextEngine")
				);
			}
		}

		public class WebConfiguration {
			public string   EmccPath      { get; } = @"D:\Work\Programming\Web\emsdk\emscripten\1.38.5\emcc.bat";
			public string[] ExportedFuncs { get; } = { "TextEngine_JS_Init", "TextEngine_OnStart", "TextEngine_OnRead" };

			public WebConfiguration(Configuration config) {
				var stagingRoot = Path.Combine(config.StagingDirectory, WebTarget);

				config.ProjectTargetDirectories.Add(
					WebTarget,
					Path.Combine(stagingRoot, "TextEngine")
				);
			}
		}

		public const string WindowsClassicTarget = "WindowsClassic";
		public const string AndroidTarget        = "Android";
		public const string iOSTarget            = "iOS";
		public const string MacOSTarget          = "MacOS";
		public const string WebTarget            = "Web";

		public List<string> Targets { get; } = new List<string> {
			WindowsClassicTarget,
			AndroidTarget,
			iOSTarget,
			MacOSTarget,
			WebTarget,
		};

		public string ProjectDirectory    { get; } = "TextEngine";
		public string FoundationDirectory { get; } = "Foundation";
		public string StagingDirectory    { get; } = "Staging";
		public string BuildsDirectory     { get; } = "Builds";

		public Dictionary<string, string> ProjectTargetDirectories { get; } = new Dictionary<string, string>();

		public WindowsConfiguration Windows { get; }
		public AndroidConfiguration Android { get; }
		public iOSConfiguration     iOS     { get; }
		public MacOSConfiguration   MacOS   { get; }
		public WebConfiguration     Web     { get; }

		public Configuration() {
			Windows = new WindowsConfiguration(this);
			Android = new AndroidConfiguration(this);
			iOS     = new iOSConfiguration    (this);
			MacOS   = new MacOSConfiguration  (this);
			Web     = new WebConfiguration    (this);
		}
	}
}
