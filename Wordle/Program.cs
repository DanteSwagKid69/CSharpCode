Console.Clear();



//making a array of all the possible words
string[] possibleWords = File.ReadAllLines("words.txt");
//making lists
List<string> guessedWordList = new List<string>();

Random random = new Random();
string rightWord = possibleWords[random.Next(1, possibleWords.Length)];

char[] rightWordArray = rightWord.ToCharArray();

//declaring some variables
int guessedLeft = 5;


while (true)
{
    Console.ResetColor();
    string guessedWord = Console.ReadLine();

    //say if the guessed word is not valid
    if (isValid(guessedWord, possibleWords, guessedWordList))
    {
        //put the guessed word in guessedWords Array
        guessedWordList.Add(guessedWord);
        Console.Clear();

        //checks each char in each word of guessedWordList and gives them the right color
        foreach (var item in guessedWordList)
        {
            char[] guessedWordCharArray = item.ToCharArray();
            for (var i = 0; i < 5; i++)
            {
                if (guessedWordCharArray[i] == rightWordArray[i]) Console.BackgroundColor = ConsoleColor.Green;
                else if (rightWordArray.Contains(guessedWordCharArray[i])) Console.BackgroundColor = ConsoleColor.Yellow;
                else Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(guessedWordCharArray[i]);
                Console.ResetColor();
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
    if (guessedWord == rightWord)
    {
        Console.WriteLine("DU VANN!");
        break;
        Console.ResetColor();
    }
    else if (guessedLeft <= 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("YOU LOST, the world was " + rightWord);
        Console.ResetColor();
        break;
    }
}

static bool isValid(string word, string[] possibleWords, List<string> guessedWordList)
{
    if (possibleWords.Contains(word) && !guessedWordList.Contains(word))
    {
        return true;
    }
    else
    {
        return false;
    }
}