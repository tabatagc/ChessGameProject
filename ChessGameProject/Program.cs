using System;
using ChessGameProject.board;
using ChessGameProject.board.Enum;
using ChessGameProject.Chess;

namespace ChessGameProject
{
    class Program
    {
        static void Main(string[] args)
        {

            Board board = new Board(8,8);

            board.PutPiece(new Tower(board, Color.Preta), new Position(0, 0));
            board.PutPiece(new Tower(board, Color.Preta), new Position(1, 3));
            board.PutPiece(new King(board, Color.Preta), new Position(2, 4));

            Screen.PrintBoard(board);
        }
    }
}
