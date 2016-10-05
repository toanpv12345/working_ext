// hello.cc
#include <node.h>

namespace demo {

using v8::FunctionCallbackInfo;
using v8::Isolate;
using v8::Local;
using v8::Object;
using v8::String;
using v8::Value;

void HelloWorldMethod(const FunctionCallbackInfo<Value>& args) {
  Isolate* isolate = args.GetIsolate();
  args.GetReturnValue().Set(String::NewFromUtf8(isolate, "Addon hello world"));
}

void init(Local<Object> exports) {
  NODE_SET_METHOD(exports, "hello", HelloWorldMethod);
}

NODE_MODULE(addon, init)

}  // namespace demo