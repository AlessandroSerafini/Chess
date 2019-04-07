using System;

namespace Chess.Models
{
    public class Knight : Piece
    {
        public Knight(bool isWhite, int x, int y, Piece[,] matrix) : base(isWhite, x, y, matrix)
        {
            identifier = 'H'; // horse
        }

        /* This method overrides the relative method defined in the parent class
        by applying specific rules on the possibility of doing certain kinds of moves
        belonging to the Knight piece. */
        public override bool CheckMove(Position end)
        {
            /* Getting how many cells the piece is moved horizontally and vertically */
            int absX = GetMoveAbsValue(end, 'X');
            int absY = GetMoveAbsValue(end, 'Y');
            
            /* Check that the piece performs two horizontal/vertical steps followed by a
            vertical/horizontal step, so that the path traveled ideally forms an "L"
            and there isn't already a piece of mine in the place where I want to move. */
            return ((absX == 2) && (absY == 1)
                    || (absX == 1) && (absY == 2)) ? 
                IsPositionFreeOfAllies(end) : false;
        }
    }
}
