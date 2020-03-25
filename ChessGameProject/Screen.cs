using System;
using System.Collections.Generic;
using System.Text;
using ChessGameProject.board;
using ChessGameProject.Chess;

namespace ChessGameProject
{
    class Screen
    {
        //Print Board
        public static void PrintBoard (Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        //Print Board with all possibilities for move the piece

        public static void PrintBoard(Board board, bool [,] PossiblePossitions)
        {
            ConsoleColor OriginalBackground = Console.BackgroundColor;
            ConsoleColor ChangedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (PossiblePossitions[i, j])
                        Console.BackgroundColor = ChangedBackground;
                    else
                        Console.BackgroundColor = OriginalBackground;

                    PrintPiece(board.piece(i, j));
                    Console.BackgroundColor = OriginalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = OriginalBackground;

        }

        // read Position input 
        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }

        public static void PrintPiece (Piece piece)
        {
            if (piece == null)
                Console.Write("- ");
            else
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
                Console.Write(" ");
            }
        }
    }
}
