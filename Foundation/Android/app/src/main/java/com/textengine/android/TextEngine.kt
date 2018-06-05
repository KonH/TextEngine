package com.textengine.android

import android.util.Log

fun instance() = {}

class TextEngine private constructor() {

    private object Holder { val instance = TextEngine() }

    companion object {
        val instance : TextEngine by lazy { Holder.instance }
    }

    private val wrapper = TextEngineWrapper()

    private lateinit var writeCallback : (String) -> Unit?
    private lateinit var debugCallback : (String) -> Unit?

    fun init(writeCallback : (String) -> Unit, debugCallback : (String) -> Unit) {
        this.writeCallback = writeCallback
        this.debugCallback = debugCallback
        wrapper.init()
    }

    fun write(msg : String) {
        Log.d("TextEngine", "write: '$msg''")
        writeCallback.invoke(msg)
    }

    fun onStart() {
        Log.d("TextEngine", "start")
        wrapper.onStart()
    }

    fun onRead(msg : String) {
        Log.d("TextEngine", "onRead: '$msg'")
        wrapper.onRead(msg)
    }

    fun callWriteCallback(msg : String) {
        writeCallback(msg)
    }

    fun callDebugCallback(msg : String) {
        debugCallback(msg)
    }
}