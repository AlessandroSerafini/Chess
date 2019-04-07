using System;

namespace Chess.Models
{
    public class Knight : Piece
    {
        public Knight(bool isWhite, int x, int y, Piece[,] matrix) : base(isWhite, x, y, matrix)
        {
            identifier = '♞'; // horse
        }

        public override bool CheckMove(Position end)
        {
            int absX = GetMoveAbsValue(end, 'X');
            int absY = GetMoveAbsValue(end, 'Y');

            return ((absX == 2) && (absY == 1)
                    || (absX == 1) && (absY == 2)) ? 
                IsPositionFreeOfAllies(end) : false;
        }
    }
}
