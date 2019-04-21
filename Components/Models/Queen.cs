using System;

namespace Chess.Models
{
    public class Queen : Piece
    {
        public Queen(bool isWhite, int x, int y, Piece[,] piece) : base(isWhite, x, y, piece)
        {
            identifier = 'Q';
            symbol = this.isWhite ? '♕' : '♛';
        }

        /* This method overrides the relative method defined in the parent class
        by applying specific rules on the possibility of doing certain kinds of moves
        belonging to the Queen piece. */
        public override bool CheckMove(Position targetPosition)
        {
            bool result = false;
            
            /* Getting how many cells the piece is moved horizontally and vertically */
            int absX = GetMoveAbsValue(targetPosition, 'X');
            int absY = GetMoveAbsValue(targetPosition, 'Y');

            // If is a vertical move
            if (position.X == targetPosition.X)
            {
                result = true;

                /* Verifying that in the route, whether you move
                upwards or downwards, all the cells are empty. */
                for (int i = 1; ((i < absY) && (result)); i++)
                {
                    if (position.Y < targetPosition.Y)
                    {
                        result = piece[position.X, position.Y + i] == null;
                    }
                    else if (position.Y > targetPosition.Y)
                    {
                        result = piece[position.X, position.Y - i] == null;
                    }
                }
            }
            // If is an horizontal move
            else if (position.Y == targetPosition.Y)
            {
                result = true;

                /* Verifying that in the route, whether you move
                left or right, all the cells are empty. */
                for (int i = 1; ((i < absX) && (result)); i++)
                {
                    if (position.X < targetPosition.X)
                    {
                        result = piece[position.X + i, position.Y] == null;
                    }
                    else if (position.X < targetPosition.X)
                    {
                        result = piece[position.X - i, position.Y] == null;
                    }
                }
            }
            // If is a diagonal move
            else if (absX == absY)
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
