# Selenium C# Automation Framework

This project demonstrates UI automation using Selenium WebDriver with C# and NUnit.

## Tech Stack

- Selenium WebDriver
- NUnit
- .NET
- JSON (test data)

## Features
- Page Object Model
- Data-driven testing
- Screenshot on failure
- Explicit waits

## Test Scenarios

- Login test

## How to Run
### Prerequisite
- Install .NET SDK
- Install Chrome browser (for ChromeDriver)
- Ensure NuGet packages are restored

### Steps to Run
1. Clone the repository
```bash
git clone https://github.com/DarrylYau/selenium-csharp-automation-framework.git
cd selenium-csharp-automation-framework
```


2. Restore dependencies
```bash
dotnet restore
```

3. Run all tests
```bash
dotnet test
```

### Notes
- Test data is stored in TestData/loginData.json. You can add or modify users for data-driven testing.
