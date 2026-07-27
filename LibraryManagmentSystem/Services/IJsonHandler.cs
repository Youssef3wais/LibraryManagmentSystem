namespace JsonHandler;

public interface IJsonHandler<T>
{
    List<T> ReadFileToList();
    bool WriteListToFile(List<T> items);
}