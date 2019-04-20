using System;

namespace Chess.Models
{
    public class Rook : Piece
    {
        public Rook(bool isWhite, int x, int y, Piece[,] piece) : base(isWhite, x, y, piece)
        {
            identifier = this.isWhite ? '♖' : '♜';
        }

        /* This method overrides the relative method defined in the parent class
        by applying specific rules on the possibility of doing certain kinds of moves
        belonging to the Rook piece. */
        public override bool CheckMove(Position end)
        {
            bool result = false;

            /* Getting how many cells the piece is moved horizontally and vertically */
            int absX = GetMoveAbsValue(end, 'X');
            int absY = GetMoveAbsValue(end, 'Y');

            // If is a vertical move
            if (position.X == end.X)
            {
                result = true;

                /* Verifying that in the route, whether you move
                upwards or downwards, all the cells are empty. */
                for (int i = 1; ((i < absY) && (result)); i++)
                {
                    if (position.Y < end.Y)
                    {
                        result = piece[position.X, position.Y + i] == null;
                    }
                    else if (position.Y > end.Y)
                    {
                        result = piece[position.X, position.Y - i] == null;
                    }
                }
            }
            // If is an horizontal move
            else if (position.Y == end.Y)
            {
                result = true;

                /* Verifying that in the route, whether you move
                left or right, all the cells are empty. */
                for (int i = 1; ((i < absX) && (result)); i++)
                {
                    if (position.X < end.X)
                    {
                        result = piece[position.X + i, position.Y] == null;
                    }
                    else if (position.X < end.X)
                    {
                        result = piece[position.X - i, position.Y] == null;
                    }
                }
            }
            /* Checking that there isn't already a piece of mine in the place where I want to move. */
            return result ? IsPositionFreeOfAllies(end) : result;
        }
    }
}
