# Agent Instructions for DatingApp API Project

## Project Overview
- .NET API project using Entity Framework Core with SQLite.
- Main data model: `AppUser`, managed via `AppDbContext` in the `Data` folder.

## Key Folders and Files
- `API/Program.cs`: Configures services and middleware, registers `AppDbContext` with SQLite.
- `API/entities/AppUser.cs`: Defines the `AppUser` entity.
- `API/Data/AppDbContext.cs`: Defines the EF Core context and exposes `DbSet<AppUser>`.
- `API/appsettings.json`: Contains the SQLite connection string.

## Entity Framework Usage
- EF Core packages installed and referenced in `API.csproj`.
- Use the `dotnet-ef` tool for migrations and database updates.

## Common Commands
- Create migration: `dotnet ef migrations add <MigrationName> --project API`
- Update database: `dotnet ef database update --project API`

## Best Practices
- Keep entities in the `entities` folder.
- Keep context classes in the `Data` folder.
- Store connection strings in `appsettings.json`.
