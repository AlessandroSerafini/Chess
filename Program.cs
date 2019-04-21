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

            // Initialization of the chessboard drawing chessboard and loading pieces for both teams.
            chessboard.Init();

            /* Verify that game is still valid, making sure that none of the two teams has made checkmates against the opponent. */
            while (chessboard.CanIDoAnotherTurn)
            {
                if(!chessboard.CheckLastMoveColor) {
                    // Asking the user to make his move by entering valid coordinates.
                    do
                    {
                        UserMessage.WhereYouWantMovePiece(true);
                    } while (!chessboard.GetMoveCoordinates(true));
                }
                else {
                    do
                    {
                        UserMessage.WhereYouWantMovePiece(false);
                    } while (!chessboard.GetMoveCoordinates(false));
                }

            }

            // In the event that a user has made checkmates against the opponent, communicates the end of the game and the winner.
            UserMessage.UserWins(chessboard.GetCheckMate(false));
            Console.ReadKey();
        }
    }
}
