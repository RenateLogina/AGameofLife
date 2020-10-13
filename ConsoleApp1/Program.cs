namespace GameofLife
{
    using System;
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            GameManager gameManager = new GameManager();
            gameManager.GameMenu();
        }
    }
}
