using System;
using System.Collections.Generic;
using System.Text;
using ChessGameProject.board.Enum;
using ChessGameProject.board;

namespace ChessGameProject.Chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        //The piece can moves if the position is free or occupied by another piece of a different color
        private bool CanMove(Position position)
        {
            Piece p = Board.piece(position);
            return p == null || p.Color != Color;
        }

        //Possible Movements the Tower Piece -> can move until find a piece of diferent color or can move as many free square the player wants
        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];
            Position pos = new Position(0, 0);

            //Up 
            pos.SetPositionValues(Position.Row - 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                    break;
                pos.Row = pos.Row - 1;
            }

            //Right
            pos.SetPositionValues(Position.Row, Position.Column+1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                    break;
                pos.Column = pos.Column + 1;
            }


            //Down -> S
            pos.SetPositionValues(Position.Row + 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                    break;
                pos.Row = pos.Row + 1;
            }


            //Left
            pos.SetPositionValues(Position.Row, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                    break;
                pos.Column = pos.Column - 1;
            }

            return mat;
        }
    }
}
