using System;
using System.Collections.Generic;
using System.Text;
using ChessGameProject.board.Enum;
using ChessGameProject.board;

namespace ChessGameProject.Chess
{
    class King: Piece
    {
        private ChessMatch Match;
        public King (Board board, Color color, ChessMatch match) : base (board, color)
        {
            Match = match;
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

        //Verify if the piece is a Tower for Rock Move
        private bool TestTowerForRockMove (Position position)
        {
            Piece p = Board.piece(position);
            return p != null && p is Tower && p.Color == Color && p.MoveCount == 0;
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

            // #Special Moves -> Rock Move
            if(MoveCount==0 && !Match.Check)
            {
                //Rock Move small
                Position positionTower = new Position(Position.Row, Position.Column + 3);
                if (TestTowerForRockMove (positionTower))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Board.piece(p1) == null && Board.piece(p2) == null)
                        mat[Position.Row, Position.Column + 2] = true;
                }

                //Rock Move Big
                Position positionTower2 = new Position(Position.Row, Position.Column - 4);
                if (TestTowerForRockMove(positionTower2))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.piece(p1) == null && Board.piece(p2) == null && Board.piece(p3) == null)
                        mat[Position.Row, Position.Column - 2] = true;
                }
            }

            return mat;
        }
    }
}
