using System.Runtime.CompilerServices;

namespace LibraryManagmentSystem;

public class Member : Person {
    private Library _library = Library.getInstance();

        // 2. Access the notification service through your public getter
        
    private List<Book> Books = new List<Book>();

    public Member(string name): base(name) {
    }
    
    public bool borrowBooks(List<Book> books) {
        bool borrowed = false ;
        foreach(Book book in books) {
            if (book.IsAvailable) {
                Books.Add(book);
                borrowed = true;
                _library.GetNotificationService().notify($"{this.Name} succesfully borrowed {book.Title}");
                book.IsAvailable = false ; // when some one borrow a book changes to not available
            } else {
                _library.GetNotificationService().notify($"{book.Title} isn't available right now!!");
            }
        }
        return borrowed ;
    }
    public bool returnBooks(List<Book> books) {
        bool allBooksReturned = true ; //is all books in the list books returned?
        foreach(Book book in books) {
            bool removed = Books.Remove(book);
            if (removed) {
                _library.GetNotificationService().notify($"{this.Name} succesfully returned {book.Title}");
                book.IsAvailable = true ;
            }else if (!removed) {
                //notification book not found 
                allBooksReturned = false ;
                _library.GetNotificationService().notify($"{book.Title} is not found in borrowed books by {this.Name}");
            }
        }
        return allBooksReturned ;
    }
    public override void displayInfo() {
        _library.GetNotificationService().notify($"Member name: {this.Name}, Id: {this.Id}");
        if (Books.Count < 1) {
            _library.GetNotificationService().notify("No Borrowed books");
        } else {
            _library.GetNotificationService().notify("Current Borrowed books: ");
            for(int i=0; i<Books.Count;i++) {
                _library.GetNotificationService().notify($"{i+1}: {Books[i].Title}");
            }
        }
    }



}
