#include <jni.h>
#include "TextEngine/Framework/TextEngine.Bridge.h"

static JNIEnv* _env;

void CallWriteCallback(const char* msg) {
    jclass clazz = _env->FindClass("com/textengine/android/TextEngineWrapper");
    jmethodID methodId = _env->GetStaticMethodID(clazz, "onWrite", "(Ljava/lang/String;)V");
    jstring str = _env->NewStringUTF(msg);
    _env->CallStaticVoidMethod(clazz, methodId, str);
}

WriteCallback GetWriteCallback() {
    return CallWriteCallback;
}

void CallDebugCallback(const char* msg) {
    jclass clazz = _env->FindClass("com/textengine/android/TextEngineWrapper");
    jmethodID methodId = _env->GetStaticMethodID(clazz, "onDebug", "(Ljava/lang/String;)V");
    jstring str = _env->NewStringUTF(msg);
    _env->CallStaticVoidMethod(clazz, methodId, str);
}

DebugCallback GetDebugCallback() {
    return CallDebugCallback;
}

extern "C" JNIEXPORT void
JNICALL
Java_com_textengine_android_TextEngineWrapper_init(
        JNIEnv *env,
        jobject /* this */) {
    _env = env;
    WriteCallback writeCallback = GetWriteCallback();
    DebugCallback debugCallback = GetDebugCallback();
    TextEngine_Init(writeCallback, debugCallback);
}

extern "C" JNIEXPORT void
JNICALL
Java_com_textengine_android_TextEngineWrapper_onStart(
        JNIEnv *env,
        jobject /* this */) {
    TextEngine_OnStart();
}

extern "C" JNIEXPORT void
JNICALL
Java_com_textengine_android_TextEngineWrapper_onRead(
        JNIEnv *env,
        jobject /* this */,
        jstring message) {
    const char *nativeString = env->GetStringUTFChars(message, 0);
    TextEngine_OnRead(nativeString);
    env->ReleaseStringUTFChars(message, nativeString);
}
