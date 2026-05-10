# SOLID Principles Review – Siemens.Internship2026.GradeBook

## Violation 1 — Single Responsibility Principle (SRP)

**File:** `ItemController.cs` — `GetAll()` method

**Why it is a violation:**
The controller directly computes business statistics (`TotalCount`, `AverageValue`) inline inside the action method. A controller's only responsibility is to handle HTTP concerns (receive a request, delegate to a service, return a response). Aggregation and statistical logic is business logic and must not live in the controller.

Additionally, the controller uses raw `Console.WriteLine` for logging instead of the framework-provided `ILogger<T>`, mixing infrastructure concerns into the controller.

**Fix applied:**
- Moved all business logic (passing-grade filter, statistics computation) into `GradeService`.
- Injected `ILogger<GradeController>` and replaced every `Console.WriteLine` with structured log calls (`_logger.LogInformation`, `_logger.LogWarning`).

---

## Violation 2 — Single Responsibility Principle (SRP) / Missing Service Layer

**Files:** `ItemController.cs`, `ItemRepository.cs`

**Why it is a violation:**
There is no service layer. Business rules (e.g., "only active items", "passing grade ≥ 5") are either implied inside the repository's LINQ queries or computed ad-hoc in the controller. The repository's job is data access; the controller's job is HTTP handling. Neither should own business logic.

**Fix applied:**
Introduced `IGradeService` / `GradeService`. All business rules now live there:
- `GetTopPassingGradesAsync(int count)` filters active grades with `Value >= 5` and takes the first `count` results.
- The controller delegates entirely to the service and does no filtering or computation itself.

---

## Violation 3 — Dependency Inversion Principle (DIP)

**File:** `Program.cs`

**Why it is a violation:**
The original `Program.cs` registered no services at all — `IItemReader` was never registered in the DI container, so injecting it into `ItemController` would throw a runtime `InvalidOperationException`. High-level modules (controller) must depend on abstractions registered through the DI system.

**Fix applied:**
Registered both abstractions in `Program.cs`:
```csharp
builder.Services.AddSingleton<IGradeReader, GradeRepository>();
builder.Services.AddScoped<IGradeService, GradeService>();
```

---

## Violation 4 — Interface Segregation Principle (ISP) / Naming

**File:** `IItemReader.cs`, `Item.cs`, `ItemController.cs`, `ItemRepository.cs`

**Why it is a violation:**
The domain is a GradeBook, yet the model is called `Item`, the interface `IItemReader`, the repository `ItemRepository`, and the controller `ItemController`. The interface name `IItemReader` also leaks the implementation concern ("reader") without clearly expressing the domain concept. Generic names obscure intent, making the codebase harder to understand and maintain.

**Fix applied:**
Renamed throughout to use the correct ubiquitous language:
- `Item` → `Grade`
- `IItemReader` → `IGradeReader`
- `ItemRepository` → `GradeRepository`
- `ItemController` → `GradeController`

---

## Violation 5 — Open/Closed Principle (OCP)

**File:** `ItemController.cs` — `GetAll()` method

**Why it is a violation:**
The statistics block (`TotalCount`, `AverageValue`) is hard-coded in the controller action. Adding any new statistic or changing the filtering criteria requires modifying the controller directly. The system is not open for extension without modification.

**Fix applied:**
By pushing all computation into the service layer (`GradeService`), new business rules or statistics can be added to the service (or new service implementations) without touching the controller. The controller simply calls `_gradeService.GetAllGradesAsync()` and returns the result.

---

## Violation 6 — Single Responsibility Principle (SRP) — Route constraint missing

**File:** `ItemController.cs` — `GetById(int id)` 

**Why it is a violation:**
The route `[HttpGet("{id}")]` does not constrain the `{id}` segment to integers. A non-numeric URL segment like `/api/item/abc` would reach the action and fail with an unhandled format exception rather than a clean 404/400.

**Fix applied:**
Changed to `[HttpGet("{id:int}")]` so the routing layer rejects non-integer segments before the action is invoked.

---

## Summary Table

| # | Principle | File(s) | Fix |
|---|-----------|---------|-----|
| 1 | SRP | `ItemController.cs` | Moved stats/logging to service + ILogger |
| 2 | SRP (missing layer) | `ItemController.cs`, `ItemRepository.cs` | Introduced `IGradeService` / `GradeService` |
| 3 | DIP | `Program.cs` | Registered `IGradeReader` and `IGradeService` in DI |
| 4 | ISP / Naming | All files | Renamed to `Grade`, `IGradeReader`, `GradeRepository`, `GradeController` |
| 5 | OCP | `ItemController.cs` | Pushed business logic to service layer |
| 6 | SRP | `ItemController.cs` | Added `{id:int}` route constraint |
