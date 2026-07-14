namespace LibraryManagmentSystem;

public abstract class Person {
    public static int lastId = 0 ;
    public int Id{private set; get;}
    public string Name{set;get;}

    public Person(string name) {
        Id = ++lastId;
        Name = name ;
    }
    public abstract void displayInfo();
}
