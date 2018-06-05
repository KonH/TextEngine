package com.textengine.android

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {
    private val onWriteCallback: (String)->Unit = {m -> onWrite(m)}
    private val onDebugCallback: (String)->Unit = {m -> onDebug(m)}

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        TextEngine.instance.init(onWriteCallback, onDebugCallback)
        TextEngine.instance.onStart()
    }

    fun onSubmitButtonClick(view : View) {
        val text = textInput.text.toString()
        TextEngine.instance.onRead(text)
        textInput.setText("")
    }

    private fun onWrite(msg : String) {
        textView.append(msg + "\n")
    }

    private fun onDebug(msg : String) {
        Log.d("TextEngine", msg)
    }
}
