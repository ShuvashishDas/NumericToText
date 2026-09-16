# NumericToText
A small application that converts a numeric currency value into its text equivalent.

This repository contains two main parts:

1. Backend
   - NumberToTextConversion.Api: ASP.NET Core Web API with a single GET endpoint that converts a numeric value to text.
   - NumberToTextConversion.Test: XUnit test project covering the conversion logic.

2. Frontend
   - Vue + TypeScript app that consumes the API and displays the converted value in the browser.

## Prerequisites
- .NET 9 SDK
- Node.js and npm

## Run the Backend
From the repository root or the Backend folder, run:

1. dotnet restore
2. dotnet build
3. dotnet run --project .\Backend\NumberToTextConversion.Api\NumberConversionApi.csproj

The API runs at:
- https://localhost:12346/

Interactive API docs are available at:
- https://localhost:12346/scalar/v1

The conversion endpoint is:
- GET /NumberConversion/{value}

Example:
- https://localhost:12346/NumberConversion/12345

## Run the Frontend
From the repository root, run:

1. cd .\Frontend
2. npm install
3. npm run dev

The application should start at:
- http://localhost:5173/

## Run the Tests
From the Backend folder, run:

- dotnet test .\NumberToTextConversion.Test\NumberToTextConversion.Test.csproj

## Notes
- The API validates input and returns a 400 Bad Request when the value is invalid.
- The frontend calls the backend using the HTTPS local API URL configured in the app.
