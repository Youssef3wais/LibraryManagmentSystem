using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using JsonHandler;

namespace LibraryManagmentSystem;

public class Member : Person {

    
    private readonly List<Book> _books = new List<Book>();
    
    [JsonInclude]
    public IReadOnlyList<Book> Books {
        get => _books.AsReadOnly();
        init {
            _books.Clear();
            _books.AddRange(value);
        }
    }

    public Member(string name): base(name) {
    }
    
    public bool BorrowBook(Book book) {
        if (book.IsAvailable) {
            _books.Add(book);
            book.MarkBorrowed(); // when some one borrow a book changes to not available
            return true;
        }
        return false ;
    }
    public bool ReturnBook(Book book) {
        bool removed = _books.Remove(book);
        if (removed) {
            book.MarkReturned(); ;
            return true ;
        }

        return false ;
    }
    public override string ToString() {
        string result = $"- Member name: {this.Name}, Id: {this.Id}";
        if (Books.Count < 1) {
            return result;
        } else {
            result += "\n      Current Borrowed books:";
            for(int i=0; i<Books.Count;i++) {
                result += $"\n           {i+1}: {Books[i].Title}";
            }
        }
        return result;
    }



}
