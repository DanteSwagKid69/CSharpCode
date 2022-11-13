
Console.Clear();



Console.Clear();
Console.WriteLine("Write the room number of the room that you want to change: ");
int targetroom = int.Parse(Console.ReadLine());

Console.WriteLine(targetroom);

Console.WriteLine("Skriv in namnet på den som bokar");
string name = Console.ReadLine();

Console.WriteLine(name);

roomArray[targetroom].makeBooking("hej", 20);





public class Room
{
    bool isOccupied = false;
    string names;
    int numberOfPeople;

    public string Names()
    {
        return names;
    }

    public void makeBooking(string theirNames, int amountOfPeople)
    {
        isOccupied = true;
        names = theirNames;
        numberOfPeople = amountOfPeople;
    }

    public void removeBooking()
    {
        isOccupied = false;
        names = "";
    }

    public void info()
    {
        Console.WriteLine("Room Number: ");
        Console.WriteLine("Name: " + names);
        Console.WriteLine("Number of people: " + numberOfPeople);
    }
}