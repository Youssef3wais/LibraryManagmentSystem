# Library Management System

A professional, object-oriented console-based Library Management System (LMS) built with C# and .NET 10.0. The application enables library administrators (librarians) and members to manage book inventories, register members, borrow and return books, search the catalog, and log notifications.

---

## Table of Contents
1. [Project Overview](#project-overview)
2. [Directory Structure](#directory-structure)
3. [How to Run the Project](#how-to-run-the-project)
4. [Design Decisions & OOP Principles](#design-decisions--oop-principles)
5. [Bonus Features & Implementation Details](#bonus-features--implementation-details)
    - [1. Save Data to JSON](#1-save-data-to-json-implemented)
    - [2. Load Data from JSON](#2-load-data-from-json-implemented)
    - [3. LINQ for Searching and Filtering](#3-linq-for-searching-and-filtering-implemented)
    - [4. Dependency Injection](#4-dependency-injection-implemented)
    - [5. Logging](#5-logging-implemented)
    - [6. Unit Tests](#6-unit-tests-implemented)
    - [7. Generic Repository Pattern](#7-generic-repository-pattern-implemented)

---

## Project Overview

The Library Management System provides a clean Command Line Interface (CLI) for executing common library transactions. 

### Key Features
* **Book Management**: Register new books with automated duplicate prevention via ISBN validation, and remove books from the library (implemented in [Library.addBook](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/Library.cs#L29-L43) and [Library.removeBook](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/Library.cs#L45-L66)).
* **Member Registration**: Register library members dynamically with an auto-incrementing ID system (implemented in [Library.registerMember](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/Library.cs#L67-L71)).
* **Borrowing & Returning**: Borrow and return books with real-time availability updates (implemented in [Library.borrowBook](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/Library.cs#L73-L96), [Library.returnBook](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/Library.cs#L98-L122), [Member.borrowBooks](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Models/Member.cs#L15-L28), and [Member.returnBooks](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Models/Member.cs#L29-L43)).
* **Search Functionality**: Case-insensitive partial keyword search of book titles using LINQ (implemented in [Library.searchBooks](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/Library.cs#L151-L159)).

---

## Directory Structure

```text
LibraryManagementSystem/ (Root)
│
├── LibraryManagementSystem.Tests/      # Unit testing project (xUnit)
│   ├── LibraryManagementSystem.Tests.csproj
│   └── LibraryTests.cs                 # Unit test cases and test fakes
│
└── LibraryManagmentSystem/              # Main application project
    ├── Data/
    │   ├── Books.json                  # JSON persistent store for Books
    │   ├── Members.json                # JSON persistent store for Members
    │   └── Data.log                    # File logger log output
    │
    ├── Models/
    │   ├── Person.cs                   # Abstract base class representing any individual (ID, Name)
    │   ├── Member.cs                   # Represents library members with borrowed book lists
    │   ├── Librarian.cs                # Represents library administrators who can manage books
    │   └── Book.cs                     # Record class representing a Book entity
    │
    ├── Services/
    │   ├── INotificationService.cs     # Notification service interface
    │   ├── ConsoleNotificationService.cs # Outputs notifications to Console
    │   ├── EmailNotificationService.cs # Simulates sending email notifications
    │   ├── FileLogger.cs               # Logs notifications to Data.log with timestamp
    │   ├── IJsonHandler.cs             # Generic interface for JSON serialization/deserialization
    │   ├── JsonHandler.cs              # Concrete generic JSON file handler
    │   └── Library.cs                  # Main service containing library business logic
    │
    ├── Program.cs                      # CLI entry point and Dependency Injection setup
    └── README.md                       # Project documentation
```

---

## How to Run the Project

### Prerequisites
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download) installed on your system.

### Build and Run Steps
1. Navigate to the project's root directory:
   ```bash
   cd c:\vsProjects\LibraryManagmentSystem
   ```
2. Build the solution:
   ```bash
   dotnet build LibraryManagmentSystem\LibraryManagmentSystem.csproj
   ```
3. Run the application:
   ```bash
   dotnet run --project LibraryManagmentSystem\LibraryManagmentSystem.csproj
   ```

### Running Unit Tests
To run the xUnit test suite, execute the following command:
```bash
dotnet test LibraryManagementSystem.Tests\LibraryManagementSystem.Tests.csproj
```

### CLI Menu Interface
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
9. List Available Members
Select option:
```
Follow the console prompts to perform various operations. The CLI dynamically lists available books/members on startup.

---

## Design Decisions & OOP Principles

This system relies on core Object-Oriented Programming (OOP) concepts to maintain a clean, extensible architecture:

* **Inheritance**: Both [Member](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Models/Member.cs) and [Librarian](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Models/Librarian.cs) inherit common fields (`Id`, `Name`) and static ID-tracking methods from the abstract base class [Person](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Models/Person.cs).
* **Abstraction**: The [Person](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Models/Person.cs) base class declares an abstract method `displayInfo()`, forcing derived types to define their own specific logic for displaying information.
* **Polymorphism**: 
  * The [INotificationService](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/INotificationService.cs) interface allows the program to swap notification methods (e.g., [ConsoleNotificationService](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/ConsoleNotificationService.cs), [EmailNotificationService](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/EmailNotificationService.cs), or [FileLogger](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/FileLogger.cs)) seamlessly without changing the consuming code.
  * Method overloading is used in [Librarian](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Models/Librarian.cs#L9-L14) to support adding books by passing either a full [Book](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Models/Book.cs) object or raw metadata.
* **Encapsulation**: State information (such as availability of books, list of books checked out by members, and database writing processes) is securely managed inside respective classes ([Book](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Models/Book.cs), [Member](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Models/Member.cs), [Library](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/Library.cs)).
* **Data Persistence and Safety**:
  * Uniqueness of book ISBNs is verified during book registration to prevent duplicate records.
  * File writes are triggered atomically upon any update operation (adding/removing books, borrowing/returning books, registering members) to keep physical JSON stores in sync.
  * Static ID counters are adjusted dynamically on startup to match existing database entries, preventing key collision.

---

## Bonus Features & Implementation Details

All requested bonus features are fully implemented:

### 1. Save Data to JSON (Implemented)
When changes are made to the libraries or members list, data is serialized into `Books.json` and `Members.json` using the generic method [JsonHandler.WriteListToFile](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/JsonHandler.cs#L19-L29):
```csharp
public bool WriteListToFile(List<T> list) {                
    try {
        string json = JsonSerializer.Serialize(list, _options);
        File.WriteAllText(_filePath, json);
        _notificationService.notify($"JSON Serialization complete. Data saved to {_filePath}");
        return true ;
    }catch(Exception e) {
        _notificationService.notify($"JSON Serialization Failed. Error saving {_filePath}: {e.Message}");
        return false ;
    }
}
```

### 2. Load Data from JSON (Implemented)
During system initialization, [JsonHandler.ReadFileToList](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/JsonHandler.cs#L32-L48) parses the persistent JSON files, reconstructing object graphs:
```csharp
public List<T> ReadFileToList() {
    try {
        if (!File.Exists(_filePath)) return new List<T>();
        string json = File.ReadAllText(_filePath);
        if (string.IsNullOrEmpty(json)) {
            _notificationService.notify($"JSON Deserialization complete, File {_filePath} is Empty...");
            return new List<T>();
        }
        _notificationService.notify("JSON Deserialization complete");
        var x = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        return x;
    }catch(Exception e) {
        _notificationService.notify($"JSON Deserialization Failed. Error reading {_filePath}: {e.Message}");
        return new List<T>();
    }
}
```

### 3. LINQ for Searching and Filtering (Implemented)
Language-Integrated Query (LINQ) is utilized to manage search operations and entity lookup:
* **Lookup**: Uses `FirstOrDefault` in [Library.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/Library.cs) to find members or books by ID (e.g., `books.FirstOrDefault(book => book.Id == bookId)`).
* **Search**: Uses `Where` to filter books by keyword case-insensitively (implemented in [Library.searchBooks](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/Library.cs#L151-L159)):
  ```csharp
  return books.Where(book => book.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
  ```

### 4. Dependency Injection (Implemented)
Services are decoupled and injected using constructor injection in [Program.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Program.cs#L11-L13):
```csharp
string booksJsonFilePath = @"C:\vsProjects\LibraryManagmentSystem\LibraryManagmentSystem\Data\Books.json";
string membersJsonFilePath = @"C:\vsProjects\LibraryManagmentSystem\LibraryManagmentSystem\Data\Members.json";
Library library = new Library(
    new ConsoleNotificationService(), 
    new JsonHandler<Book>(booksJsonFilePath), 
    new JsonHandler<Member>(membersJsonFilePath)
);
```

### 5. Logging (Implemented)
The [FileLogger](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/FileLogger.cs) implements [INotificationService](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/INotificationService.cs) to log events directly to a file (`Data/Data.log`). Inside [JsonHandler.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/JsonHandler.cs#L10), the handler uses a [FileLogger](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/FileLogger.cs) to log JSON load and save results with date and time:
```csharp
private readonly INotificationService _notificationService = new FileLogger(@"C:\vsProjects\LibraryManagmentSystem\LibraryManagmentSystem\Data\Data.log");
```

### 6. Unit Tests (Implemented)
A complete xUnit test suite resides in the [LibraryManagementSystem.Tests](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagementSystem.Tests) project.
* **Test File**: [LibraryTests.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagementSystem.Tests/LibraryTests.cs)
* **Test Isolation**: Leverages custom fake implementations (`FakeJsonHandler<T>` and `FakeNotificationService` defined at the bottom of [LibraryTests.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagementSystem.Tests/LibraryTests.cs#L52-L61)) to run unit tests in-memory, ensuring disk data is untouched.
* **Test Cases**:
  * `AddBook_ShouldReturnTrue_WhenIsbnIsUnique`
  * `RemoveBook_ShouldReturnIfbookIdIsntAvailableOrNull`
  * `SearchBooks_ShouldReturnMatchingBook`

### 7. Generic Repository Pattern (Implemented)
File serialization is abstracted behind the generic interface [IJsonHandler](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/IJsonHandler.cs) and implemented in [JsonHandler](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/JsonHandler.cs). This abstracts CRUD-like file access, isolating persistence specifics from the core business rules of [Library.cs](file:///c:/vsProjects/LibraryManagmentSystem/LibraryManagmentSystem/Services/Library.cs).
