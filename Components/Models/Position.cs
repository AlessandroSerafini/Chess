using System;

namespace Chess.Models
{
    public class Position
    {
        private int x, y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                //Il valore di ogni coordinata deve essere compreso fra 0 e 7
                if ((value >= 0) && (value < 8))
                    x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                //Il valore di ogni coordinata deve essere compreso fra 0 e 7
                if ((value >= 0) && (value < 8))
                    y = value;
            }
        }
    }
}
