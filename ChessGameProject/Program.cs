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
            try
            {
                Board board = new Board(8, 8);

                board.PutPiece(new Tower(board, Color.Black), new Position(0, 0));
                board.PutPiece(new Tower(board, Color.Black), new Position(1, 3));
                board.PutPiece(new King(board, Color.Black), new Position(5, 7));
                board.PutPiece(new King(board, Color.White), new Position(4, 1));

                Screen.PrintBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            //ChessPosition chessPosition = new ChessPosition('b', 5);
            //Console.WriteLine(chessPosition);
            //Console.WriteLine(chessPosition.ToPosition());
        }
    }
}
