namespace GameofLife
{
    using System;
    /// <summary>
    /// Startpoint of the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts the main game menu located in GameManager.
        /// </summary>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            GameManager gameManager = new GameManager(new Serializer(), new GameUI());
            gameManager.StartGame();
        }
    }
}
