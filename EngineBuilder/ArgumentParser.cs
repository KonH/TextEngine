using System;
using System.Collections.Generic;
using EngineBuilder.Commands;

namespace EngineBuilder {
	class ArgumentParser {
		public List<string>     Commands  { get; }
		public CommandArguments Arguments { get; }
		
		public ArgumentParser(Configuration config) {
			Commands  = new List<string>();
			Arguments = new CommandArguments(config);
		}

		public void Parse(string[] args) {
			foreach ( var arg in args ) {
				// -arg=value
				if ( arg.StartsWith("-") ) {
					var str = arg.Substring(1);
					var parts = str.Split("=");
					if ( parts.Length == 2 ) {
						Arguments.Add(parts[0], parts[1]);
					} else {
						throw new Exception($"Invalid input: '{arg}'");
					}
				} else {
					Commands.Add(arg);
				}
			}
		}
	}
}
