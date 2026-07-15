using System;
using System.Linq.Expressions;
using LibraryManagmentSystem;
using System.Linq;

class Program {
    static void Main() {
        Library library = new Library(new ConsoleNotificationService());

        while (true) {
            Console.WriteLine("==========================\nLibrary Management System\n==========================");
            Console.WriteLine("1. Add Book\n2. Register Member\n3. Borrow Book\n4. Return Book\n5. List Available Books\n6. Search Books\n7. Exit");
            
            
            Console.Write("Select option: ");
            string option = Console.ReadLine()??"";
            if (option == "1") {
                Console.Write("Enter book title: ");
                string title = Console.ReadLine()??"";
                Console.Write("Enter book author: ");
                string author = Console.ReadLine()??"";
                Console.Write("Enter book isbn: ");
                string isbn = Console.ReadLine()??"";
                library.addBook(new Book(title, author, isbn, true));
            }else if (option == "2") {
                Console.Write("Enter member name: ");
                string name = Console.ReadLine()??"";
                library.registerMember(new Member(name));
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
                library.borrowBook(memberId,bookId);
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
                library.returnBook(memberId, bookId);
            }else if (option == "5") {
                library.displayAvailableBooks();
            }else if (option == "6") {
                Console.Write("Enter search keyword: ");
                string keyword = Console.ReadLine();
                Console.WriteLine("Matching book titles: ");
                foreach(Book book in library.searchBooks(keyword)) {
                    Console.WriteLine($"- {book.Title}, Id: {book.Id}");
                }
            }else if (option == "7") {
                break;
            } else {
                Console.WriteLine("Invalid option, Please enter a valid number...");
            }
            Console.WriteLine();
            
        }






        Console.ReadKey();
    }
}