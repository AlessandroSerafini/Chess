using System;

namespace Chess.Models
{
    abstract public class Piece
    {
        protected Piece[,] piece;  // Piece reference
        protected char identifier; // Piece identifier
        protected char symbol; // Piece symbol used in chessboard
        protected Position position; // Piece position
        protected bool isWhite;// true for white piece, false for black piece
        
        /* Definition of Piece class, which is composed by the
        "piece", "position" and "isWhite" properties. */
        public Piece(bool isWhite, int x, int y, Piece[,] piece)
        {
            this.piece = piece;
            this.position = new Position(x, y);
            this.isWhite = isWhite;
        }

        /* This method takes care of verify that user can
        do a specific move with a specific piece. */
        public abstract bool CheckMove(Position targetPosition);

        /* This method takes care of calculating how many cells the piece is moved. */
        public int GetMoveAbsValue(Position targetPosition, char axis)
        {
            return axis == 'X' ? Math.Abs(position.X - targetPosition.X) : Math.Abs(position.Y - targetPosition.Y);
        }

        /* This method takes care of check if the specified position is free from my pieces.  */
        public bool IsPositionFreeOfAllies(Position position)
        {
            return piece[position.X, position.Y] != null ?
                (piece[position.X, position.Y].IsWhite != isWhite) : true;
        }

        /* This method takes care of get/set piece identifier. */
        public char Symbol
        {
            get
            {
                return symbol;
            }
        }

        /* This method takes care of get/set piece identifier. */
        public bool IsKing
        {
            get
            {
                return identifier == 'K';
            }
        }

        /* This method takes care of get/set piece color: true for white piece, false for black piece. */
        public bool IsWhite
        {
            get
            {
                return isWhite;
            }
            set
            {
                isWhite = value;
            }
        }

        /* This method takes care of get/set piece position (Position object). */
        public Position Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
    }
}
