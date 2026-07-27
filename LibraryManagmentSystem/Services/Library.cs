namespace LibraryManagmentSystem;

using System.Diagnostics.CodeAnalysis;
using JsonHandler;
public class Library {
    private readonly INotificationService _notificationService ; 
    private readonly IJsonHandler<Book> _booksJsonHandler ;
    private readonly IJsonHandler<Member> _membersJsonHandler;

    private List<Book> books;
    private List<Member> members;

    public Library(INotificationService notificationService, IJsonHandler<Book> booksJsonHandler, IJsonHandler<Member> membersJsonHandler) {
        _notificationService = notificationService; 
        _booksJsonHandler = booksJsonHandler;
        _membersJsonHandler = membersJsonHandler;

        books = booksJsonHandler.ReadFileToList();
        members = membersJsonHandler.ReadFileToList();

        int lastBookId = books.Count > 0 ? books.Max(b => b.Id) : 0;       //fix the error the id resets when starting a new program 
        int lastMemberId = members.Count > 0 ? books.Max(b => b.Id) : 0;
        Book.SetLastId(lastBookId);
        Member.SetLastId(lastMemberId);
    }
    
    public bool AddBook(Book newBook) {
        bool bookAdded = false;
        foreach(Book book in books) {
            if(book.Isbn == newBook.Isbn) {
                _notificationService.Notify($"Duplicate ISBN, {newBook.Title} cant be added!!!");
                return bookAdded;
            }
        }
        books.Add(newBook);
        //booksJsonHandler.AddItem(newBook);
        _booksJsonHandler.WriteListToFile(books);    //take the books list write to the json file as it is
        bookAdded = true ;
        _notificationService.Notify($"{newBook.Title} added succesfully with id: {newBook.Id}");
        return bookAdded ;
    }

    public bool RemoveBook(int bookId) {
        //search using LINQ for the first matching bookId in books list
        Book? book = books.FirstOrDefault(book => book.Id == bookId);

        //if book cant found 
        if(book == null) {
            _notificationService.Notify($"book Id: {bookId} doesn't exist in the library!!!");
            return false ;
        }


        if (book.IsAvailable) {
            books.Remove(book);
            _booksJsonHandler.WriteListToFile(books);    //take the books list write to the json file as it is
            _notificationService.Notify($"{book.Title} successfully removed from library...");
            return true ;

        }else {             //if book exist but isnt available right now 
            _notificationService.Notify($"{book.Title} isn't available in the library right now!!!");
            return false ;
        }
    }
    public void RegisterMember(Member member) {
        members.Add(member);
        _membersJsonHandler.WriteListToFile(members);    //take the members list write to the json file as it is
        _notificationService.Notify($"{member.Name} succesfully registered, with Id {member.Id}");
    }

    public bool BorrowBook(int memberId, int bookId) {
        if (FindMemberAndBook(memberId, bookId, out Member? member, out Book? book)) {      //FindMemberAndBook print if couldnt find member id or a book id
            bool borrowed = member.BorrowBook(book);
            if (borrowed) 
            {
                _notificationService.Notify($"{member.Name} successfully borrowed {book.Title}");
                _membersJsonHandler.WriteListToFile(members);
                _booksJsonHandler.WriteListToFile(books);
                return true ;
            } else {
                _notificationService.Notify($"{member.Name} Couldn't borrow {book.Title}, it isn't available right now");
            }
        }
        return false;
    }
    
    public bool ReturnBook(int memberId, int bookId) {
        if (FindMemberAndBook(memberId, bookId, out Member? member, out Book? book)) {      //FindMemberAndBook print if couldnt find member id or a book id
            bool bookReturned = member.ReturnBook(book);
            if (bookReturned) {
                _notificationService.Notify($"{member.Name} succesfully returned {book.Title}");
                _membersJsonHandler.WriteListToFile(members); //take the members list write to the json file as it is
                _booksJsonHandler.WriteListToFile(books);    //take the books list write to the json file as it is
                return true;
            } else {
                _notificationService.Notify($"{book.Title} is not found in borrowed books by {member.Name}");
            }
        }
        return false;
    }

    public void DisplayAvailableBooks() {
        bool bookDisplayed = false ; //to check if any book is displayed 
        _notificationService.Notify("Current available Books: ");
        foreach(Book book in books) {
            if (book.IsAvailable) {
                _notificationService.Notify($"   - Book Title: {book.Title}, Id: {book.Id}");
                bookDisplayed = true ;
            }
        }
        if (!bookDisplayed) { //if no dingle book is displayed 
            _notificationService.Notify("No current available Books");
        }
    }
    public void DisplayAvailableMembers() {
        bool membersDisplayed = false ; //to check if any book is displayed 
        _notificationService.Notify("Current registered Members: ");
        foreach(Member member in members) {
            if (member != null) {
                _notificationService.Notify("  "+member.ToString());
                membersDisplayed = true ;
            }
        }
        if (!membersDisplayed) { //if no single member is displayed 
            _notificationService.Notify("No current available Members");
        }
    }

    public List<Book> SearchBooks(string keyword) {
        if(keyword == null) {
            _notificationService.Notify("Please enter a valid keyword!!");
            return new List<Book>() ;   //return an empty list
        }

        //return the matched books list 
        return books.Where(book => book.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    private bool FindMemberAndBook(int memberId, int bookId, [NotNullWhen(true)] out Member? member, [NotNullWhen(true)] out Book? book) {
        member = members.FirstOrDefault(member => member.Id == memberId);
        book = books.FirstOrDefault(book => book.Id == bookId);

        
        //if couldnt find member id in the member list 
        if (member == null) {
            _notificationService.Notify($"Member Id: {memberId}, Doesn't exist in the library");
        }

        if (book == null) {
            _notificationService.Notify($"book Id: {bookId} doesn't exist in the library!!!");
        }

        // if member id and book id both doesnt exist i want to display both messages then exit
        if(member == null || book == null) {
            return false ;
        }
        return true ;
    }

    

}
