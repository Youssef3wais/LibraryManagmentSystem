using System.Text.Json;
using LibraryManagmentSystem;

namespace JsonHandler;


public class JsonHandler<T>: IJsonHandler<T> {
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options;

    public JsonHandler(string filePath) {
        this._filePath = filePath;
        _options = new JsonSerializerOptions {
            WriteIndented = true
        };
    }

    public bool WriteListToFile(List<T> list) {                
        try {
            string json = JsonSerializer.Serialize(list, _options);
            File.WriteAllText(_filePath, json);
            Console.WriteLine($"JSON Serialization complete. Data saved to {_filePath}");
            return true ;
        }catch(Exception e) {
            Console.WriteLine($"JSON Serialization Failed. Error saving {_filePath}: {e.Message}");
            return false ;
        }
    }


    public List<T> ReadFileToList() {
        try {
            if (!File.Exists(_filePath)) return new List<T>();
            string json = File.ReadAllText(_filePath);
            if (string.IsNullOrEmpty(json)) {
                Console.WriteLine($"JSON Deserialization complete, File {_filePath} is Empty...");
                return new List<T>();
            }
            Console.WriteLine("JSON Deserialization complete");
            var x = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            return x;
        }catch(Exception e) {
            Console.WriteLine($"JSON Deserialization Failed. Error reading {_filePath}: {e.Message}");
            return new List<T>();
        }
        
    }

    //not used any more
    public bool AddItem(T item) {
        if (!File.Exists(_filePath)) {
            return WriteListToFile(new List<T>(){item});
        }
        List<T> currentData = this.ReadFileToList();
        
        currentData.Add(item);
        return WriteListToFile(currentData);
    }

    //not used any more
    public bool RemoveItem(T item) {
        if (!File.Exists(_filePath)) {
            return false ;
        }
        List<T> currentData = this.ReadFileToList();
        currentData.Remove(item);
        Console.WriteLine($"{item.ToString} Succesfully removed from the Json file");
        WriteListToFile(currentData);
        return true;
    }

}