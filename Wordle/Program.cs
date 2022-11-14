Console.Clear();



//declaring some variables
int guessedLeft = 5;


while (true)
{
    Console.ResetColor();
    string guessedWord = Console.ReadLine();

    //say if the guessed word is not valid
    if (isValid(guessedWord))
    {
        //put the guessed word in guessedWords Array
        GlobalVaribles.guessedWordList.Add(guessedWord);
        Console.Clear();

        //checks each char in each word of guessedWordList and gives them the right color
        foreach (var item in GlobalVaribles.guessedWordList)
        {
            char[] guessedWordCharArray = item.ToCharArray();
            for (var i = 0; i < 5; i++)
            {
                if (guessedWordCharArray[i] == GlobalVaribles.rightWordArray[i]) Console.ForegroundColor = ConsoleColor.Green;
                else if (GlobalVaribles.rightWordArray.Contains(guessedWordCharArray[i])) Console.ForegroundColor = ConsoleColor.Yellow;
                else Console.ResetColor();
                Console.Write(guessedWordCharArray[i]);
            }
            Console.Write("\n");
        }
        guessedLeft--;
    }
    else
    {
        //gives errorcode if the word is not valid
        Console.WriteLine("Word is not valid");
    }
    //check if the game is won
    if (guessedWord == GlobalVaribles.rightWord)
    {
        Console.WriteLine("DU VANN!");
        break;
        Console.ResetColor();
    }
    else if (guessedLeft <= 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("YOU LOST, the world was " + GlobalVaribles.rightWord);
        Console.ResetColor();
        break;
    }
}

static bool isValid(string word)
{
    if (GlobalVaribles.possibleWords.Contains(word) && !GlobalVaribles.guessedWordList.Contains(word))
    {
        return true;
    }
    else
    {
        return false;
    }
}

//class that stores public variables
public class GlobalVaribles
{
    public static string rightWord = "pussy";

    //making arrays 
    public static char[] rightWordArray = rightWord.ToCharArray();
    //making a array of all the possible words
    public static string[] possibleWords = File.ReadAllLines("words.txt");
    //making lists
    public static List<string> guessedWordList = new List<string>();

}