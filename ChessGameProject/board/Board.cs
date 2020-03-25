using System;
using System.Collections.Generic;
using System.Text;
using ChessGameProject.Chess;

namespace ChessGameProject.board
{
    class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] pieces;

        public Board ()
        {
        }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            pieces = new Piece[rows, columns];
        }

        //Returns the piece in position (row, column) 
        public Piece piece(int row, int column)
        {
            return pieces[row, column];
        }

        // For instance the piece in matriz [x,y]
        public Piece piece(Position position)
        {
            return pieces[position.Row, position.Column];
        }

        //If there is a piece in x posisiton -> but first, verify if position is valid
        public bool ExistsPiece(Position position)
        {
            ValidateIfPositionWithinLimits(position);
            return piece(position) != null;
        }

        //Put one piece in x position 
        public void PutPiece (Piece piece, Position position)
        {
            if(ExistsPiece(position))
            {
                throw new BoardException("There is already a piece in that position!");
            }
            pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        //Remove one piece in x position 
        public Piece RemovePiece(Position position)
        {
            if (piece(position) == null)
            {
                return null;
            }
            Piece aux = piece(position);
            aux.Position = null;
            pieces[position.Row, position.Column] = null;
            return aux;
        }

        //Position Verification-> if is valid or not 
        public bool ValidPosition(Position position)
        {
            if(position.Row<0 || position.Row>= Rows || position.Column<0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        //Exception for invalid position -> verify if position is within the limits
        public void ValidateIfPositionWithinLimits(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid position!");
            }
        }
    }
}
