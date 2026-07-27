# Library Management System

A professional, object-oriented console-based Library Management System (LMS) built with C# and .NET 10.0. The application enables library administrators (librarians) and members to manage book inventories, search for books, register members, and perform borrow/return actions.

---

## Table of Contents
1. [Project Overview](#project-overview)
2. [Directory Structure](#directory-structure)
3. [UML Class Diagram](#uml-class-diagram)
4. [How to Run the Project](#how-to-run-the-project)
5. [Design Decisions](#design-decisions)
6. [Bonus Features & Implementation Details](#bonus-features--implementation-details)
    - [LINQ for Searching and Filtering](#1-linq-for-searching-and-filtering-implemented)
    - [Dependency Injection](#2-dependency-injection-implemented)
    - [Logging / Notifications](#3-logging--notifications-partially-implemented)
    - [Unit Tests](#4-unit-tests-not-implemented)
    - [Generic Repository Pattern](#5-generic-repository-pattern-not-implemented)
    - [Save Data to JSON](#6-save-data-to-json-not-implemented)
    - [Load Data from JSON](#7-load-data-from-json-not-implemented)

---

## Project Overview

The Library Management System provides a Command Line Interface (CLI) application for executing common library transactions. 

### Key Features
* **Book Management**: Register new books with automated duplicate prevention via ISBN validation.
* **Member Registration**: Track library members using an auto-incrementing ID system.
* **Borrowing & Returning**: Borrow and return books with real-time availability updates and notifications.
* **Search Functionality**: Dynamic, case-insensitive keyword searching of titles using LINQ.

---

## Directory Structure

```text
LibraryManagementSystem/ (Root)
│
├── LibraryManagementSystem.Tests/
│   ├── LibraryManagementSystem.Tests.csproj
│   ├── LibraryTests.cs                # xUnit test cases
│   └── ...
│
└── LibraryManagmentSystem/
    ├── Data/
    │   ├── Books.json                 # JSON data store for Books
    │   └── Members.json               # JSON data store for Members
    │
    ├── Models/
    │   ├── Person.cs                  # Abstract base class representing any individual (ID, Name)
    │   ├── Member.cs                  # Represents library members with borrowed book lists
    │   ├── Librarian.cs               # Represents administrators capable of adding books
    │   └── Book.cs                    # Core Book entity (ID, Title, Author, ISBN, Availability)
    │
    ├── Services/
    │   ├── INotificationService.cs    # Logging and notification interface
    │   ├── ConsoleNotificationService.cs # Concrete service outputting to console
    │   ├── EmailNotificationService.cs   # Concrete service simulating email logging
    │   ├── IJsonHandler.cs            # Generic JSON serialization interface
    │   ├── JsonHandler.cs             # Generic JSON file handler implementation
    │   └── Library.cs                 # Core engine containing business logic
    │
    ├── Program.cs                     # CLI entry point, path setup, and menu router
    ├── LibraryManagmentSystem.csproj
    ├── uml_class_diagram.png          # UML class diagram of the system
    ├── .gitignore                     # Git ignore configuration
    └── README.md                      # Project documentation
```

---

## UML Class Diagram

Below is the UML class diagram showing the core classes, interfaces, inheritance hierarchy, and relationships:

![UML Class Diagram](uml_class_diagram.png)

---

## How to Run the Project

### Prerequisites
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download) installed on your system.

### Build and Run Steps
1. Navigate to the project's source directory:
   ```bash
   cd LibraryManagmentSystem
   ```
2. Build the project:
   ```bash
   dotnet build
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

### Running Unit Tests
To run the xUnit test suite, execute the following command from the repository root directory:
```bash
dotnet test LibraryManagementSystem.Tests/LibraryManagementSystem.Tests.csproj
```

### Usage
Upon launching, the interactive CLI menu displays:
```text
==========================
Library Management System
==========================
1. Add Book
2. Register Member
3. Borrow Book
4. Return Book
5. List Available Books
6. Search Books
7. Exit
8. Remove book from the library
Select option:
```
Follow the console prompts to add/remove books, register members, borrow/return books, list available books, or search books.

---

## Design Decisions

* **Inheritance & Abstraction**: Code duplication is minimized by using an abstract base class `Person` for common fields (`Id`, `Name`) and abstract method signatures (`displayInfo()`), which are inherited by `Member` and `Librarian`.
* **Polymorphism**: The `INotificationService` interface decouples user notifications from the domain models. The system can switch between Console notifications and simulated Email notifications without changing the consumer code.
* **Data Integrity**:
  * ISBN uniqueness is enforced at the `Library` service level during book addition.
  * Availability state (`IsAvailable`) transitions atomically when books are checked out or returned.
  * Persisted state is automatically synchronized on write operations to JSON datastores.

---

## Bonus Features & Implementation Details

Below is the status of the requested bonus features with references to their code implementations:

### 1. LINQ for Searching and Filtering (Implemented)
LINQ is leveraged in [Library.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/Library.cs) for efficient query execution instead of manual loops:
* **Item Retrieval**: Uses `FirstOrDefault` to retrieve items by identifier.
  ```csharp
  Book book = books.FirstOrDefault(book => book.Id == bookId);
  ```
* **Case-Insensitive Searching**: Uses `Where` and `StringComparison.OrdinalIgnoreCase` to search titles by partial keyword match.
  ```csharp
  return books.Where(book => book.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
  ```

### 2. Dependency Injection (Implemented)
Constructor-based dependency injection is used to supply notifications and JSON handler services to the core system in [Program.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Program.cs):
* **Library DI**:
  ```csharp
  // Program.cs
  Library library = new Library(
      new ConsoleNotificationService(),
      new JsonHandler<Book>(booksJsonFilePath),
      new JsonHandler<Member>(membersJsonFilePath)
  );
  ```
* **Librarian DI**:
  ```csharp
  // Librarian.cs
  public Librarian(string name, Library library): base(name){
      this.library = library;
  }
  ```

### 3. Logging / Notifications (Implemented)
Logging and notification dispatching are abstracted using [INotificationService.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/INotificationService.cs). 
* `ConsoleNotificationService` logs directly to standard output.
* `EmailNotificationService` prefixes messages with `[Email] Sending:` to simulate mail notification logging.

### 4. Unit Tests (Implemented)
Unit tests are implemented under the [LibraryManagementSystem.Tests](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagementSystem.Tests) project using **xUnit**:
* **Test File**: [LibraryTests.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagementSystem.Tests/LibraryTests.cs)
* **Features Tested**:
  * Prevent duplicate ISBN addition.
  * Attempting to remove non-existent or borrowed books.
  * Search capability using LINQ queries.
* **Test Isolation**: Leverages custom fake implementations (`FakeJsonHandler<T>` and `FakeNotificationService`) to run unit tests in memory without causing side-effects to disk JSON files.

### 5. Generic Repository Pattern (Implemented)
Instead of hardcoding file writing logic inside the domain layer or the `Library` service directly:
* We introduced the generic handler interface [IJsonHandler.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/IJsonHandler.cs) and its implementation [JsonHandler.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/JsonHandler.cs).
* This provides a clean generic repository-style interface for database serialization and deserialization, separating file storage rules from the business rule engine.

### 6. Save Data to JSON (Implemented)
* Serialization to JSON files (`Books.json` and `Members.json`) is executed whenever the state of the library is updated (e.g., adding/removing a book, registering a member, or borrowing/returning a book).
* File writes are handled asynchronously/atomically via standard JSON serializer options inside [JsonHandler.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/JsonHandler.cs).

### 7. Load Data from JSON (Implemented)
* When the application starts, the injected handlers read `Books.json` and `Members.json` using `ReadFileToList()` to prepopulate the list of books and members in [Library.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/Library.cs).
* Ensures the user's data persists across program lifecycles.

