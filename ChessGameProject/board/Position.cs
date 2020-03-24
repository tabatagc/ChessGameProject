using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameProject.board
{
    class Position
    {
        public int row { get; set; }
        public int column { get; set; }

        public Position()
        {
        }

        public Position(int _row, int _column)
        {
            row = _row;
            column = _column;
        }

        public override string ToString()
        {
            return row + ", " + column;
        }
    }
}
