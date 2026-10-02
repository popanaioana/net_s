# SOLID Principles Review – Siemens.Internship2026.GradeBook

## Violation 1 — Single Responsibility Principle (SRP)

**File:** `GradeController.cs`

**Why it is a violation:**  
The controller originally handled both HTTP concerns and business logic. Controllers should primarily receive requests, delegate work to the appropriate service, and return HTTP responses.

Keeping business rules directly inside the controller makes the code harder to maintain and test.

**Fix applied:**
- Introduced `IGradeService` and `GradeService` to separate business logic from HTTP handling.
- The controller delegates grade-related operations to the service layer.
- Added `ILogger<GradeController>` for structured application logging.

---

## Violation 2 — Missing Service Layer / Separation of Concerns

**Files:** `GradeController.cs`, `GradeService.cs`, `IGradeService.cs`

**Why it is a violation:**  
Without a dedicated service layer, business rules can become mixed with HTTP handling or data-access logic.

This creates unnecessary coupling between the controller and repository and makes future changes more difficult.

**Fix applied:**  
Introduced the `IGradeService` / `GradeService` abstraction.

The application now follows a layered structure:

`Controller → Service → Repository`

The controller handles HTTP requests, the service coordinates business operations, and the repository is responsible for retrieving grade data.

---

## Violation 3 — Dependency Inversion Principle (DIP)

**File:** `Program.cs`

**Why it is a violation:**  
High-level components such as controllers and services should depend on abstractions rather than concrete implementations.

Directly creating repository or service implementations would tightly couple the components and make the application harder to test or extend.

**Fix applied:**  
Dependencies are registered through ASP.NET Core's dependency injection container.

The application uses abstractions such as:

- `IGradeReader`
- `IGradeService`

The current repository implementation is injected through the DI container instead of being instantiated directly by the controller or service.

This makes it possible to replace the data source without modifying the higher-level components.

---

## Violation 4 — Domain Naming and Code Clarity

**Files:** Model, repository, service, and controller classes

**Why it is a problem:**  
The original implementation used generic names such as `Item`, `IItemReader`, `ItemRepository`, and `ItemController`.

Because the application represents a GradeBook, these names did not clearly communicate the purpose of the classes.

Clear domain terminology improves readability and maintainability.

**Fix applied:**  
The application was renamed to use GradeBook-specific terminology:

- `Item` → `Grade`
- `IItemReader` → `IGradeReader`
- `ItemRepository` → Grade repository implementation
- `ItemController` → `GradeController`

This makes the responsibilities of the classes easier to understand.

---

## Violation 5 — Open/Closed Principle (OCP)

**Files:** `GradeController.cs`, `GradeService.cs`

**Why it is a violation:**  
When business rules are hard-coded directly inside controller actions, changing or extending those rules requires modifying the HTTP layer.

This creates unnecessary coupling between API behavior and business logic.

**Fix applied:**  
Business operations are delegated to the service layer.

This allows new grade-related rules and operations to be introduced in the service layer without requiring the controller to manage their implementation details.

The separation also makes alternative implementations easier to introduce through interfaces and dependency injection.

---

## Violation 6 — API Route Validation

**File:** `GradeController.cs`

**Why it is a problem:**  
An unconstrained route parameter such as:

```csharp
[HttpGet("{id}")]
```

can match values that are not valid integers.

**Fix applied:**  
The route uses an integer constraint:

```csharp
[HttpGet("{id:int}")]
```

ASP.NET Core routing can therefore reject incompatible route values before the controller action is executed.

---

## Summary

| # | Principle / Concern | Area | Improvement |
|---|---|---|---|
| 1 | SRP | Controller | Separated HTTP handling from business operations |
| 2 | Separation of Concerns | Architecture | Introduced a service layer |
| 3 | DIP | Dependency Injection | Components depend on interfaces |
| 4 | Domain Naming | Project structure | Replaced generic `Item` terminology with `Grade` terminology |
| 5 | OCP | Controller / Service | Business behavior can evolve outside the HTTP layer |
| 6 | API Design | Routing | Added an integer route constraint |

## Resulting Architecture

The refactored application follows a clearer layered architecture:

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
Repository / External Data Source
```

This structure reduces coupling between components and makes the application easier to maintain, test, and extend.