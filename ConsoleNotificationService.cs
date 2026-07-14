namespace LibraryManagmentSystem;

class ConsoleNotificationService : INotificationService {
    public void notify(string message) {
        Console.WriteLine(message);
    }
}