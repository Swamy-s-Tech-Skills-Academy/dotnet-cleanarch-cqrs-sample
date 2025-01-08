# dotnet-cleanarch-cqrs-sample

I am learning .NET 9 Clean Architecture CQRS MediatR Filtering from different Video Courses, Books, and Websites. This repository demonstrates how to implement Clean Architecture with CQRS (Command Query Responsibility Segregation), MediatR, and filtering in a .NET API.

## Table of Contents

- [dotnet-cleanarch-cqrs-sample](#dotnet-cleanarch-cqrs-sample)
  - [Table of Contents](#table-of-contents)
  - [Project Description](#project-description)
  - [Technologies Used](#technologies-used)
  - [Getting Started](#getting-started)
  - [Project Structure](#project-structure)
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
dotnet-cleanarch-cqrs-sample/
├── src/
│   ├── Application/       (Use Cases, Handlers, DTOs, Mappings, Validators)
│   ├── Domain/            (Entities, Interfaces, Domain Logic)
│   ├── Infrastructure/   (Database, External Services)
│   └── Presentation/     (Controllers)
└── ...
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
