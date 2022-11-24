
Console.Clear();

HighScoreHandler(Console.ReadLine());

static void HighScoreHandler(string inputString)
{
    //Varibles
    string fileName = "SavedScores.txt";

    //debugg
    if (inputString == "debug info")
    {
        Console.WriteLine(HighScore.Info());
        Environment.Exit(0);
    }

    if (int.Parse(inputString) > int.Parse(HighScore.score))
    {
        HighScore.AddScore(int.Parse(inputString));
        Console.WriteLine("NEW HIGHSCORE!");
        Console.WriteLine("Write your name: ");
        HighScore.AddName(Console.ReadLine());
    }
    else
    {
        Console.WriteLine("The number is smaller than " + HighScore.score);
    }
}


public class HighScore
{
    public static string name = File.ReadAllText("SavedNames.txt");
    public static string score = File.ReadAllText("SavedScores.txt");

    public static void AddScore(int newScore)
    {
        File.WriteAllText("SavedScores.txt", newScore.ToString());
    }

    public static void AddName(string newName)
    {
        File.WriteAllText("SavedNames.txt", newName);
    }

    public static string Info()
    {
        return "Highscore holder is " + HighScore.name + " With the score " + HighScore.score;
    }
}


