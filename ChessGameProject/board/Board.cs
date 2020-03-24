using System;
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

        public Piece piece(int row, int column)
        {
            return Pieces[row, column];
        }

        public void PutPiece (Piece piece, Position position)
        {
            Pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }
    }
}
