using System;
using System.Linq.Expressions;
using LibraryManagmentSystem;
using System.Linq;
using JsonHandler;
using System.Security.Cryptography.X509Certificates;

class Program {
    static void Main() {
        //program current path
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        
        //files pathes
        string booksJsonFilePath = Path.Combine(baseDir, "Data", "Books.json");
        string membersJsonFilePath = Path.Combine(baseDir, "Data", "Members.json");
        string logFilePath = Path.Combine(baseDir, "Data", "Data.log");

        //give paths to services 
        var notificationService = new ConsoleNotificationService();
        var logger = new FileLogger(logFilePath);
        var booksJsonFileHandler = new JsonHandler<Book>(booksJsonFilePath, logger);
        var membersJsonFileHandler = new JsonHandler<Member>(membersJsonFilePath, logger);


        
        Library library = new Library(notificationService, booksJsonFileHandler, membersJsonFileHandler);
        

        library.DisplayAvailableMembers();
        library.DisplayAvailableBooks();
        Console.WriteLine();
        



        while (true) {
            Console.WriteLine("==========================\nLibrary Management System\n==========================");
            Console.WriteLine("1. Add Book\n2. Register Member\n3. Borrow Book\n4. Return Book\n5. List Available Books\n6. Search Books\n7. Exit\n8. Remove book from the library\n9. List Available Members");
            
            
            Console.Write("Select option: ");
            string option = Console.ReadLine()??"";
            if (option == "1") {
                Console.Write("Enter book title: ");
                string title = Console.ReadLine()??"";
                Console.Write("Enter book author: ");
                string author = Console.ReadLine()??"";
                Console.Write("Enter book isbn: ");
                string isbn = Console.ReadLine()??"";
                library.AddBook(new Book(title, author, isbn, true));
            }else if (option == "2") {
                Console.Write("Enter member name: ");
                string name = Console.ReadLine()??"";
                library.RegisterMember(new Member(name));
            }else if (option == "3") {
                Console.Write("Enter member id: ");
                if ( !int.TryParse(Console.ReadLine(), out int memberId)) {
                    Console.Write("invalid id!!!");
                    continue;
                }
                Console.Write("Enter book id: ");
                if ( !int.TryParse(Console.ReadLine(), out int bookId)) {
                    Console.Write("invalid id!!!");
                    continue;
                }
                library.BorrowBook(memberId,bookId);
            }else if (option == "4") {
                Console.Write("Enter member id: ");
                if ( !int.TryParse(Console.ReadLine(), out int memberId)) {
                    Console.Write("invalid id!!!");
                    continue;
                }
                Console.Write("Enter book id: ");
                if ( !int.TryParse(Console.ReadLine(), out int bookId)) {
                    Console.Write("invalid id!!!");
                    continue;
                }
                library.ReturnBook(memberId, bookId);
            }else if (option == "5") {
                library.DisplayAvailableBooks();
            }else if (option == "6") {
                Console.Write("Enter search keyword: ");
                string keyword = Console.ReadLine()??"";
                Console.WriteLine("Matching book titles: ");
                foreach(Book book in library.SearchBooks(keyword)) {
                    Console.WriteLine($"- {book.Title}, Id: {book.Id}");
                }
            }else if (option == "7") {
                break;
            }else if (option == "8") {
                Console.Write("Enter book id: ");
                if ( !int.TryParse(Console.ReadLine(), out int bookId)) {
                    Console.Write("invalid id!!!");
                    continue;
                }
                library.RemoveBook(bookId);
            }else if (option == "9") {
                library.DisplayAvailableMembers();
            } else {
                Console.WriteLine("Invalid option, Please enter a valid number...");
            }
            Console.WriteLine();
            
        }






        Console.ReadKey();
    }



}