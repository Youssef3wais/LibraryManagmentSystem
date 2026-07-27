namespace LibraryManagmentSystem;

class EmailNotificationService : INotificationService {
    public void Notify(string message) {
        Console.WriteLine($"[Email] Sending:{message}");
    }
}