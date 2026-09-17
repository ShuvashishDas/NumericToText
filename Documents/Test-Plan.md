# Numeric To Text Conversion Test Plan

## 1. Purpose

Verify that the application correctly converts non-negative numeric currency values into English text and shows clear messages for successful and failed conversions in the frontend.

## 2. System Under Test

The followings have been tested

1. **Backend service**: `NumberConversionService`
2. **API**: `GET /NumberConversion/{value}`
3. **Frontend:** Vue application in `Frontend/src/App.vue`
4. **Supported currency format:** whole dollars with optional zero-to-two decimal places for cents
5. **Lower bound:** whole-number negative values
6. **Upper bound:** whole-number input up to 18 digits, subject to the implementation's quadrillion wording

## 3. Test Strategy

### 3.1 Unit tests: conversion service

1. xUnit is used to do unit tests in project `Backend/NumberToTextConversion.Test`.
2. Currently it's only covering the only publicly exposed function `NumberConversionService.Convert` in `Backend/NumberToTextConversion.Api`
3. Function `ConvertTest` is validating some numeric values and possible outcomes.

### 3.2 Frontend and Backend tests

The current frontend has no test runner. The testing for the frontend is done by browser smoke testing and by Black box testing. Along with the unit testing the service, the Backend endpoints are also smoke tested at the browser.

### 3.3 Browser smoke tests

Smoke test was done against the locally started backend and frontend.

1. Open backend at `https://localhost:12346/NumberConversion/1870` and make sure it returns correct data.
2. Open frontend at `http://localhost:5173/`.
3. Verify the page renders without console errors.
4. Convert `12345` and confirm the uppercase API result is shown.
5. Tried an invalid value and confirm the validation message is shown.
6. Tried a decimal value such as `123.45` and confirm cents are shown.
7. Submit with both the button and Enter key.

## 4. Non-functional Checks

1. Verify `dotnet build` completes.
2. Verify `dotnet test` completes.
3. Verify `npm install` completes.
4. Verify `npm run dev` completes.

## 5. Test Data

Following variations have been used in the UI, at API and at unit testing

1. `0`, `1`, `2`, `10`, `11`, `19`, `20`, `21`, `99`
2. `100`, `101`, `105`, `110`, `115`, `999`
3. `1000`, `1001`, `1234`, `1000000`, `1000001`, `123456789012345678`
4. `0.01`, `0.10`, `0.99`, `1.01`, `12.30`, `1234.56`
5. `-1`, `abc`, `1.001`, `1.234`, empty string, whitespace-only input

Expected strings should be asserted exactly, including hyphens, spaces, `and`, dollar singular/plural, and cents pluralization.

## 6. Execution Commands

### 6.1 powershell - Backend Build, Test and Run

Run the followings from the repository root:

1. dotnet restore
2. dotnet build .\Backend\NumberConversionApi.sln
3. dotnet test .\Backend\NumberToTextConversion.Test\NumberToTextConversion.Test.csproj
4. dotnet run --project .\Backend\NumberToTextConversion.Api\NumberConversionApi.csproj

### 6.2 powershell - Frontend Build and Run

Run the followings from the repository root:

1. cd .\Frontend
2. npm install
3. npm run type-check
4. npm run build
5. npm run dev
