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

- [ ] Make foundation frontend apps:
   - [x] Web
   - [x] MacOS
   - [x] Windows
   - [x] Android
   - [x] iOS
- [ ] Fix scrolling on iOS/Mac
