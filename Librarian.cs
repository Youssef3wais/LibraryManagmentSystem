namespace LibraryManagmentSystem;

public class Librarian: Person{
    public Librarian(string name): base(name){
    }
    public void addNewBook(Book book) {
        //add in library using addBook function
    }
    public void addNewBook(string title, string author, string isbn, bool isAvailable) {
        Book book = new Book(title, author, isbn, isAvailable);
        //add in library using addBook function
    }
    public override void displayInfo() {
        Console.WriteLine($"Librarian name: {this.Name}, Id: {this.Id}");
    }
    
}
