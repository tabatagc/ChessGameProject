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
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.Write("Position initial: ");
                        Position origem = Screen.ReadChessPosition().ToPosition();
                        match.ChoiceInicialPositionIsValid(origem);

                        bool[,] PossiblePossitions = match.Board.piece(origem).PossibleMovements();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, PossiblePossitions);

                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadChessPosition().ToPosition();
                        match.ChoiceDestinyPositionIsValid(origem, destiny);

                        match.PlayWithChangeTurn(origem, destiny);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                Screen.PrintMatch(match);

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
