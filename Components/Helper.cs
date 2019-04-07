using System;
namespace Chess.Components
{
    public static class Helper
    {
        /* This method takes care to ask the user of a specific color to select
        the piece with which makes the move by printing a message on the screen. */
        public static void GetPiece(bool isWhite)
        {
            Console.ForegroundColor = isWhite ? ConsoleColor.DarkGreen : ConsoleColor.Blue;
            string player = isWhite ? "WHITE" : "BLACK";
            Console.Write("[" + player + "], select the piece to move (X,Y): ");
            Console.ResetColor();
        }

        /* This method takes care to ask the user of a specific color to select
        the position where to move the piece by printing a message on the screen. */
        public static void AskMove(bool isWhite)
        {
            Console.ForegroundColor = isWhite ? ConsoleColor.DarkGreen : ConsoleColor.Blue;
            string player = isWhite ? "WHITE" : "BLACK";
            Console.Write("[" + player + "], select the position where to move the piece (X,Y): ");
            Console.ResetColor();
        }

        /* This method takes care to communicate with the user by printing a message on the screen. */
        public static void Info(string message, ConsoleColor color = ConsoleColor.White) 
        {
            Console.ForegroundColor = color;
            Console.WriteLine("[INFO] " + message);
            Console.ResetColor();
        }

        /* This method takes care to alert the user that the game is ended with the winning
        of a specific team by printing a message on the screen. */
        public static void GameEnd(bool isWhite)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string player = isWhite ? "WHITE" : "BLACK";
            Console.WriteLine("[CHECK MATE] " + player + " won the game!");
            Console.ResetColor();
        }

        /* This method takes care to communicate an error to the user by printing a message on the screen. */
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[ERROR] " + message);
            Console.ResetColor();
        }
    }
}
