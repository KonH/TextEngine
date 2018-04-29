using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WindowsClassic {
	static class TextEngineNative {

		//typedef void (*WriteCallback)(const char*);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate void WriteCallback(string value);

		//typedef void (*DebugCallback)(const char*);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		delegate void DebugCallback(string value);

		public interface ITextEngine {
			//void TextEngine_Init(WriteCallback writeCallback, DebugCallback debugCallback);
			void Init(Action<string> writeCallback, Action<string> debugCallback);

			//void TextEngine_OnStart();
			void OnStart();

			//void TextEngine_OnRead(const char* msg);
			void OnRead(string str);
		}

		 static void CommonInit(
			Action<string> writeCallback, Action<string> debugCallback,
			Action<WriteCallback, DebugCallback> initializer,
			List<GCHandle> locks
		) {
			var writeCallbackDelegate = new WriteCallback(writeCallback);
			var debugCallbackDelegate = new DebugCallback(debugCallback);
			locks.Clear();
			locks.Add(GCHandle.Alloc(writeCallbackDelegate));
			locks.Add(GCHandle.Alloc(debugCallbackDelegate));
			initializer(writeCallbackDelegate, debugCallbackDelegate);
		}

		internal class TextEngine_x86 : ITextEngine {
			const string LibraryName = "TextEngineLibrary-x86";

			[DllImport(LibraryName,  CallingConvention = CallingConvention.Cdecl, EntryPoint = "TextEngine_Init")]
			static extern void InitInternal(WriteCallback writeCallback, DebugCallback debugCallback);

			[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TextEngine_OnStart")]
			static extern void OnStartInternal();

			[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TextEngine_OnRead")]
			static extern void OnReadInternal(string str);

			List<GCHandle> _locks = new List<GCHandle>();

			public void Init(Action<string> writeCallback, Action<string> debugCallback) {
				CommonInit(writeCallback, debugCallback, InitInternal, _locks);
			}

			public void OnStart() {
				OnStartInternal();
			}

			public void OnRead(string str) {
				OnReadInternal(str);
			}
		}

		internal class TextEngine_x64 : ITextEngine {
			const string LibraryName = "TextEngineLibrary-x64";

			[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TextEngine_Init")]
			static extern void InitInternal(WriteCallback writeCallback, DebugCallback debugCallback);

			[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TextEngine_OnStart")]
			static extern void OnStartInternal();

			[DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TextEngine_OnRead")]
			static extern void OnReadInternal(string str);

			List<GCHandle> _locks = new List<GCHandle>();

			public void Init(Action<string> writeCallback, Action<string> debugCallback) {
				CommonInit(writeCallback, debugCallback, InitInternal, _locks);
			}

			public void OnStart() {
				OnStartInternal();
			}

			public void OnRead(string str) {
				OnReadInternal(str);
			}
		}

		static ITextEngine _instance;
		public static ITextEngine Instance {
			get {
				if ( _instance == null ) {
					if ( IntPtr.Size == 4 ) {
						_instance = new TextEngine_x86();
					} else {
						_instance = new TextEngine_x64();
					}
				}
				return _instance;
			}
		} 
	}
}
