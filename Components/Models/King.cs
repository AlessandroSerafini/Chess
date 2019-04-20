using System;

namespace Chess.Models
{
    public class King : Piece
    {
        public King(bool isWhite, int x, int y, Piece[,] matrix) : base(isWhite, x, y, matrix)
        {
            identifier = '♚';
        }

        /* This method overrides the relative method defined in the parent class
        by applying specific rules on the possibility of doing certain kinds of moves
        belonging to the King piece. */
        public override bool CheckMove(Position end)
        {
            /* Check that you are moving only one cell and there isn't already
            a piece of mine in the place where I want to move. */
            return ((GetMoveAbsValue(end, 'X') <= 1) && (GetMoveAbsValue(end, 'Y') <= 1)) ?
                IsPositionFreeOfAllies(end) : false;
        }
    }
}
