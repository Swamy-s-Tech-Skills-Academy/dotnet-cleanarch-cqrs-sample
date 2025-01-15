# Clean Architecture CQRS Sample in .NET 9

I am learning .NET 9 Clean Architecture CQRS MediatR Filtering from different Video Courses, Books, and Websites. This repository demonstrates how to implement Clean Architecture with CQRS (Command Query Responsibility Segregation), MediatR, and filtering in a .NET API.

## Table of Contents

- [Clean Architecture CQRS Sample in .NET 9](#clean-architecture-cqrs-sample-in-net-9)
  - [Table of Contents](#table-of-contents)
  - [Project Description](#project-description)
  - [Few Commands](#few-commands)
  - [Technologies Used](#technologies-used)
  - [Getting Started](#getting-started)
  - [Project Structure](#project-structure)
  - [Categories Feature](#categories-feature)
  - [Key Features/Examples](#key-featuresexamples)
    - [Filtering by Price](#filtering-by-price)
    - [Filtering by Date](#filtering-by-date)
    - [Filtering by Category](#filtering-by-category)
    - [Pagination](#pagination)
  - [Error Handling](#error-handling)
  - [Logging](#logging)
  - [Testing](#testing)
  - [Contributing](#contributing)
  - [License](#license)

## Project Description

This sample project provides a practical example of building a .NET API using Clean Architecture principles, CQRS, and MediatR. It focuses on implementing robust filtering and querying capabilities while maintaining a clean, testable, and maintainable codebase.

## Few Commands

```text
PS D:\STSA\dotnet-cleanarch-cqrs-sample\src>
```

```powershell
dotnet ef migrations add InitialCreate --context StoreDbContext --project .\Products.Infrastructure --startup-project .\Products.API -o Persistence/Migrations
dotnet ef migrations add AddCategoryDescription --context StoreDbContext --project .\Products.Infrastructure --startup-project .\Products.API -o Persistence/Migrations

# Update database for StoreDbContext
dotnet ef database update --context StoreDbContext --project .\Products.Infrastructure --startup-project .\Products.API
```

## Technologies Used

- .NET [Version]
- C# [Version]
- Clean Architecture
- CQRS (Command Query Responsibility Segregation)
- MediatR
- AutoMapper
- FluentValidation
- Serilog
- Entity Framework Core (or your chosen ORM)

## Getting Started

1.  Clone the repository: `git clone https://github.com/YourUsername/dotnet-cleanarch-cqrs-sample.git`
2.  Navigate to the project directory: `cd dotnet-cleanarch-cqrs-sample`
3.  Restore NuGet packages: `dotnet restore`
4.  Build the project: `dotnet build`
5.  Run the project: `dotnet run`

## Project Structure

```text
dotnet-cleanarch-cqrs-sample/      (Solution Folder)
├── CleanArchCQRS.Sample.sln       (Solution File)
├── src/
│   ├── Products.Domain/
│   ├── Products.Application/
│   ├── Products.Infrastructure/
│   └── Products.API/
└── tests/
    └── CleanArchCQRS.Sample.Tests/
```

## Categories Feature

```text
Listing all categories

Creating new categories

Editing existing categories

Deleting categories

Filtering and sorting categories
```

## Key Features/Examples

### Filtering by Price

(Provide a code example of how to use price filtering)

### Filtering by Date

(Provide a code example of how to use date filtering)

### Filtering by Category

(Provide a code example of how to use category filtering)

### Pagination

(Show how to use pagination in the requests and responses)

## Error Handling

(Explain the centralized error handling middleware)

## Logging

(Explain how logging is implemented using Serilog)

## Testing

(Briefly explain the testing strategy and provide a few example test cases)

## Contributing

(Optional: Add guidelines for contributing to the project)

## License

(Include the license information)


**Strategy Design Pattern:**

*   **Intent:** Defines a family of algorithms, encapsulates each one, and makes them interchangeable. Strategy lets the algorithm vary independently from clients that use it.
*   **Level:** Design pattern (object-oriented design). Focuses on how to structure classes and objects within a single part of an application.
*   **Problem:** Solves the problem of having multiple ways to perform a specific task and wanting to choose the algorithm at runtime.
*   **Example:** Different sorting algorithms (bubble sort, quicksort, merge sort). You can switch between these algorithms based on the data or performance requirements.

**CQRS (Command Query Responsibility Segregation):**

*   **Intent:** Separates read and write operations for a data store. This means that the models used for querying data are different from the models used for updating data.
*   **Level:** Architectural pattern. Focuses on the overall structure of an application or a significant part of it.
*   **Problem:** Solves the problem of complex data models and performance bottlenecks that can arise when using the same data model for both reads and writes, especially in complex domains.
*   **Example:** In an e-commerce application, the "Product" model used for displaying product details on a product page might be very different from the model used for adding a product to the inventory.

**Key Differences Summarized:**

| Feature          | Strategy Pattern                                   | CQRS                                               |
| ---------------- | -------------------------------------------------- | -------------------------------------------------- |
| **Intent**       | Interchangeable algorithms                         | Separation of read and write models              |
| **Level**        | Design pattern (object-oriented)                   | Architectural pattern                             |
| **Focus**        | How to implement a specific task                  | How to structure data access and manipulation    |
| **Granularity**  | Fine-grained (within a class or small set of classes) | Coarse-grained (application or bounded context)   |
| **Primary Goal** | Flexibility in algorithm selection                | Performance, scalability, and data consistency |

**Relationship (Can be Used Together):**

While they are distinct, the Strategy pattern can be used *within* a CQRS architecture. For example:

*   You might use the Strategy pattern to choose different query strategies based on the type of query being executed (e.g., a simple lookup vs. a complex report).
*   You could use the Strategy pattern to select different command handlers based on the type of command being processed.

**In essence:**

*   The Strategy pattern is about choosing *how* to do something.
*   CQRS is about deciding *what* models to use for different operations (reads vs. writes).

CQRS is a much broader concept than the Strategy pattern. It's an architectural decision that impacts the entire structure of your data access and manipulation logic. The Strategy pattern, on the other hand, is a more localized design decision that helps you manage variations within a specific task or algorithm.
