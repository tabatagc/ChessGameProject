using System;
using System.Collections.Generic;
using System.Text;
using ChessGameProject.board;

namespace ChessGameProject
{
    class Screen
    {
        public static void PrintBoard (Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.piece(i, j) == null)
                        Console.Write("- ");
                    else
                    Console.Write(board.piece(i, j) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintPiece (Piece piece)
        {
            if (piece.Color == board.Enum.Color.White)
                Console.Write(piece);
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
