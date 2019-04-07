using System;

namespace Chess.Models
{
    abstract public class Piece
    {
        protected char identifier; // Piece identifier
        protected bool isWhite;// true for white piece, false for black piece

        protected Position start; // Piece start position
        protected Piece[,] matrix;  // Matrix reference
        
        /* Definition of Piece class, which is composed by the
        "matrix", "start" and "isWhite" properties. */
        public Piece(bool isWhite, int x, int y, Piece[,] matrix)
        {
            this.matrix = matrix;
            this.start = new Position(x, y);
            this.isWhite = isWhite;
        }

        /* This method takes care of verify that user can
        do a specific move with a specific piece. */
        public abstract bool CheckMove(Position end);

        /* This method takes care of calculating how many cells the piece is moved. */
        public int GetMoveAbsValue(Position end, char axis)
        {
            return axis == 'X' ? Math.Abs(start.X - end.X) : Math.Abs(start.Y - end.Y);
        }

        /* This method takes care of check if the specified position is free from my pieces.  */
        public bool IsPositionFreeOfAllies(Position position)
        {
            return matrix[position.X, position.Y] != null ?
                (matrix[position.X, position.Y].IsWhite != isWhite) : true;
        }

        /* This method takes care of get/set piece identifier. */
        public char Identifier
        {
            get
            {
                return identifier;
            }
            set
            {
                identifier = value;
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

        /* This method takes care of get/set piece start position (Position object). */
        public Position Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }
    }
}
