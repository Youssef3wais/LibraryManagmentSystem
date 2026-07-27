using System.Runtime.CompilerServices;

namespace LibraryManagmentSystem;

public abstract class Person {
    private static int _lastId = 0 ;
    public int Id{get; init;}
    public string Name{set;get;}

    Person() {
    }
    public Person(string name) {
        Id = Interlocked.Increment(ref _lastId);
        Name = name ;
    }
    public static void SetLastId(int id) {
        _lastId = id;
    }
    public abstract override string ToString();
}
