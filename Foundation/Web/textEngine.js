class TextEngine {
	constructor(textView, textInput, submitButton) {
		submitButton.onclick = () => {
			this.onRead(textInput.value);
			textInput.value = "";
		}
		this.onStart();
	}

	write(msg) {
		console.log("TextEngine.write: '" + msg + "'");
		textView.innerText += msg;
	}

	onStart() {
		console.log("TextEngine.onStart");
	}

	onRead(msg) {
		console.log("TextEngine.onRead: '" + msg + "'")
		this.write("You entered: '" + msg + "'\n");
	}
}

document.addEventListener("DOMContentLoaded", () => {
	let textView = document.getElementById("textView");
	let textInput = document.getElementById("textInput");
	let submitButton = document.getElementById("submitButton");
	let engine = new TextEngine(textView, textInput, submitButton);
	engine.write("Test text\n");
});