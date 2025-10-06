# Future Now Android App

This is the Android mobile application for the Future Now streaming platform.

## Features

- Browse music tracks with artist, album, genre, and duration information
- Browse video content with categories and descriptions
- Browse podcasts with host information and episodes
- Search and filter content by various criteria
- Simple and intuitive tabbed interface

## Technology Stack

- Java
- Android SDK
- Retrofit 2 for REST API communication
- RecyclerView for content lists
- Material Design components

## Setup Instructions

1. Open the `AndroidApp` folder in Android Studio
2. Sync Gradle files
3. Update the API base URL in `ApiClient.java`:
   - For emulator: `http://10.0.2.2:5000/` (localhost on host machine)
   - For physical device: Replace with your computer's IP address
4. Build and run the app on an emulator or physical device

## API Configuration

The app connects to the Future Now API backend. Make sure the API is running before launching the app.

Default API endpoint: `http://10.0.2.2:5000/`

To connect from a physical device, update the BASE_URL in `ApiClient.java` to your computer's IP address on the local network.

## Project Structure

```
app/
├── src/main/
│   ├── java/com/futurenow/app/
│   │   ├── MainActivity.java           - Main activity with tabs
│   │   ├── ContentDetailActivity.java  - Detail view for content
│   │   ├── models/                     - Data models
│   │   ├── api/                        - Retrofit API client
│   │   └── adapters/                   - RecyclerView adapters
│   ├── res/
│   │   ├── layout/                     - UI layouts
│   │   └── values/                     - String resources
│   └── AndroidManifest.xml
├── build.gradle
└── ...
```

## Building

To build the APK:
```bash
./gradlew assembleDebug
```

The APK will be located at: `app/build/outputs/apk/debug/app-debug.apk`
