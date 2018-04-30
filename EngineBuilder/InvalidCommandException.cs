using System;
using EngineBuilder.Commands;

namespace EngineBuilder {
	class InvalidCommandException : Exception {
		public ICommand Command { get; }

		public InvalidCommandException(ICommand command, string message):base(message) {
			Command = command;
		}
	}
}
