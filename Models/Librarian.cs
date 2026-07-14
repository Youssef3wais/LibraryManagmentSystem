namespace LibraryManagmentSystem;

public class Librarian: Person{
    Library library = Library.getInstance();
    public Librarian(string name): base(name){
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
