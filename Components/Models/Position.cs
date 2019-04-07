using System;

namespace Chess.Models
{
    public class Position
    {
        private int x, y;

        /* Definition of Position class, which is composed by the "x" and "y"
        properties which represents the piece's column and row respectively. */
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /* This method takes care of get/set the "x" position. */
        public int X
        {
            get
            {
                return x;
            }
            set
            {
                // The "x" value must be included between 0 and 7
                if ((value >= 0) && (value < 8))
                    x = value;
            }
        }

        /* This method takes care of get/set the "y" position. */
        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                // Also the "y" value must be included between 0 and 7
                if ((value >= 0) && (value < 8))
                    y = value;
            }
        }
    }
}
