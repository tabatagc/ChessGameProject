using System;
using System.Collections.Generic;
using System.Text;
using ChessGameProject.board.Enum;
using ChessGameProject.board;

namespace ChessGameProject.Chess
{
    class King: Piece
    {
        public King (Board board, Color color): base (board, color)
        {
        }

        public override string ToString()
        {
            return "R";
        }

        //The piece can moves if the position is free or occupied by another piece of a different color
        private bool CanMove (Position position)
        {
            Piece p = Board.piece(position);
            return p == null || p.Color != Color;
        }

        //Possible Movements the King Piece -> just can move one position
        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];
            Position pos = new Position(0, 0);

            //up - North
            pos.SetPositionValues(Position.Row - 1, Position.Column);
            if(Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Row, pos.Column] = true;

            //NE
            pos.SetPositionValues(Position.Row - 1, Position.Column+1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Row, pos.Column] = true;

            //Right
            pos.SetPositionValues(Position.Row, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Row, pos.Column] = true;

            //SE
            pos.SetPositionValues(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Row, pos.Column] = true;

            //Down -> S
            pos.SetPositionValues(Position.Row + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Row, pos.Column] = true;

            //SO
            pos.SetPositionValues(Position.Row + 1, Position.Column-1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Row, pos.Column] = true;

            //Left
            pos.SetPositionValues(Position.Row, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Row, pos.Column] = true;

            //NO
            pos.SetPositionValues(Position.Row - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
                mat[pos.Row, pos.Column] = true;

            return mat;
        }
    }
}
