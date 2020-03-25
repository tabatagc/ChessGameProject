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
        public int Turn { get; private set; }
        public Color ActualPlayer { get; private set; }
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

        //Chage turn when the player finished one movement
        public void PlayWithChangeTurn(Position initial, Position destiny)
        {
            MovePiece(initial, destiny);
            Turn++;
            ChangePlayer();
        }
        
        //Verify if initial position is valid
        public void ChoiceInicialPositionIsValid (Position position)
        {
            if (Board.piece(position) == null)
                throw new BoardException("There isn't piece in the chosen origin position!");
            
            if(ActualPlayer != Board.piece(position).Color)
                throw new BoardException("A piece of chosen origin isn't yours!");

            if(!Board.piece(position).ThereIsPossibleMovements())
                throw new BoardException("There aren't possible moves for the chosen piece!");
        }

        //Verify if destiny position is valid
        public void ChoiceDestinyPositionIsValid(Position initial, Position destiny)
        {
            if(!Board.piece(initial).ThisPieceCanMoveForThisPosition(destiny))
                throw new BoardException("Destiny position invalid!");
        }

            //Change player for play
            private void ChangePlayer()
        {
            if (ActualPlayer == Color.White)
                ActualPlayer = Color.Black;
            else
                ActualPlayer = Color.White;
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
