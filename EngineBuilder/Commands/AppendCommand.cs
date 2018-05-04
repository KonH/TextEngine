using EngineBuilder.Tools;

namespace EngineBuilder.Commands {
	class AppendCommand : ICommand {
		public string Description { get; } =
			"'append -target={target}' - integrate project files to starging directory";

		protected string GetProjectTargetDirectory(Configuration config, string target) {
			if ( config.ProjectTargetDirectories.TryGetValue(target, out var projectTargetDir) ) {
				return projectTargetDir;
			} else {
				throw new InvalidCommandException(
					this, $"Project target directory for target '{target}' isn't specified"
				);
			}
		}

		public virtual void Run(Configuration config, CommandArguments args) {
			var target = args.GetTarget(this);
			CommonCopyProjectDirectory(config, target);
		}

		void CommonCopyProjectDirectory(Configuration config, string target) {
			IOTools.CopyDirectory(config.ProjectDirectory, GetProjectTargetDirectory(config, target));
		}
	}
}
