using System;

namespace Chess.Models
{
    public class Bishop : Piece
    {
        public Bishop(bool isWhite, int x, int y, Piece[,] matrix) : base(isWhite, x, y, matrix)
        {
            identifier = 'B';
        }

        /* This method overrides the relative method defined in the parent class
        by applying specific rules on the possibility of doing certain kinds of moves
        belonging to the Bishop piece. */
        public override bool CheckMove(Position end)
        {
            bool result = false;

            /* Getting how many cells the piece is moved horizontally and vertically */
            int absX = GetMoveAbsValue(end, 'X');
            int absY = GetMoveAbsValue(end, 'Y');

            // If is a diagonal move
            if (absX == absY)
            {
                result = true;

                // I can go on as long as there are empty cells in my way
                for (int i = 1; ((i < absX) && (result)); i++)
                {
                    if (start.X < end.X)
                    {
                        result = start.Y < end.Y ?
                                      !(matrix[start.X + i, start.Y + i] != null) :
                                      !(matrix[start.X + i, start.Y - i] != null);
                    }
                    else
                    {
                        result = start.Y < end.Y ?
                                      !(matrix[start.X - i, start.Y + i] != null) :
                                      !(matrix[start.X - i, start.Y - i] != null);
                    }
                }
            }

            /* Checking that there isn't already a piece of mine in the place where I want to move. */
            return result ? IsPositionFreeOfAllies(end) : result;
        }
    }
}
