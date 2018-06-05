package com.textengine.android

class TextEngineWrapper {
    companion object {

        // Used to load the 'native-lib' library on application startup.
        init {
            System.loadLibrary("native-lib")
        }

        @JvmStatic
        fun onWrite(msg: String) {
            TextEngine.instance.callWriteCallback(msg)
        }

        @JvmStatic
        fun onDebug(msg: String) {
            TextEngine.instance.callDebugCallback(msg)
        }

        @JvmStatic
        fun onWrite() {
            TextEngine.instance.callWriteCallback("EMPTY")
        }

        @JvmStatic
        fun onDebug() {
            TextEngine.instance.callDebugCallback("EMPTY")
        }
    }

    external fun init()
    external fun onStart()
    external fun onRead(msg : String)
}