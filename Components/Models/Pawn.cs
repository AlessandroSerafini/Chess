using System;

namespace Chess.Models
{
    public class Pawn : Piece
    {
        bool moved; // if moved or not, the pawn has different logic

        public Pawn(bool isWhite, int x, int y, Piece[,] matrix) : base(isWhite, x, y, matrix)
        {
            identifier = 'P';
            moved = false;
        }

        public override bool CheckMove(Position end)
        {
            bool result = false;

            int absX = GetMoveAbsValue(end, 'X');
            int absY = GetMoveAbsValue(end, 'Y');

            // vertical move
            if (start.X == end.X) 
            {
                // if not already moved, can be 2 position upwards
                if (absY == 2 && !moved) 
                { 
                    if (isWhite) {
                        result = !((matrix[end.X, end.Y] != null) || (matrix[end.X, end.Y - 1] != null));
                    } else {
                        result = !((matrix[end.X, end.Y] != null) || (matrix[end.X, end.Y + 1] != null));
                    }
                } 
                else if (absY == 1) 
                {
                    result = isWhite ? (start.Y < end.Y) : (start.Y > end.Y);

                    if (result) {
                        if (matrix[end.X, end.Y] == null) {
                            result = IsPositionFreeOfAllies(end);
                        } else {
                            result = false;
                        }
                    }
                }
            } 
            else if (absX == 1 && absY == 1) // diagonal move to kill enemy
            {
                result = matrix[end.X, end.Y] != null && matrix[end.X, end.Y].IsWhite != isWhite;
            }

            if (result) {
                moved = true;
            }

            return result;
        }
    }
}
