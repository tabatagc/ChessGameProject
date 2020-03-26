using System;
using System.Collections.Generic;
using System.Text;
using ChessGameProject.board;
using ChessGameProject.board.Enum;

namespace ChessGameProject.Chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool IsThereEnemy (Position position)
        {
            Piece p = Board.piece(position);
            return p != null && p.Color != Color;
        }
        private bool IsFree(Position position)
        {
            return Board.piece(position)== null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.SetPositionValues(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos))
                    mat[pos.Row, pos.Column] = true;

                pos.SetPositionValues(Position.Row - 2, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos) && MoveCount==0)
                    mat[pos.Row, pos.Column] = true;

                pos.SetPositionValues(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && IsThereEnemy(pos))
                    mat[pos.Row, pos.Column] = true;

                pos.SetPositionValues(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && IsThereEnemy(pos))
                    mat[pos.Row, pos.Column] = true;
            }

            else
            {
                pos.SetPositionValues(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos))
                    mat[pos.Row, pos.Column] = true;

                pos.SetPositionValues(Position.Row + 2, Position.Column);
                if (Board.ValidPosition(pos) && IsFree(pos) && MoveCount == 0)
                    mat[pos.Row, pos.Column] = true;

                pos.SetPositionValues(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && IsThereEnemy(pos))
                    mat[pos.Row, pos.Column] = true;

                pos.SetPositionValues(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && IsThereEnemy(pos))
                    mat[pos.Row, pos.Column] = true;
            }

            return mat;
        }
    }
}
