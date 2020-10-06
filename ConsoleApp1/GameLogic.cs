
namespace GameofLife
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography.X509Certificates;

    public class GameLogic
    {
        public int MyProperty { get; set; }
        //public int CellCount;
        Random random = new Random();
        //pointerposition
        public int xPos;
        public int yPos;
        public int SizeX;
        public int SizeY;
        //stores array values
        public string[,] Generation;
        //Sets the very first iteration of the game, randomly
        public void SetSeed(int boardSize)
        {
            Console.WriteLine("   this means the SetSeed is called \u25CF");
            //set a 2D array size for the seed
            SizeX = 50 * boardSize - 3;
            SizeY = 14 + boardSize * boardSize;
            Generation = new string[SizeY, SizeX];
            //populate Generation with "empty" cells for test purposes
            for (int RowIndex = 0; RowIndex < SizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < SizeX; ColIndex++)
                {
                    Generation[RowIndex, ColIndex] = " ";
                }
            }
            //fills generation with seedlings on a field sized 6x6, randomly, positioned approximately in middle
            string[] Filler = new string[] { "\u25CF", " " };
            for (int RowIndex = SizeY/2-6; RowIndex < SizeY/2; RowIndex++)
            {
                for (int ColIndex = SizeX/2-6; ColIndex < SizeX/2; ColIndex++)
                {
                    int CharacterIndex = random.Next(Filler.Length);
                    Generation[RowIndex, ColIndex] = Filler[CharacterIndex];
                }
            }
        }
        public void PrintArray()
        {
            //prints the array
            xPos = 2;
            yPos = 6;
            for (int RowIndex = 0; RowIndex < SizeY; RowIndex++)//how tall
            {
                Console.SetCursorPosition(xPos, yPos);
                for (int ColIndex = 0; ColIndex < SizeX; ColIndex++)//how wide
                {
                    //prints a single character
                    Console.Write(Generation[RowIndex, ColIndex]);
                }
                yPos++;
                //Prints every new row in next line
                //Console.Write("\n");
            }
        }
    }
}
