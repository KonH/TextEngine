using System;
using System.Diagnostics;

namespace WindowsClassic {
	class TextEngine {
		public static TextEngine Instance { get; } = new TextEngine();

		public event Action<string> OnWrite = new Action<string>((_) => { });

		public void Init() {
			TextEngineNative.Instance.Init(OnWrite, OnDebug);
		}

		void OnDebug(string msg) {
			Debug.WriteLine(msg);
		}

		public void OnStart() {
			Debug.Write("TextEngine.OnStart");
			TextEngineNative.Instance.OnStart();
		}

		public void OnRead(string msg) {
			Debug.Write($"TextEngine.OnRead: '{msg}'");
			TextEngineNative.Instance.OnRead(msg);
		}
	}
}
