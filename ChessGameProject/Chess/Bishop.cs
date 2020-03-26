using System;
using System.Collections.Generic;
using System.Text;
using ChessGameProject.board;
using ChessGameProject.board.Enum;

namespace ChessGameProject.Chess
{
    class Bishop : Piece
    {
        public Bishop (Board board, Color color) :base (board, color)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool CanMove(Position position)
        {
            Piece p = Board.piece(position);
            return p == null || p.Color != Color;
        }

        public override bool [,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            //NO 
            pos.SetPositionValues(Position.Row - 1, Position.Column-1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                    break;
                pos.SetPositionValues(pos.Row - 1, pos.Column - 1);
            }

            //NE 
            pos.SetPositionValues(Position.Row - 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                    break;
                pos.SetPositionValues(pos.Row - 1, pos.Column + 1);
            }

            //SE 
            pos.SetPositionValues(Position.Row + 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                    break;
                pos.SetPositionValues(pos.Row + 1, pos.Column + 1);
            }

            //SO 
            pos.SetPositionValues(Position.Row + 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Board.piece(pos) != null && Board.piece(pos).Color != Color)
                    break;
                pos.SetPositionValues(pos.Row + 1, pos.Column - 1);
            }
            return mat;
        }
    }
}
