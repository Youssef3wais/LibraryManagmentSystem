namespace LibraryManagmentSystem;

public class Library {
    private readonly INotificationService _notificationService ; 
    private List<Book> books = new List<Book>();
    private List<Member> members = new List<Member>();

    public Library(INotificationService notificationService) {
        _notificationService = notificationService; 
    }
    
    public bool addBook(Book newBook) {
        bool bookAdded = false;
        foreach(Book book in books) {
            if(book.Isbn == newBook.Isbn) {
                _notificationService.notify($"Duplicate ISBN, {newBook.Title} cant be added!!!");
                return bookAdded;
            }
        }
        books.Add(newBook);
        bookAdded = true ;
        _notificationService.notify($"{newBook.Title} added succesfully with id: {newBook.Id}");
        return bookAdded ;
    }

    public bool removeBook(int bookId) {
        //search using LINQ for the first matching bookId in books list
        Book book = books.FirstOrDefault(book => book.Id == bookId);

        //if book cant found 
        if(book == null) {
            _notificationService.notify($"book Id: {bookId} doesn't exist in the library!!!");
            return false ;
        }


        if (book.IsAvailable) {
            _notificationService.notify($"{book.Title} successfully removed from library...");
            return true ;

        }else {             //if book exist but isnt available right now 
            _notificationService.notify($"{book.Title} isn't available in the library right now!!!");
            return false ;
        }
    }
    public void registerMember(Member member) {
        members.Add(member);
        _notificationService.notify($"{member.Name} succesfully registered, with Id {member.Id}");
    }

    public bool borrowBook(int memberId, int bookId) {
        bool borrowed = false ;
        Member member = members.FirstOrDefault(member => member.Id == memberId);
        Book book = books.FirstOrDefault(book => book.Id == bookId);

        //if couldnt find member id in the member list 
        if (member == null) {
            _notificationService.notify($"Member Id: {memberId}, Doesn't exist in the library");
        }

        if (book == null) {
            _notificationService.notify($"book Id: {bookId} doesn't exist in the library!!!");
        }

        // if member id and book id both doesnt exist i want to display both messages then exit
        if(member == null || book == null) {
            return false ;
        }

        borrowed = member.borrowBooks([book]);
        return borrowed;
    }

    public bool returnBook(int memberId, int bookId) {
        bool bookReturned = false;
        Member member = members.FirstOrDefault(member => member.Id == memberId);
        Book book = books.FirstOrDefault(book => book.Id == bookId);

        
        //if couldnt find member id in the member list 
        if (member == null) {
            _notificationService.notify($"Member Id: {memberId}, Doesn't exist in the library");
        }

        if (book == null) {
            _notificationService.notify($"book Id: {bookId} doesn't exist in the library!!!");
        }

        // if member id and book id both doesnt exist i want to display both messages then exit
        if(member == null || book == null) {
            return false ;
        }

        bookReturned = member.returnBooks([book]);
        return bookReturned;
    }

    public void displayAvailableBooks() {
        bool bookDisplayed = false ; //to check if any book is displayed 
        _notificationService.notify("Current available books: ");
        foreach(Book book in books) {
            if (book.IsAvailable) {
                _notificationService.notify($"- Book Title: {book.Title}, Id: {book.Id}");
                bookDisplayed = true ;
            }
        }
        if (!bookDisplayed) { //if no dingle book is displayed 
            _notificationService.notify("No current available books");
        }
    }

    public List<Book> searchBooks(string keyword) {
        if(keyword == null) {
            _notificationService.notify("Please enter a valid keyword!!");
            return new List<Book>() ;   //return an empty list
        }

        //return the matched books list 
        return books.Where(book => book.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
    }
    

}
