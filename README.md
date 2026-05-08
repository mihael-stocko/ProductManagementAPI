# Product Management API

A .NET 9 Web API for managing products and categories.

## Tech Stack
- **Framework:** ASP.NET Core 9 (Web API)
- **Database:** SQLite with EF Core 9
- **Architecture:** Layered Architecture (Service & WebAPI projects)
- **Testing:** xUnit with In-Memory Database

## Features
- Full CRUD for Products and Categories.
- Advanced Filtering, Sorting, and Paging for both resources.
- Global Exception Handling Middleware.
- Swagger/OpenAPI documentation.
- Seed data for immediate testing.

## How to Run
1. Navigate to `Project.WebAPI`.
2. Run `dotnet run`.
3. Open `http://localhost:<port>/swagger` in your browser.

## How to Test
1. Navigate to the root.
2. Run `dotnet test`.