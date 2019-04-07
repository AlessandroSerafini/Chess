using System;

namespace Chess.Models
{
    abstract public class Piece
    {
        protected char identifier; // identifier for the piece
        protected bool isWhite;// if 1 piece is white, if 0 piece is black

        protected Position start; //  start position of the piece
        protected Piece[,] matrix;  // matrix reference

        public Piece(bool isWhite, int x, int y, Piece[,] matrix)
        {
            this.matrix = matrix;
            this.start = new Position(x, y);
            this.isWhite = isWhite;
        }

        public abstract bool CheckMove(Position end);

        public int GetMoveAbsValue(Position end, char axis)
        {
            return axis == 'X' ? Math.Abs(start.X - end.X) : Math.Abs(start.Y - end.Y);
        }

        public bool IsPositionFreeOfAllies(Position position)
        {
            return matrix[position.X, position.Y] != null ?
                (matrix[position.X, position.Y].IsWhite != isWhite) : true;
        }

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
