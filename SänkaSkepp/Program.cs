using System;
using System.Collections.Generic;
using System.Linq;

namespace SänkaSkepp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            int mapLegnth = 5;

            //making a array that storas all real data
            Tile[,] realMapArrayPlayer1 = tileArrayMaker(mapLegnth, false, 5);
            Tile[,] realMapArrayPlayer2 = tileArrayMaker(mapLegnth, false, 5);
            // making a array that only shows ships that have been hit, and hides the ones that are still active
            Tile[,] fakeMapArrayPlayer1 = tileArrayMaker(mapLegnth, false, 0);
            Tile[,] fakeMapArrayPlayer2 = tileArrayMaker(mapLegnth, false, 0);

            Tutorial();
            Console.ReadKey();
            Console.Clear();

            //placing ships for player 1
            Console.WriteLine("Spelare 1");
            placeShip(realMapArrayPlayer1, fakeMapArrayPlayer1, 5);
            Console.Clear();
            drawTileArray(realMapArrayPlayer1);
            Console.WriteLine("Det här är hur dina skepp är placerade!\nTryck på valfri knapp för att låta spelare 2 placera sina skepp");
            Console.ReadKey();
            Console.Clear();

            //placing ships for player 2
            Console.WriteLine("Spelare 2");
            placeShip(realMapArrayPlayer2, fakeMapArrayPlayer2, 5);
            Console.Clear();
            drawTileArray(realMapArrayPlayer2);
            Console.ReadKey();
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Player 1:s turn to shoot");
                Shoot(realMapArrayPlayer2, fakeMapArrayPlayer2);
                Console.WriteLine("Press any key to start player 2:s turn");
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine("Player 2:s turn to shoot");
                Shoot(realMapArrayPlayer1, fakeMapArrayPlayer1);
                Console.WriteLine("Press any key to start player 2:s turn");
                Console.ReadKey();
                Console.Clear();
            }

        }

        static void Shoot(Tile[,] tileArray, Tile[,] fakeMapArray)
        {
            char[] inputArray;
            //PLAN:
            //take coordinates for the tile that is shot on
            //making a name for each ship that is generatied
            Console.Clear();
            drawTileArray(fakeMapArray);

            //writing all instruction and taking input for placing a ship
            Console.Write("Skriv in en position du vill skuta: ");
            string positionString = Console.ReadLine();


            while (!EachNumberIsGood(positionString, 5))
            {
                Console.WriteLine("Ej giltigt tal, försök igen:");
                positionString = Console.ReadLine();
            }

            inputArray = stringToArray(positionString);
            //each character is internally represented by a number, therefore by finding the difference between char 0 and char inputArray[o] is right
            int xPos = (inputArray[0] - '0') - 1;
            int yPos = (inputArray[1] - '0') - 1;

            //check if a ship coordanite is matching the given coordinates
            //destroy the ship if that is the case 
            if (tileArray[yPos, xPos].isOccupied())
            {
                tileArray[yPos, xPos].makeUnOccupied();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("DU TRÄFFADE ETT SKEPP");
                Console.ResetColor();
                tileArray[yPos, xPos].MakeHit();
                fakeMapArray[yPos, xPos].MakeHit();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("DU MISSADE!");
                Console.ResetColor();
            }

            //change a value that indicates how many ships are left 
        }

        static void Tutorial()
        {
            Console.WriteLine("GOAL");
            Console.WriteLine("-----------");
            Console.WriteLine("The goal of the game is too shoot all the places that your opponent has placed a ship on.");
            Console.WriteLine("\nCONTROLS:");
            Console.WriteLine("-----------");
            Console.WriteLine("Place ships and choose:\nwrite two numbers where both numbers are in range of the size of the map");
            Console.WriteLine("eg. 34, the first number (3) is the y-axis and the second number (4) us the x-axis");
        }

        static void placeShip(Tile[,] tileArray, Tile[,] fakeMapArray, int amountOfShips)
        {
            char[] inputArray;

            while (amountOfShips >= 0)
            {
                //making a name for each ship that is generatied
                if (amountOfShips == 0)
                {
                    break;
                }
                Console.Clear();
                drawTileArray(tileArray);

                //writing all instruction and taking input for placing a ship
                Console.Write("Skriv in en position: ");
                string positionString = Console.ReadLine();


                while (!EachNumberIsGood(positionString, 5))
                {
                    Console.WriteLine("Ej giltigt tal, försök igen:");
                    positionString = Console.ReadLine();
                }

                inputArray = stringToArray(positionString);
                //each character is internally represented by a number, therefore by finding the difference between char 0 and char inputArray[o] is right
                int xPos = (inputArray[0] - '0') - 1;
                int yPos = (inputArray[1] - '0') - 1;

                //check if tile is already occupied
                while (tileArray[yPos, xPos].isOccupied())
                {
                    Console.WriteLine("Plattsen är redan upptagen, skriv in en annan:");
                    inputArray = stringToArray(Console.ReadLine());

                    xPos = (inputArray[0] - '0') - 1;
                    yPos = (inputArray[1] - '0') - 1;
                }

                tileArray[yPos, xPos].makeOccupied();
                amountOfShips--;
            }
        }

        public class Ship
        {
            int shipSize;
            int yPos;
            int xPos;

            public Ship(int _shipSize, int _yPos, int _xPos)
            {
                shipSize = _shipSize;
                yPos = _yPos;
                xPos = _xPos;
            }

            //method that returns the current size of the ship
            public int thisShipSize()
            {
                return shipSize;
            }
        }

        static Tile[,] tileArrayMaker(int legnth, bool isRandom, int amountOfShips)
        {
            //making a array
            Tile[,] newArray = new Tile[legnth, legnth];

            for (int i = 0; i < newArray.GetLength(0); i++)
            {

                for (int j = 0; j < newArray.GetLength(1); j++)
                {
                    newArray[i, j] = new Tile(0);
                }
            }

            if (isRandom)
            {
                Random randomGen = new Random();
                while (amountOfShips > 0)
                {
                    int x = randomGen.Next(0, newArray.GetLength(1));
                    int y = randomGen.Next(0, newArray.GetLength(0));

                    if (!newArray[y, x].isOccupied())
                    {
                        newArray[y, x].makeOccupied();
                        if (amountOfShips > 0)
                        {
                            amountOfShips--;
                        }
                    }
                }
            }
            return newArray;
        }

        static void drawTileArray(Tile[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[j, i].TileValue());
                }
                Console.Write("\n");
            }
        }

        //function to take a string input, then split it into char, convert into two seperate integers
        // PLAN: input.ToCharArray -> 
        static char[] stringToArray(string input)
        {
            char[] array = new char[2];
            array = input.ToCharArray();
            return array;
        }

        //funtion that checkes if the input is a integer and sends out a error code if its not
        static string CheckIfInt(string input)
        {
            while (!int.TryParse(input, out int integer))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Du skrev inte in ett heltal, försök igen: ");
                input = Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            return input;
        }

        static bool EachNumberIsGood(string inputString, int max)
        {
            //Method bool som kollar så talen inte är för stora eller för små mm.
            //skapar en array som sedan används till utplockning av char
            char[] charArray = new char[2];
            //kolla om det är två tal enbart
            if (inputString.Length == 2)
            {
                //kolla om det är ett heltal
                if (int.TryParse(inputString, out int heltal))
                {
                    //gör om inputString till char array
                    charArray = inputString.ToCharArray();
                    //for loop för vardera char
                    int points = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        //gör om char till int 
                        int value = charArray[i] - '0';

                        //kolla om talet är större än xLegnth och yLegnth
                        if (value >= 0 && value <= max && i == 0)
                        {
                            points += 1;
                        }
                        if (value >= 0 && value <= max && i == 1)
                        {
                            points += 1;
                        }
                    }
                    if (points == 2)
                    {
                        return true;
                    }
                }
            }
            //spotta ut en bool 
            return false;
        }

        public class GameManager
        {
            int yLegnth;
            int xLegnth;
            int[,] realMap;
            int[,] drawnMap;

            //method that declares the input variables and making a new array with it
            public GameManager(int _yLegnth, int _xLegnth)
            {
                yLegnth = _yLegnth;
                xLegnth = _xLegnth;
                realMap = new int[yLegnth, xLegnth];
                drawnMap = new int[yLegnth, xLegnth];
            }

            //funktion som fyller en tvådimentionel array med ett nummer
            public void FillMap()
            {
                for (int i = 0; i < realMap.GetLength(0); i++)
                {
                    for (int j = 0; j < realMap.GetLength(1); j++)
                    {
                        realMap[i, j] = 0;
                    }
                }

                for (int i = 0; i < drawnMap.GetLength(0); i++)
                {
                    for (int j = 0; j < drawnMap.GetLength(1); j++)
                    {
                        drawnMap[i, j] = 0;
                    }
                }
            }

            //method that draws the array in the console
            public void DrawMap()
            {
                drawnMap = new int[yLegnth, xLegnth];

                for (int i = 0; i < realMap.GetLength(0); i++)
                {
                    for (int j = 0; j < realMap.GetLength(1); j++)
                    {
                        Console.Write(realMap[i, j]);
                    }
                    Console.Write("\n");
                }
            }

            public void DrawShownMap()
            {
                for (int i = 0; i < drawnMap.GetLength(0); i++)
                {
                    for (int j = 0; j < drawnMap.GetLength(1); j++)
                    {
                        Console.Write(drawnMap[i, j]);
                    }
                    Console.Write("\n");
                }
            }

            public void randomMap(int amountOfShips)
            {
                //randomiserar ett antal plattser som ska ha skepp
                Random randomGen = new Random();
                while (amountOfShips > 0)
                {
                    int x = randomGen.Next(0, realMap.GetLength(1));
                    int y = randomGen.Next(0, realMap.GetLength(0));

                    if (realMap[y, x] != 1)
                    {
                        realMap[y, x] = 1;
                        if (amountOfShips > 0)
                        {
                            amountOfShips--;
                        }
                    }
                }
            }
            //method to that handles the placement of the ships
            public void placeShip(char[] inputArray)
            {
                //each character is internally represented by a number, therefore by finding the difference between char 0 and char inputArray[o] is right
                int xPos = (inputArray[0] - '0') - 1;
                int yPos = (inputArray[1] - '0') - 1;

                //check if the position is already occupied

                drawnMap[yPos, xPos] = 1;
                realMap[yPos, xPos] = 1;
            }
        }

        public class Tile
        {
            int value;
            public Tile(int _value)
            {
                value = _value;
            }

            public bool isOccupied()
            {
                if (value == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void makeOccupied()
            {
                value = 1;
            }

            public void makeUnOccupied()
            {
                value = 0;
            }

            public int TileValue()
            {
                return value;
            }

            public void MakeHit()
            {
                value = 3;
            }
        }
    }
}

