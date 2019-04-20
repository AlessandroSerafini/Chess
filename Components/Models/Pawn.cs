using System;

namespace Chess.Models
{
    public class Pawn : Piece
    {
        /* If moved or not, the pawn has a different logic: it can moves only one cell at a time,
        except starting from the starting line, in which case it can move even two cells at once. */
        bool moved;

        public Pawn(bool isWhite, int x, int y, Piece[,] piece) : base(isWhite, x, y, piece)
        {
            identifier = this.isWhite ? '♙' : '♟';
            moved = false;
        }

        /* This method overrides the relative method defined in the parent class
        by applying specific rules on the possibility of doing certain kinds of moves
        belonging to the Pawn piece. */
        public override bool CheckMove(Position end)
        {
            bool result = false;
            
            /* Getting how many cells the piece is moved horizontally and vertically */
            int absX = GetMoveAbsValue(end, 'X');
            int absY = GetMoveAbsValue(end, 'Y');

            // If is a vertical move (I'm moving)
            if (position.X == end.X) 
            {
                // If it isn't already moved, it can go on for two cells
                if (absY == 2 && !moved) 
                { 
                    /* A two-cell move can only be done if there are no pieces in the route. */
                    if (isWhite) {
                        result = !((piece[end.X, end.Y] != null) || (piece[end.X, end.Y - 1] != null));
                    } else {
                        result = !((piece[end.X, end.Y] != null) || (piece[end.X, end.Y + 1] != null));
                    }
                } 
                // If it is already moved, it can go on for only one cell
                else if (absY == 1) 
                {
                    // Pawn can't move back
                    result = isWhite ? (position.Y < end.Y) : (position.Y > end.Y);

                    // If i'm not moving back
                    if (result) {
                        /* Checking that there isn't already a piece
                        of mine in the place where I want to move. */
                        result = piece[end.X, end.Y] == null ? IsPositionFreeOfAllies(end) : false;
                    }
                }
            } 
            // If is a diagonal move (I'm eating) and I'm moving one cell
            else if (absX == 1 && absY == 1)
            {
                /* Verifying that the final position is occupied by an opponent's piece. */
                result = piece[end.X, end.Y] != null && piece[end.X, end.Y].IsWhite != isWhite;
            }

            /* The move can be performed: set the moved attribute to true. */
            if (result) {
                moved = true;
            }

            return result;
        }
    }
}
