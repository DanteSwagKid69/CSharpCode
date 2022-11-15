//making a array that stores all places on the board
char[] boardArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

//The region that runs the game
#region  RunGame
//making a loop that runs the game
while (true)
{
    Console.Clear();

    PlayGame(1, boardArray);

    //Checking if the game has ended
    if (GameStateCheck(boardArray) != 0) break;
    Console.Clear();

    //Changing the color for player 2
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("PLAYER 2");

    //Drawing board
    Board(boardArray);

    //Letting player 2 play
    PlayGame(2, boardArray);

    //Checking if the game has ended
    if (GameStateCheck(boardArray) != 0) break;
}
#endregion RunGame

//Handeling what happens after the game has ended
#region GameEnd
Console.Clear();
if (GameStateCheck(boardArray) == 1)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Board(boardArray);
    Console.WriteLine("VICTORY!");
}
else if (GameStateCheck(boardArray) == -1)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Board(boardArray);
    Console.WriteLine("Tie");
}
Console.ResetColor();
#endregion GameEnd

//function that draws the game board
static void Board(char[] arr)
{
    Console.WriteLine("     |     |      ");
    Console.WriteLine("  {0}  |  {1}  |  {2}", arr[1], arr[2], arr[3]);
    Console.WriteLine("_____|_____|_____ ");
    Console.WriteLine("     |     |      ");
    Console.WriteLine("  {0}  |  {1}  |  {2}", arr[4], arr[5], arr[6]);
    Console.WriteLine("_____|_____|_____ ");
    Console.WriteLine("     |     |      ");
    Console.WriteLine("  {0}  |  {1}  |  {2}", arr[7], arr[8], arr[9]);
    Console.WriteLine("     |     |      ");
}

static int GameStateCheck(char[] arr)
{
    //Checking the vertical possibilities
    if (arr[1] == arr[4] && arr[4] == arr[7]) return 1;
    else if (arr[2] == arr[5] && arr[5] == arr[8]) return 1;
    else if (arr[3] == arr[6] && arr[6] == arr[9]) return 1;

    //Checking the vertical possibilities
    if (arr[1] == arr[2] && arr[2] == arr[3]) return 1;
    else if (arr[4] == arr[5] && arr[5] == arr[6]) return 1;
    else if (arr[7] == arr[8] && arr[8] == arr[9]) return 1;

    //Checking diagonal possibilities
    if (arr[1] == arr[5] && arr[5] == arr[9]) return 1;
    else if (arr[3] == arr[5] && arr[5] == arr[7]) return 1;
    //checking for draw
    else if (arr[1] != '1' && arr[2] != '2' && arr[3] != '3' && arr[4] != '4' && arr[5] != '5' && arr[6] != '6' && arr[7] != '7' && arr[8] != '8' && arr[9] != '9') return -1;
    else return 0;
}

//funtion that checks if the choosenTile is valid
static bool InputIsValid(string input)
{
    if (input.Length == 1 && int.TryParse(input, out int heltal))
    {
        int inputInt = int.Parse(input);
        if (inputInt > 0 && inputInt <= 9)
        {
            return true;
        }
        else return false;
    }
    else return false;
}

static void PlayGame(int player, char[] boardArray)
{
    //Declaring some variables 
    char symbolUsed;
    int choosenTile;

    if (player == 1) symbolUsed = 'X';
    else symbolUsed = 'O';

    Console.Clear();

    //Changing color for player depending on what player is playing
    if (player == 1) Console.ForegroundColor = ConsoleColor.Magenta;

    Console.WriteLine("PLAYER " + player);

    //drawing board
    Board(boardArray);

    //letting player 1 play
    Console.WriteLine($"Write the place you want to place {symbolUsed}: ");
    string choosenTileString = Console.ReadLine();

    //Check if the input is valid 
    while (!InputIsValid(choosenTileString))
    {
        Console.Clear();
        Console.WriteLine("PLAYER " + player);
        Board(boardArray);
        Console.WriteLine("The input is not valid, try again: ");
        choosenTileString = Console.ReadLine();
    }
    choosenTile = int.Parse(choosenTileString);

    //placeing a X in the place of the choosen tile
    //checking if the tile is already occupied
    while (boardArray[choosenTile] == 'X' || boardArray[choosenTile] == 'O')
    {
        Console.WriteLine("The place is occupied, try again: ");
        choosenTile = int.Parse(Console.ReadLine());
    }
    boardArray[choosenTile] = symbolUsed;
}