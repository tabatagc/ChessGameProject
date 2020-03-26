using System;
using System.Collections.Generic;
using System.Text;
using ChessGameProject.board.Enum;
using ChessGameProject.board;

namespace ChessGameProject.board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MoveCount { get; protected set; }
        public Board Board { get; protected set; }

        public Piece ()
        {
        }

        public Piece( Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            MoveCount = 0;
        }

        //Increase quantity the movements
        public void IncreaseQuantityMovements ()
        {
            MoveCount++;
        }

        //Decrease quantity the movements
        public void DecreaseQuantityMovements()
        {
            MoveCount--;
        }

        // verify if there is possible movements
        public bool ThereIsPossibleMovements()
        {
            bool[,] mat = PossibleMovements();
            for (int i=0; i< Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j] == true)
                        return true;
                }
            }
            return false;
        }

        // Verify if position is valid for move this piece
        public bool ThisPieceCanMoveForThisPosition (Position position)
        {
            return PossibleMovements()[position.Row, position.Column];
        }

        //Possible movements -> this method is abstract because depend of piece type
        public abstract bool[,] PossibleMovements();
    }
}
