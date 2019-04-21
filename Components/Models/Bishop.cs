using System;

namespace Chess.Models
{
    public class Bishop : Piece
    {
        public Bishop(bool isWhite, int x, int y, Piece[,] piece) : base(isWhite, x, y, piece)
        {
            identifier = 'B';
            symbol = this.isWhite ? '♗' : '♝';
        }

        /* This method overrides the relative method defined in the parent class
        by applying specific rules on the possibility of doing certain kinds of moves
        belonging to the Bishop piece. */
        public override bool CheckMove(Position targetPosition)
        {
            bool result = false;

            /* Getting how many cells the piece is moved horizontally and vertically */
            int absX = GetMoveAbsValue(targetPosition, 'X');
            int absY = GetMoveAbsValue(targetPosition, 'Y');

            // If is a diagonal move
            if (absX == absY)
            {
                result = true;

                // I can go on as long as there are empty cells in my way
                for (int i = 1; ((i < absX) && (result)); i++)
                {
                    if (position.X < targetPosition.X)
                    {
                        result = position.Y < targetPosition.Y ?
                                      !(piece[position.X + i, position.Y + i] != null) :
                                      !(piece[position.X + i, position.Y - i] != null);
                    }
                    else
                    {
                        result = position.Y < targetPosition.Y ?
                                      !(piece[position.X - i, position.Y + i] != null) :
                                      !(piece[position.X - i, position.Y - i] != null);
                    }
                }
            }

            /* Checking that there isn't already a piece of mine in the place where I want to move. */
            return result ? IsPositionFreeOfAllies(targetPosition) : result;
        }
    }
}
