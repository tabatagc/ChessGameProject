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

                ChessMatch match = new ChessMatch();

                while (! match.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);
                    Console.WriteLine();

                    Console.Write("Position initial: ");
                    Position origem = Screen.ReadChessPosition().ToPosition();

                    bool[,] PossiblePossitions = match.Board.piece(origem).PossibleMovements();

                    Console.Clear();
                    Screen.PrintBoard(match.Board, PossiblePossitions);

                    Console.Write("Destiny: ");
                    Position destiny = Screen.ReadChessPosition().ToPosition();

                    match.MovePiece(origem, destiny);
                }

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
