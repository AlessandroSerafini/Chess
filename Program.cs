using System;
using Chess.Components;

namespace Chess
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Creating a new chessboard object belonging to the Chessboard class.
            Chessboard chessboard = new Chessboard();

            // Initialization of the chessboard loading pieces for both teams.
            chessboard.Init();
            
            // Draw the playing field.
            chessboard.Draw();

            /* Verify that game is still valid, making sure that none of the two teams has made checkmates against the opponent. */
            while (!chessboard.CheckMateWhite && !chessboard.CheckMateBlack)
            {
                // Asking the user to make his move by entering valid coordinates.
                do
                {
                    Helper.GetPiece(true);
                } while (!chessboard.ReadMove(true));

                Console.WriteLine();

                do
                {
                    Helper.GetPiece(false);
                } while (!chessboard.ReadMove(false));

                Console.WriteLine();
            }

            // In the event that a user has made checkmates against the opponent, communicates the end of the game and the winner.
            Helper.GameEnd(chessboard.CheckMateBlack);
            Console.ReadKey();
        }
    }
}
