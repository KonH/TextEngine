using System;
using System.Diagnostics;

namespace WindowsClassic {
	class TextEngine {
		public static TextEngine Instance { get; } = new TextEngine();

		public event Action<string> OnWrite = new Action<string>((_) => { });

		public void Write(string msg) {
			Debug.Write($"TextEngine.Write: '{msg}'");
			OnWrite(msg);
		}

		public void OnStart() {
			Debug.Write("TextEngine.OnStart");
		}

		public void OnRead(string msg) {
			Debug.Write($"TextEngine.OnRead: '{msg}'");
			Write($"You entered: '{msg}'\n");
		}
	}
}
