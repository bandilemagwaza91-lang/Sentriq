# Sentriq

A full-stack financial transaction monitoring and fraud detection platform.
(Sentriq is a provisional project name.)

## Overview

Sentriq is a two-part system. An Android app sends payment transactions to a
backend API. The backend checks each transaction against fraud rules, raises
alerts for suspicious activity, and the app displays the results.

## Status

Work in progress. Setup is complete and the backend foundation is under way.

- [x] Repository, Android project and ASP.NET Core API created
- [x] Backend models and DTOs
- [x] Fraud detection service and rules
- [x] API controllers
- [ ] Android screens
- [ ] App and API connected
- [ ] Database

## Tech stack

**Backend:** C#, ASP.NET Core Web API (Visual Studio)

**Android:** Kotlin, Jetpack Compose (Android Studio)

**Planned:** Room, Retrofit, Coroutines, MVVM architecture

## Planned fraud rules

- Large amount: a single transaction over R10,000
- Rapid activity: 5 or more transactions on one account within 1 minute
- Location change: a different location within 10 minutes of the previous transaction

## Repository structure

    Sentriq/
    ├── android/    Kotlin Android app
    ├── backend/    ASP.NET Core Web API
    ├── docs/       Project documentation
    ├── README.md
    └── .gitignore

## Running the project

**Backend:** open `backend/FraudOps/FraudOps.slnx` in Visual Studio and press Run.

**Android:** open the `android` folder in Android Studio, let Gradle sync
finish, then run the app on a device or emulator.

## Author

Bandile Magwaza