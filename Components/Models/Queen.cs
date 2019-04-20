using System;

namespace Chess.Models
{
    public class Queen : Piece
    {
        public Queen(bool isWhite, int x, int y, Piece[,] matrix) : base(isWhite, x, y, matrix)
        {
            identifier = '♛';
        }

        /* This method overrides the relative method defined in the parent class
        by applying specific rules on the possibility of doing certain kinds of moves
        belonging to the Queen piece. */
        public override bool CheckMove(Position end)
        {
            bool result = false;
            
            /* Getting how many cells the piece is moved horizontally and vertically */
            int absX = GetMoveAbsValue(end, 'X');
            int absY = GetMoveAbsValue(end, 'Y');

            // If is a vertical move
            if (start.X == end.X)
            {
                result = true;

                /* Verifying that in the route, whether you move
                upwards or downwards, all the cells are empty. */
                for (int i = 1; ((i < absY) && (result)); i++)
                {
                    if (start.Y < end.Y)
                    {
                        result = matrix[start.X, start.Y + i] == null;
                    }
                    else if (start.Y > end.Y)
                    {
                        result = matrix[start.X, start.Y - i] == null;
                    }
                }
            }
            // If is an horizontal move
            else if (start.Y == end.Y)
            {
                result = true;

                /* Verifying that in the route, whether you move
                left or right, all the cells are empty. */
                for (int i = 1; ((i < absX) && (result)); i++)
                {
                    if (start.X < end.X)
                    {
                        result = matrix[start.X + i, start.Y] == null;
                    }
                    else if (start.X < end.X)
                    {
                        result = matrix[start.X - i, start.Y] == null;
                    }
                }
            }
            // If is a diagonal move
            else if (absX == absY)
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
