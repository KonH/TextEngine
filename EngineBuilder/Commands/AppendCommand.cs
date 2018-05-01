using EngineBuilder.Tools;
using EngineBuilder.Commands.WindowsClassic;

namespace EngineBuilder.Commands {
	class AppendCommand : ICommand {
		public string Description { get; } =
			"'append -target={target}' - integrate project files to starging directory";

		public void Run(Configuration config, CommandArguments args) {
			var target           = args.GetTarget(this);
			var projectTargetDir = CommonCopyProjectDirectory(config, target);
			IntegrateProjectTargetSpecific(config, target, projectTargetDir);
		}

		string CommonCopyProjectDirectory(Configuration config, string target) {
			if ( config.ProjectTargetDirectories.TryGetValue(target, out var projectTargetDir) ) {
				IOTools.CopyDirectory(config.ProjectDirectory, projectTargetDir);
				return projectTargetDir;
			} else {
				throw new InvalidCommandException(
					this, $"Project target directory for target '{target}' isn't specified"
				);
			}
		}

		void IntegrateProjectTargetSpecific(Configuration config, string target, string projectTargetDir) {
			switch ( target ) {
				case Configuration.WindowsClassicTarget: {
						new AppendCommand_WindowsClassic(this).
							InterateWindowsClassicProject(config, projectTargetDir);
					}
					break;
			}
		}
	}
}
