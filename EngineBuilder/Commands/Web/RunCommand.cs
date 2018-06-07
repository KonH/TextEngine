using System;
using System.IO;
using EngineBuilder.Tools;

namespace EngineBuilder.Commands.Web {
	class RunCommand_Web : RunCommand {

		public override void Run(Configuration config, CommandArguments args) {
			base.Run(config, args);

			var target   = args.GetTarget(this);
			var buildDir = Path.Combine(config.BuildsDirectory, target);
			using ( var proc = ProcessTools.RunProcess("python", "-m SimpleHTTPServer", buildDir) ) {
				Console.WriteLine("Web server started on 'localhost:8000'");
			}
		}
	}
}
