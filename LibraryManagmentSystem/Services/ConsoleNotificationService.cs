namespace LibraryManagmentSystem;

class ConsoleNotificationService : INotificationService {
    public void Notify(string message) {
        Console.WriteLine(message);
    }
}