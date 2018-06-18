# TextEngine

## What is it?

It is another yet "engine" development. Minimal feature set is planned.

## Motiviation

Educational purposes at the first place. I want to improve common development skills in practice on small amount of features, implemented on several platforms. Interaction between languages, common engine issues, build pipeline, cross-platform, platform-specific stuff and so on.

## Wanted MVP features

- C++ Core (backend) and user code base
- Frontend on platform-friendly languages
- Text view with scrolling
- Text input
- Toolchain:
   - EngineBuilder (C#+.NET Core)
- Platforms:
   - Web (HTML+JS)
   - MacOS (Swift)
   - Windows Classic (C#+WPF)
   - Android (Kotlin)
   - iOS (Swift)

## Frontend interface

- **Start** - called when app ready to use
- **Write(string)** - write string to text view
- **OnRead(string)** - string input callback

## TODO

- [x] Make foundation frontend apps:
   - [x] Web
   - [x] MacOS
   - [x] Windows
   - [x] Android
   - [x] iOS
- [x] User code usage?
- [x] Shared library usage:
   - [x] Debug handlers
   - [x] Web
   - [x] MacOS
   - [x] Windows
   - [x] Android
   - [x] iOS
- [ ] Build pipeline:
   - [x] Pull foundation and copy to Staging
   - [x] Append latest library and user code
   - [x] Ready to build & run
   - [x] Perform build and place it to Builds
   - [ ] Full build process
- [ ] Common issues:
   - [x] Shared Library Safety
   - [ ] Wrappers Safety
- [ ] Platform issues:
   - [ ] Mac:
      - [ ] Fix scrolling
   - [ ] iOS:
      - [ ] Fix scrolling
      - [ ] Build & run on device/simulator
   - [ ] Android:
      - [ ] Run on device
   - [ ] Web:
      - [x] Run web server
      - [ ] Properly kill python process
