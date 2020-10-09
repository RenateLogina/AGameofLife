namespace GameofLife
{
    using System;
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            GameManager gameUI = new GameManager();
            gameUI.GameMenu();
        }
    }
}
