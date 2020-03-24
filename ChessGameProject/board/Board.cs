﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameProject.board
{
    class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board ()
        {
        }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[rows, columns];
        }
    }
}