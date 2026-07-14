namespace LibraryManagmentSystem;

public class Library {
    private static Library instance;
    private static INotificationService _notificationService ; 
    private List<Book> books = new List<Book>();
    private List<Member> members = new List<Member>();

    private Library() {
        _notificationService = new ConsoleNotificationService(); 
    }

    public static Library getInstance() {
        if(instance == null) {
            instance = new Library();
        }
        return instance;
    }
    public INotificationService GetNotificationService() {
        return _notificationService ;
    }
    
    public void addBook(Book book) {
        books.Add(book);
        _notificationService.notify($"{book.Title} added succesfully with id: {book.Id}");
    }

    public bool removeBook(int bookId) {
        bool removed = false ;
        foreach(Book book in books) {
            if (book.Id == bookId) {
                if (book.IsAvailable) {
                    books.Remove(book);
                    removed = true ;
                    _notificationService.notify($"{book.Title} successfully removed from library...");
                } else {
                    _notificationService.notify($"{book.Title} isn't available in the library, can't be removed!!!");
                }
                break;
            }
        }
        if (!removed) { //print notfound message
            _notificationService.notify($"book Id: {bookId} Not Found!!!");
        }
        return removed;
    }
    public void registerMember(Member member) {
        members.Add(member);
        _notificationService.notify($"{member.Name} succesfully registered, with Id {member.Id}");
    }

    public bool borrowBook(int memberId, int bookId) {
        bool borrowed = false;
        //loop with id, later changes to linq
        foreach(Book book in books) {
            if (book.Id == bookId) {
                foreach(Member member in members) {
                    if (member.Id == memberId) {
                        borrowed = member.borrowBooks([book]);
                        return borrowed;
                    }
                }
                _notificationService.notify($"Member Id: {memberId}, Doesn't exist");
                break;
            }
        }
        _notificationService.notify($"Book Id: {bookId}, Doesn't exist");
        return borrowed;
    }

    public bool returnBook(int memberId, int bookId) {
        bool bookReturned = false;
        //loop with id, later changes to linq
        foreach(Book book in books) {
            if (book.Id == bookId) {
                foreach(Member member in members) {
                    if (member.Id == memberId) {
                        bookReturned = member.returnBooks([book]);
                        return bookReturned;
                    }
                }
                _notificationService.notify($"Member Id: {memberId}, Doesn't exist");
                break;
            }
        }
        _notificationService.notify($"Book Id: {bookId}, Doesn't exist");
        return bookReturned;
    }

    public void displayAvailableBooks() {
        bool bookDisplayed = false ;
        _notificationService.notify("Current available books: ");
        foreach(Book book in books) {
            if (book.IsAvailable) {
                _notificationService.notify($"- Book Title: {book.Title}, Id: {book.Id}");
                bookDisplayed = true ;
            }
        }
        if (!bookDisplayed) {
            _notificationService.notify("No current available books");
        }
    }

    public List<Book> searchBooks(string keyword) {
        List<Book> matchedBooks = new List<Book>() ;
        if(keyword == null) {
            _notificationService.notify("Please enter a valid keyword!!");
            return matchedBooks;
        }
        foreach(Book book in books) {
            if (book.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)) {
                matchedBooks.Add(book);
            }
        }
        return matchedBooks;
    }
    public void setNotificationService(INotificationService notificationService) {
        _notificationService = notificationService ;
    }

}
