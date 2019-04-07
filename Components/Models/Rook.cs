using System;

namespace Chess.Models
{
    public class Rook : Piece
    {
        public Rook(bool isWhite, int x, int y, Piece[,] matrix) : base(isWhite, x, y, matrix)
        {
            identifier = 'R';
        }

        public override bool CheckMove(Position end)
        {
            bool result = false;

            if (start.X == end.X) // vertical move
            {
                result = true;

                for (int i = 1; ((i < GetMoveAbsValue(end, 'Y')) && (result)); i++)
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
            else if (start.Y == end.Y) // horizontal move
            {
                result = true;

                for (int i = 1; ((i < GetMoveAbsValue(end, 'X')) && (result)); i++)
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
            
            return result ? IsPositionFreeOfAllies(end) : result;
        }
    }
}
