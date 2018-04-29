package com.textengine.android

import android.util.Log

fun instance() = {}

class TextEngine private constructor() {

    private object Holder { val instance = TextEngine() }

    companion object {
        val instance : TextEngine by lazy { Holder.instance }
    }

    private lateinit var writeCallback : (String) -> Unit?

    fun init(writeCallback : (String) -> Unit) {
        this.writeCallback = writeCallback
    }

    fun write(msg : String) {
        Log.d("TextEngine", "write: '$msg''")
        writeCallback?.invoke(msg)
    }

    fun onStart() {
        Log.d("TextEngine", "start")
    }

    fun onRead(msg : String) {
        Log.d("TextEngine", "onRead: '$msg'")
        write("You entered: '$msg'\n")
    }
}