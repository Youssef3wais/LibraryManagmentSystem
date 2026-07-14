namespace LibraryManagmentSystem;

class EmailNotificationService : INotificationService {
    public void notify(string message) {
        Console.WriteLine($"[Email] Sending:{message}");
    }
}