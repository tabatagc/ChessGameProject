using System;
using System.Collections.Generic;
using System.Text;
using ChessGameProject.board;
using ChessGameProject.board.Enum;

namespace ChessGameProject.Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color ActualPlayer;
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            ActualPlayer = Color.White;
            Finished = false;
            PutAllPiecesForPlay();
        }

        //Move piece the initial position for destiny position
        public void MovePiece (Position initial, Position destiny)
        {
            Piece p = Board.RemovePiece(initial);
            p.IncreaseQuantityMovements();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(p, destiny);
        }

        //Put all Pieces of the game
        private void PutAllPiecesForPlay()
        {
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.PutPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.PutPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
