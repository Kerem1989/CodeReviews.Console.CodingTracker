# Coding Tracker

A console app for logging coding sessions — start time, end time, and an automatically
calculated duration — built as a follow-up to the Habit Logger project. This time the
focus is on handling dates/times correctly, using an external library (Dapper +
Spectre.Console), and applying Separation of Concerns instead of one flat `Program.cs`.

## Features

- Log a coding session by entering a start and end date/time
- View all logged sessions in a formatted table (Spectre.Console)
- Edit a session's start or end time
- Delete a session
- Duration is always derived from `EndTime - StartTime`, never entered by hand

## Tech stack

- .NET 8 / C#
- [Dapper](https://github.com/DapperLib/Dapper) for data access (no raw ADO.NET, no EF)
- [Spectre.Console](https://spectreconsole.net/) for all console output (tables, prompts, styled text)
- Microsoft.Extensions.DependencyInjection + Microsoft.Extensions.Configuration for DI and config
- SQL Server (via `Microsoft.Data.SqlClient`) as the backing store

## Project structure

The project is organized by architectural layer, with features further split into their
own folders so each operation (create/edit/delete/list/count) lives in its own file:

```
Kerem.CodingTracker/
  Domain/
    Entities/CodingSession.cs          # the CodingSession model (Id, StartTime, EndTime, Duration)
    Interfaces/ICodingSessionRepository.cs
  Infrastructure/
    Persistance/DapperDbContext.cs     # wraps the SqlConnection used by Dapper
    Repositories/CodingSessionRepository.cs
    Utils/Validator.cs                 # date format / abort / start-before-end checks
  Features/
    CreateCodingSession/
    EditCodingSession/
    DeleteCodingSession/
    FindAllCodingSession/
    CountCodingSession/
  UI/ConsoleMenu.cs                    # the main menu loop
  DependencyInjection.cs               # wires everything up via IServiceCollection
  Program.cs                           # entry point: builds config, builds DI, runs the menu
  appsettings.json                     # connection string lives here, not hardcoded

Kerem.CodingTracker.Tests/
  ValidatorTests.cs                    # unit tests for the pure validation methods
```

Each feature only depends on `ICodingSessionRepository`, never on the concrete
repository or Dapper directly, so the console/business logic stays decoupled from
the data access layer.

## Why these design choices

- **Feature folders over one big file**: each user action (create, edit, delete, list,
  count) is its own class with a single public method, injected with only the
  repository it needs. This maps directly to the Separation of Concerns requirement
  and makes it obvious where to look when a specific menu option misbehaves.
- **`ICodingSessionRepository` interface**: the features depend on the interface, not
  `CodingSessionRepository` directly, so the data access implementation could be swapped
  (e.g. for a different database or a mock in tests) without touching feature code.
- **`Validator` as a static utility class**: date-format checking, the "abort" keyword
  check, and the start-before-end check are pure functions with no console or database
  dependency, so they're cheap to unit test in isolation (see Testing below) and reused
  across `CreateCodingSession` and `EditCodingSession` instead of being duplicated.
- **Duration is never user input**: `CodingSession.Duration` is only ever set from
  `(EndTime - StartTime).TotalMinutes`, calculated in the feature classes after both
  dates have been validated.
- **Config over hardcoding**: the connection string lives in `appsettings.json` and is
  read once in `Program.cs`, then passed into `DependencyInjection.AddApplication`,
  so nothing in the data layer needs to know where the config file lives.

## Date/time format

The app only accepts dates typed in exactly this format:

```
yyyy-MM-dd HH:mm
```

Example: `2026-08-24 14:30`

Anything else (wrong separators, missing leading zeros, a date with no time, etc.) is
rejected by `Validator.ValidateDateFormat` before it's parsed, and the end date is
checked against the start date (`Validator.ValidateStartAndEndDate`) so a session can't
end before it starts.

## Getting started

1. Make sure you have the .NET 8 SDK installed.
2. Update the `ConnectionStrings:DefaultConnection` value in
   `Kerem.CodingTracker/appsettings.json` to point at a SQL Server instance you have
   access to (default is `localhost\SQLEXPRESS` with integrated security).
3. Create the `CodingSession` table on that database:

   ```sql
   CREATE TABLE CodingSession (
       Id INT IDENTITY(1,1) PRIMARY KEY,
       StartTime DATETIME NOT NULL,
       EndTime DATETIME NOT NULL,
       Duration DECIMAL(10, 2) NOT NULL
   );
   ```

4. Run the app from the `Kerem.CodingTracker` folder:

   ```
   dotnet run --project Kerem.CodingTracker
   ```

## Running the tests

```
dotnet test
```

`Kerem.CodingTracker.Tests` covers the `Validator` methods — date format validation,
the "abort" keyword check, and the start-before-end check — since these are pure
functions with no I/O and are the easiest place for a date/time bug to hide.

## Known limitations

- The `CodingSession` table isn't created automatically; it needs to exist before the
  app is run (see step 3 above).
- Numeric input (session IDs, edit menu selections) isn't guarded against non-numeric
  input yet.
- No stopwatch-based live session tracking or filtering/sorting by period — these were
  left as future improvements rather than implemented for this pass.
