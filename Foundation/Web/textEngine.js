class TextEngine {
	constructor(textView, textInput, submitButton, onStartFuncCallback, onReadFuncCallback) {
		submitButton.onclick = () => {
			this.onRead(textInput.value);
			textInput.value = "";
		}
		this.onStartFunc = onStartFuncCallback;
		this.onReadFunc = onReadFuncCallback;
	}

	write(msg) {
		console.log("TextEngine.write: '" + msg + "'");
		textView.innerText += msg;
	}
	
	debug(msg) {
		console.log(msg);
	}

	onStart() {
		console.log("TextEngine.onStart");
		this.onStartFunc();
	}

	onRead(msg) {
		console.log("TextEngine.onRead: '" + msg + "'")
		this.onReadFunc(msg);
	}
}

document.ready = function() {
	return new Promise(function(resolve) {
		if (document.readyState === "complete") {
			resolve();
		} else {
			document.addEventListener("DOMContentLoaded", resolve);
		}
	});
}

var loadScriptAsync = function(uri) {
	return new Promise((resolve, reject) => {
		var tag = document.createElement('script');
		tag.src = uri;
		tag.async = true;
		tag.onload = () => {
			resolve();
		};
		var firstScriptTag = document.getElementsByTagName('script')[0];
		firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
	});
};

var textEngine;

document.ready().then(() => {
	fetch('a.out.wasm').then(response =>
		response.arrayBuffer()
	).then(buffer => {
		loadScriptAsync("a.out.js").then(() => {
			Module.wasmBinary = buffer;
			Module.onRuntimeInitialized = function() {
				let textView     = document.getElementById("textView");
				let textInput    = document.getElementById("textInput");
				let submitButton = document.getElementById("submitButton");
				let init         = cwrap("TextEngine_JS_Init");
				let onStart      = cwrap("TextEngine_OnStart");
				let onRead       = cwrap("TextEngine_OnRead", "", ["string"]);
				
				textEngine = new TextEngine(textView, textInput, submitButton, onStart, onRead);
				init();
				textEngine.onStart();
			}
		});
	})
});

