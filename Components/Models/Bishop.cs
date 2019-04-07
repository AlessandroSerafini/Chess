using System;

namespace Chess.Models
{
    public class Bishop : Piece
    {
        public Bishop(bool isWhite, int x, int y, Piece[,] matrix) : base(isWhite, x, y, matrix)
        {
            identifier = 'B';
        }

        public override bool CheckMove(Position end)
        {
            bool result = false;

            int absX = GetMoveAbsValue(end, 'X');
            int absY = GetMoveAbsValue(end, 'Y');

            // diagonal move
            if (absX == absY)
            {
                result = true;

                // check if all empty cells on the way
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

            return result ? IsPositionFreeOfAllies(end) : result;
        }
    }
}
