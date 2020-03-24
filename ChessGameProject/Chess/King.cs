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
    }
}
