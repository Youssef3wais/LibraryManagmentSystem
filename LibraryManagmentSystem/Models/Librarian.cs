namespace LibraryManagmentSystem;

public class Librarian: Person{
    private Library library;
    
    public Librarian(string name, Library library): base(name){
        this.library = library;
    }
    public void AddNewBook(Book book) {
        library.AddBook(book);
    }
    public void AddNewBook(string title, string author, string isbn, bool isAvailable) {
        library.AddBook(new Book(title, author, isbn, isAvailable));
    }
    public override string ToString() {
        return $"Librarian name: {this.Name}, Id: {this.Id}";
    }
}
