# Escape Room Challenge — Web.NET

This is an ASP.NET Core Razor Pages project using C#, Entity Framework Core and SQLite.

## Required
- .NET 8 SDK
- Visual Studio 2022 with ASP.NET and web development, OR VS Code
- No SQL Server installation is required.

## VS Code
Install:
- C# Dev Kit (Microsoft)
- C# (Microsoft), if prompted

Open the folder containing `EscapeRoomWeb.csproj`.

In Terminal:
`dotnet restore`
then:
`dotnet run`

Open the localhost address printed in the terminal.

The first run creates `escape-room.db` automatically.

## Main concepts
ASP.NET Core, Razor Pages, C#, Entity Framework Core, SQLite, authentication, password hashing, session, server-side validation, LINQ, multi-room game state, timer, scoring and leaderboard.

## Where code is
- `Pages/*.cshtml` = page UI
- `Pages/*.cshtml.cs` = page logic
- `Models` = data classes
- `Data` = database and seed data
- `Services/GameService.cs` = database/game operations
- `Program.cs` = application setup

## Demo flow
Register → start challenge → solve four rooms → score/time/attempts are saved → view leaderboard.

The sample puzzle answers are in `Data/SeedData.cs` so you can change them before submission.