using System.Text.Json.Serialization;

namespace LibraryManagmentSystem;

public class Book {
    private static int _lastId = 0;
    public int Id { get; init; }
    public string Title { get; init; }
    public string Author { get; init; }
    public string Isbn { get; init; }

    [JsonInclude]       // JsonInclude allows updating availability from file while preventing external code from bypassing MarkBorrowed()
    public bool IsAvailable { get; private set; }

    public Book() {
    }
    public Book(string title, string author, string isbn, bool isAvailable) {
        Id = Interlocked.Increment(ref _lastId);        //it either completely happens or doesn't, with no in-between state. to prevent 2 ids to be the same if called at the same time
        Title = title;
        Author = author;
        Isbn = isbn;
        IsAvailable = isAvailable;
    }
    public bool MarkBorrowed() {
        if (!IsAvailable) {     //isnt available cant be borrowed
            return false;
        }
        IsAvailable = false;
        return true;
    }

    public bool MarkReturned() {
        IsAvailable = true;
        return true;
    }

    public static void SetLastId(int id) {
        _lastId = id;
    }
    public override string ToString() {
        return $"[ID: {Id}] {Title}";
    }

    public override bool Equals(object? obj) {
        if (obj is Book other) {
            return this.Id == other.Id;
        }
        return false;
    }

    public override int GetHashCode() {
        return Id.GetHashCode();
    }
}
