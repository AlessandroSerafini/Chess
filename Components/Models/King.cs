using System;

namespace Chess.Models
{
    public class King : Piece
    {
        public King(bool isWhite, int x, int y, Piece[,] matrix) : base(isWhite, x, y, matrix)
        {
            identifier = 'K';
        }

        /* This method overrides the relative method defined in the parent class
        by applying specific rules on the possibility of doing certain kinds of moves
        belonging to the King piece. */
        public override bool CheckMove(Position end)
        {
            /* Check that you are moving only one cell. */
            return ((GetMoveAbsValue(end, 'X') <= 1) && (GetMoveAbsValue(end, 'Y') <= 1)) ?
                IsPositionFreeOfAllies(end) : false;
        }
    }
}
