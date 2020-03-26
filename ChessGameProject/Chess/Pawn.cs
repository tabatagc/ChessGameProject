using System;
using System.Collections.Generic;
using System.Text;
using ChessGameProject.board;
using ChessGameProject.board.Enum;

namespace ChessGameProject.Chess
{
    class Pawn : Piece
    {
        private ChessMatch Match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match=match;
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

                // #Special Move En Passant
                if (Position.Row==3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                        if (Board.ValidPosition(left) && IsThereEnemy(left) && Board.piece(left)== Match.vulnerablePieceEnPassant)
                            mat[left.Row-1, left.Column] = true;

                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && IsThereEnemy(right) && Board.piece(right) == Match.vulnerablePieceEnPassant)
                        mat[right.Row-1, right.Column] = true;
                }
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

                // #Special Move En Passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(left) && IsThereEnemy(left) && Board.piece(left) == Match.vulnerablePieceEnPassant)
                        mat[left.Row+1, left.Column] = true;

                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && IsThereEnemy(right) && Board.piece(right) == Match.vulnerablePieceEnPassant)
                        mat[right.Row+1, right.Column] = true;
                }
            }

            return mat;
        }
    }
}
