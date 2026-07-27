using System.Collections.Generic;
using LibraryManagmentSystem;
using JsonHandler;
using Xunit;

namespace LibraryManagmentSystem.Tests;

public class LibraryTests
{
    // 1. Declare the field
    public Library _library;

    // 2. Initialize _library inside the constructor
    public LibraryTests()
    {
        _library = new Library(
            new FakeNotificationService(),
            new FakeJsonHandler<Book>(),
            new FakeJsonHandler<Member>()
        );
    }

    [Fact]
    public void AddBook_ShouldReturnTrue_WhenIsbnIsUnique() {
        var newBook = new Book("java", "yusef", "009191", true);
        bool result = _library.addBook(newBook);

        Assert.True(result);
    }

    [Fact]
    public void RemoveBook_ShouldReturnIfbookIdIsntAvailableOrNull() {
        var newBook = new Book("java", "yusef", "009191", false);
        _library.addBook(newBook);

        bool result = _library.removeBook(newBook.Id);
        Assert.False(result);
    }

    [Fact]
    public void SearchBooks_ShouldReturnMatchingBook() {
        var book = new Book("Clean Code", "Robert C. Martin", "12345", true);
        _library.addBook(book);

        List<Book> results = _library.searchBooks("Clean Code");

        Assert.NotEmpty(results);
    }
}

// classes to fake testing
public class FakeJsonHandler<T> : IJsonHandler<T>
{
    public List<T> ReadFileToList() => new List<T>();
    public bool WriteListToFile(List<T> items) => true;
}

public class FakeNotificationService : INotificationService
{
    public void notify(string message) { }
}