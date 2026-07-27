using System.Text.Json;
using LibraryManagmentSystem;

namespace JsonHandler;


public class JsonHandler<T>: IJsonHandler<T> {
    private readonly string _JsonfilePath;
    private readonly JsonSerializerOptions _options;
    private readonly INotificationService _notificationService;

    public JsonHandler(string JsonfilePath, INotificationService logger) {
        this._JsonfilePath = JsonfilePath;
        this._notificationService = logger ;
        _options = new JsonSerializerOptions {
            WriteIndented = true
        };
    }

    public bool WriteListToFile(List<T> list) {                
        try {
            string json = JsonSerializer.Serialize(list, _options);
            File.WriteAllText(_JsonfilePath, json);
            _notificationService.Notify($"JSON Serialization complete. List Data saved to {_JsonfilePath}");
            return true ;
        }catch(Exception e) {
            _notificationService.Notify($"JSON Serialization Failed. Error saving {_JsonfilePath}: {e.Message}");
            return false ;
        }
    }


    public List<T> ReadFileToList() {
        try {
            if (!File.Exists(_JsonfilePath)) return new List<T>();
            string json = File.ReadAllText(_JsonfilePath);
            if (string.IsNullOrEmpty(json)) {
                _notificationService.Notify($"JSON Deserialization complete, File {_JsonfilePath} is Empty...");
                return new List<T>();
            }
            _notificationService.Notify($"{_JsonfilePath} Deserialization complete");
            var x = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            return x;
        }catch(Exception e) {
            _notificationService.Notify($"JSON Deserialization Failed. Error reading {_JsonfilePath}: {e.Message}");
            return new List<T>();
        }
        
    }

}