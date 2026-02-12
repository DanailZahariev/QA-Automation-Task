# QA Automation Task

This project is a Test Automation Framework built with **C#**, **NUnit**, **Playwright** (for UI testing), and **RestSharp** (for API testing). It follows the **Page Object Model (POM)** design pattern and adheres to **SOLID** principles.

## 🏗 Project Structure

- **Core**: Contains browser factory and strategy implementations for different browsers (Chrome, Firefox, WebKit).
- **Pages**: Implements the Page Object Model (POM). Contains page classes with locators and methods for interacting with UI elements.
- **Services**: Contains API service classes for interacting with RESTful APIs.
- **Tests**: Contains the actual test classes, separated into `Ui` and `Api` tests.
- **Models**: Data Transfer Objects (DTOs) used for API requests/responses and test data.
- **Config**: Configuration readers for managing application settings.
- **Utils**: Utility classes and helpers.

## 🚀 Technologies Used

- **Language**: C# (.NET 9.0)
- **Testing Framework**: NUnit
- **UI Automation**: Microsoft Playwright
- **API Automation**: RestSharp
- **Assertions**: FluentAssertions
- **Reporting**: Allure Report
- **Data Generation**: Bogus

## ⚙️ Configuration

The project uses `appsettings.json` for configuration. You can modify the following settings:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://dummyjson.com",
    "Endpoints": {
      "AddUser": "/users/add"
    },
    "TimeoutSeconds": 15
  },
  "UiSettings": {
    "BaseUrl": "https://demoqa.com/automation-practice-form",
    "Browser": "chromium", // Options: chromium, firefox, webkit
    "Headless": true,
    "SlowMo": 0
  }
}
```

## 🌍 Multi-Environment Support

The framework supports running tests against different environments (e.g., Development, Testing, Production) without code changes. This is achieved using environment-specific configuration files (`appsettings.Development.json`, `appsettings.Testing.json`).

To switch environments, set the `ASPNETCORE_ENVIRONMENT` environment variable before running tests.

### macOS / Linux
```bash
# Run against Development environment
export ASPNETCORE_ENVIRONMENT=Development
dotnet test

# Run against Testing environment
export ASPNETCORE_ENVIRONMENT=Testing
dotnet test
```

### Windows (PowerShell)
```powershell
# Run against Development environment
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet test

# Run against Testing environment
$env:ASPNETCORE_ENVIRONMENT="Testing"
dotnet test
```

If the variable is not set, it defaults to the settings in `appsettings.json`.

## 📊 Allure Report Setup

To view the test reports, you need to have the **Allure Commandline** tool installed on your machine.

### 1. Install Allure

#### **macOS** (using Homebrew)
```bash
brew install allure
```

#### **Windows** (using Scoop)
```powershell
scoop install allure
```

#### **Cross-Platform** (using NPM)
```bash
npm install -g allure-commandline
```

### 2. Verify Installation
Check if Allure is installed correctly by running:
```bash
allure --version
```

## 🏃‍♂️ How to Run Tests

### Prerequisites
- .NET SDK 9.0 or later
- Allure Commandline (see above)


### 🛠 First Time Setup (Important!)
After cloning the repo and restoring packages, you must install the Playwright browsers:

```bash
dotnet build
pwsh bin/Debug/net9.0/playwright.ps1 install
```


### Run all tests
```bash
dotnet test
```

### Run specific tests
```bash
dotnet test --filter "Category=Ui"
dotnet test --filter "Category=Api"
```

## 📈 Generating & Viewing Reports

After running the tests, the results are stored in the `allure-results` directory. To generate and view the HTML report:

1.  **Run the tests** (if you haven't already):
    ```bash
    dotnet test
    ```

2.  **Serve the report**:
    This command generates the report in a temporary directory and opens it in your default browser.
    ```bash
    allure serve allure-results
    ```

3.  **Generate a static report** (Optional):
    If you want to generate the report files for deployment or later viewing:
    ```bash
    allure generate allure-results --clean -o allure-report
    ```
    You can then open `index.html` inside the `allure-report` folder (note: some browsers restrict opening local HTML files with AJAX, so `allure serve` is recommended for local viewing).

## 🧩 Key Features

- **Cross-Browser Testing**: Supports Chromium, Firefox, and WebKit via a Strategy pattern.
- **Parallel Execution**: NUnit supports parallel test execution (configurable).
- **Screenshots on Failure**: Automatically captures screenshots when a UI test fails and attaches them to the Allure report.
- **Data-Driven Testing**: Uses `TestCaseSource` for running tests with multiple data sets.
- **Environment Flexibility**: Easily switch between Dev, Test, and Prod environments using configuration files.
