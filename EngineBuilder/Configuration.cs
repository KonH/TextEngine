using System.Collections.Generic;

namespace EngineBuilder {
	class Configuration {
		public List<string> Targets { get; } = new List<string> {
			"WindowsClassic"
		};
		public string ProjectDirectory    { get; } = "TextEngine";
		public string FoundationDirectory { get; } = "Foundation";
		public string StagingDirectory    { get; } = "Staging";
	}
}
