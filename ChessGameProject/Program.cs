using System;
using ChessGameProject.board;

namespace ChessGameProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position(3, 4);
            Console.WriteLine("Position: " + position);
        }
    }
}
