namespace LibraryManagmentSystem;

public record class Book {
    private static int lastId = 0;
    public int Id { get; init; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Isbn { get; set; }
    public bool IsAvailable { get; set; }

    public Book() {
    }
    public Book(string title, string author, string isbn, bool isAvailable) {
        Id = ++lastId;
        Title = title;
        Author = author;
        Isbn = isbn;
        IsAvailable = isAvailable;
    }
    public static void setLastId(int id) {
        lastId = id;
    }
    public override string ToString() {
        return $"[ID: {Id}] {Title}";
    }
}
