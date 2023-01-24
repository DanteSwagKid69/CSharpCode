using Raylib_cs;

int screenWidth = 800;
int screenHeight = 800;
int boardWidth = 350;
int boardHeight = boardWidth * 2;

int gridSize = boardWidth / 10;

int boardStartPosX = (screenWidth - boardWidth) / 2;
int boardStartPosY = screenHeight - boardHeight - 10;

int[,] boardArr = new int[boardHeight / gridSize, boardWidth / gridSize];
char[,] colorArr = new char[boardArr.GetLength(0), boardArr.GetLength(1)];

int xPos = 3;
int yPos = 3;

char currentColor = 'p';
char[] allColorsArray = { 'v', 'y', 'p', 'g', 'b', 'r', 'o' };

boardArr[yPos, xPos] = 1;
colorArr[yPos, xPos] = currentColor;

Random ranGen = new Random();


Raylib.InitWindow(screenHeight, screenWidth, "Game Window");
Raylib.SetTargetFPS(20);
int frames = 0;


//Main
while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.GRAY);

    //Draw the screen
    Raylib.DrawRectangle((screenWidth - boardWidth) / 2, screenHeight - boardHeight - 10, boardWidth, boardHeight, Color.BLACK);

    for (int y = 0; y < boardArr.GetLength(0); y++)
    {
        for (int x = 0; x < boardArr.GetLength(1); x++)
        {
            if (boardArr[y, x] == 1)
            {
                Raylib.DrawRectangle(x * gridSize + boardStartPosX, y * gridSize + boardStartPosY, gridSize, gridSize, ColorPicker(colorArr, y, x));
            }
        }
    }

    if (yPos != boardArr.GetLength(0) - 1 && !checkCollision(boardArr, yPos, xPos))
    {
        RemoveLastPiece(boardArr, colorArr, yPos, xPos);
    }


    //Movement 
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT) && !CollisionX(boardArr, yPos, xPos, 1))
    {
        RemoveLastPiece(boardArr, colorArr, yPos, xPos);
        xPos++;
    }
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT) && !CollisionX(boardArr, yPos, xPos, -1))
    {
        RemoveLastPiece(boardArr, colorArr, yPos, xPos);
        xPos--;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)) Raylib.SetTargetFPS(400);
    else Raylib.SetTargetFPS(20);


    //Removing last piece and placing a new one under it
    if (frames == 10)
    {
        if (yPos != boardArr.GetLength(0) - 1 && !checkCollision(boardArr, yPos, xPos))
        {
            yPos++;
        }
        else
        {
            yPos = 3;
            currentColor = allColorsArray[ranGen.Next(7)];
        }

        frames = 0;
    }




    boardArr[yPos, xPos] = 1;
    colorArr[yPos, xPos] = currentColor;


    CheckForRow(boardArr, colorArr);

    //Debug
    Console.Clear();
    Console.WriteLine("y: " + yPos + " x: " + xPos + " frames: " + frames);
    DrawDebugBoard(boardArr);
    frames++;
    Raylib.EndDrawing();
}

Raylib.CloseWindow();

static void DrawDebugBoard(int[,] boardArr)
{
    for (int y = 0; y < boardArr.GetLength(0); y++)
    {
        for (int x = 0; x < boardArr.GetLength(1); x++)
        {
            Console.Write(boardArr[y, x]);
        }
        Console.Write("\n");
    }
}

static void CheckForRow(int[,] boardArr, char[,] colorArr)
{
    int rowOccupied = 0;
    int targetY = 0;

    for (int y = 0; y < boardArr.GetLength(0); y++)
    {
        for (int x = 0; x < boardArr.GetLength(1); x++)
        {
            if (boardArr[y, x] == 1) rowOccupied++;
        }
        if (rowOccupied == boardArr.GetLength(1))
        {
            targetY = y;
            for (int i = 0; i < 10; i++)
            {
                boardArr[targetY, i] = 0;
                colorArr[targetY, i] = 'b';
            }
        }
        rowOccupied = 0;
    }
}

static void RemoveLastPiece(int[,] boardArr, char[,] colorArr, int yPos, int xPos)
{
    boardArr[yPos, xPos] = 0;
    colorArr[yPos, xPos] = 'b';
}

static bool checkCollision(int[,] boardArr, int yPos, int xPos)
{
    if (boardArr[yPos + 1, xPos] == 1) return true;
    else return false;
}

static bool CollisionX(int[,] boardArr, int yPos, int xPos, int dir)
{
    if (boardArr[yPos, xPos + dir] == 1) return true;
    else return false;
}

static Color ColorPicker(char[,] colorArr, int yPos, int xPos)
{
    switch (colorArr[yPos, xPos])
    {
        case 'v':
            return Color.VIOLET;
        case 'y':
            return Color.YELLOW;
        case 'p':
            return Color.PURPLE;
        case 'g':
            return Color.GREEN;
        case 'b':
            return Color.BLUE;
        case 'r':
            return Color.RED;
        case 'o':
            return Color.ORANGE;
    }
    return Color.BLACK;
}
