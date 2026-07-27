namespace LibraryManagmentSystem;

class FileLogger : INotificationService {
    private readonly string _filePath;

    public FileLogger(string filePath) {
        _filePath = filePath;
    }

    public void notify(string message) {
        File.AppendAllText(_filePath, $"[{DateTime.Now}] {message}{Environment.NewLine}");
    }
}