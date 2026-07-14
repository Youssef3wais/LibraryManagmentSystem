using System.Runtime.CompilerServices;

namespace LibraryManagmentSystem;

public class Member : Person {
    private List<Book> Books = new List<Book>();

    public Member(string name): base(name) {
    }

    public bool borrowBooks(List<Book> books) {
        bool borrowed = false ;
        foreach(Book book in books) {
            if (book.IsAvailable) {
                Books.Add(book);
                borrowed = true;
                book.IsAvailable = false ; // when some one borrow a book changes to not available
            } else {
                Console.WriteLine($"{book.Title} isn't available right now!!");
            }
        }
        return borrowed ;
    }
    public void returnBooks(List<Book> books) {
        foreach(Book book in books) {
            bool removed = Books.Remove(book);
            if (removed) {
                book.IsAvailable = true ;
            }else if (!removed) {
                //notification book not found 
                Console.WriteLine($"{book.Title} not found in borrowed books");
            }
        }
    }
    public override void displayInfo() {
        Console.WriteLine($"Member name: {this.Name}, Id: {this.Id}");
        if (Books.Count < 1) {
            Console.WriteLine("No Borrowed books");
        } else {
            Console.WriteLine("Current Borrowed books: ");
            for(int i=0; i<Books.Count;i++) {
                Console.WriteLine($"{i+1}: {Books[i].Title}");
            }
        }
    }



}
