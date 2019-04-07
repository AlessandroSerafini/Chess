using System;

namespace Chess.Models
{
    public class King : Piece
    {
        public King(bool isWhite, int x, int y, Piece[,] matrix) : base(isWhite, x, y, matrix)
        {
            identifier = 'K';
        }

        public override bool CheckMove(Position end)
        {
            return ((GetMoveAbsValue(end, 'X') <= 1) && (GetMoveAbsValue(end, 'Y') <= 1)) ?
                IsPositionFreeOfAllies(end) : false;
        }
    }
}
