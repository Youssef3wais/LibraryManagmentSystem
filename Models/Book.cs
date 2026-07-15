namespace LibraryManagmentSystem;

public class Book {
    public static int lastId = 0 ;
    public int Id {get;private set;}
    public string Title{get;set;} 
    public string Author{get;set;}
    public string Isbn{get;set;} 
    public bool IsAvailable{get;set;}

    public Book(string title, string author, string isbn, bool isAvailable) {
        Id = ++lastId;
        Title = title;
        Author = author;
        Isbn = isbn;
        IsAvailable = isAvailable;
    }
    // public Book(string title, string author, bool isAvailable) {
    //     Id = ++lastId;
    //     Title = title;
    //     Author = author;
    //     IsAvailable = isAvailable;
    // }
}
