using System;

namespace EngineBuilder {
	class InvalidCommandException : Exception {
		public InvalidCommandException(string message):base(message) { }
	}
}
