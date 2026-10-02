# GradeBook API

A RESTful Web API built with **ASP.NET Core** for retrieving and processing grade data.

The project demonstrates a clean layered architecture, dependency injection, asynchronous programming, external API communication, and the application of SOLID principles.

## Features

- Retrieve all grades
- Retrieve a grade by ID
- Retrieve top passing grades
- Calculate grade statistics
- Fetch grade data from an external source
- Asynchronous data access using `async` / `await`
- Structured logging with `ILogger`
- Dependency injection using ASP.NET Core
- Route validation using ASP.NET Core constraints

## Architecture

The application follows a layered architecture:

```text
HTTP Request
     ↓
GradeController
     ↓
IGradeService
     ↓
GradeService
     ↓
IGradeReader
     ↓
HttpGradeRepository
     ↓
External API
```

### Controller Layer

Handles HTTP requests and responses while delegating business operations to the service layer.

### Service Layer

Contains grade-related business logic and coordinates operations between the controller and repository.

### Repository Layer

Implements data access and retrieves grade information from an external API using `HttpClient`.

## Technologies

- C#
- .NET / ASP.NET Core
- ASP.NET Core Web API
- REST
- HttpClient
- Dependency Injection
- async / await
- LINQ
- ILogger
- Swagger / OpenAPI

## SOLID Principles

The project was reviewed and refactored with SOLID and separation-of-concerns principles in mind.

The main improvements include:

- separating HTTP handling from business logic
- introducing a dedicated service layer
- depending on abstractions through interfaces
- using dependency injection
- improving domain-specific naming
- separating external data access from business logic

A more detailed review is available in [`SOLID_VIOLATIONS.md`](SOLID_VIOLATIONS.md).

## API Endpoints

The API provides endpoints for accessing grade information, including:

```text
GET /api/grade
GET /api/grade/{id}
```

Additional grade operations are exposed through the `GradeController`.

Swagger can be used while running the application to explore and test the available endpoints.

## Running the Project

### Prerequisites

- .NET SDK
- Visual Studio, Visual Studio Code, or another compatible IDE

### Setup

Clone the repository:

```bash
git clone <repository-url>
```

Navigate to the project directory:

```bash
cd Siemens.Internship2026.GradeBook
```

Restore the dependencies:

```bash
dotnet restore
```

Run the application:

```bash
dotnet run
```

After starting the application, open the Swagger interface using the local URL displayed in the terminal.

## Project Structure

```text
Siemens.Internship2026.GradeBook/
├── Controllers/
├── Models/
├── Repositories/
├── Services/
├── Program.cs
├── SOLID_VIOLATIONS.md
└── README.md
```

## Purpose

This project was developed as a technical exercise focused on building and refactoring an ASP.NET Core Web API while applying clean-code practices and SOLID design principles.