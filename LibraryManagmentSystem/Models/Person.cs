using System.Runtime.CompilerServices;

namespace LibraryManagmentSystem;

public abstract class Person {
    private static int lastId = 0 ;
    public int Id{get; init;}
    public string Name{set;get;}

    Person() {
    }
    public Person(string name) {
        Id = ++lastId;
        Name = name ;
    }
    public static void setLastId(int id) {
        lastId = id;
    }
    public abstract void displayInfo();
}
