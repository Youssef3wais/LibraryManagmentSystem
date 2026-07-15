namespace LibraryManagmentSystem;

public class Librarian: Person{
    private Library library;
    
    public Librarian(string name, Library library): base(name){
        this.library = library;
    }
    public void addNewBook(Book book) {
        library.addBook(book);
    }
    public void addNewBook(string title, string author, string isbn, bool isAvailable) {
        library.addBook(new Book(title, author, isbn, isAvailable));
    }
    public override void displayInfo() {
        Console.WriteLine($"Librarian name: {this.Name}, Id: {this.Id}");
    }
}
