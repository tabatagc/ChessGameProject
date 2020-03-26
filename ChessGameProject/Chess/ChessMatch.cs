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

        private HashSet<Piece> pieces;
        private HashSet<Piece> capturedPieces;
        public bool Check { get; private set; }


        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            ActualPlayer = Color.White;
            Finished = false;
            Check = false;
            pieces = new HashSet<Piece> ();
            capturedPieces = new HashSet<Piece>();
            PutAllPiecesForPlay();
        }

        //Move piece the initial position for destiny position
        public Piece MovePiece (Position initial, Position destiny)
        {
            Piece p = Board.RemovePiece(initial);
            p.IncreaseQuantityMovements();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(p, destiny);
            if (capturedPiece  != null)
                capturedPieces.Add(capturedPiece);
            return capturedPiece;
        }


        public void UndoMovement (Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destiny);
            p.DecreaseQuantityMovements();
            if(capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, destiny);
                capturedPieces.Remove(capturedPiece);
            }
            Board.PutPiece(p,origin);
        }

        //Chage turn when the player finished one movement
        public void PlayWithChangeTurn(Position initial, Position destiny)
        {
            Piece capturedPiece = MovePiece(initial, destiny);

            if (IsInCheck(ActualPlayer))
            {
                UndoMovement(initial, destiny, capturedPiece);
                throw new BoardException("Can't be checked");
            }

            if (IsInCheck(AdversaryColor(ActualPlayer)))
                Check = true;
            else
                Check = false;

            Turn++;
            ChangePlayer();
        }
        
        //Verify if initial position is valid
        public void ChoiceInicialPositionIsValid (Position position)
        {
            Board.ValidateIfPositionWithinLimits(position);
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
                Board.ValidateIfPositionWithinLimits(destiny);
                if (!Board.piece(initial).ThisPieceCanMoveForThisPosition(destiny))
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

        //Captured Pieces by a certain color
        public HashSet<Piece> capturedPiecesByColor (Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece> ();
            foreach (Piece x in capturedPieces)
            {
                if (x.Color == color)
                    aux.Add(x);
            }
            return aux;
        }

        // Pieces on game by a certain color
        public HashSet<Piece> PiecesOnGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.Color == color)
                    aux.Add(x);
            }
            aux.ExceptWith(capturedPiecesByColor(color));
            return aux;
        }

        //Know which color is opposing
        private Color AdversaryColor (Color color)
        {
            if (color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }


        //returns the king piece of the color we want
        private Piece King (Color color)
        {
            foreach(Piece x in PiecesOnGame(color))
            {
                if (x is King)
                    return x;
            }
            return null;
        }

        //Verify if King is in check
        public bool IsInCheck (Color color)
        {
            Piece K = King(color);
            if (K == null)
                throw new BoardException("There is no " + color +  " king on the board.");
            foreach(Piece x in PiecesOnGame(AdversaryColor(color)))
            {
                bool[,] mat = x.PossibleMovements();
                if (mat[K.Position.Row, K.Position.Column])
                    return true;
            }
            return false;
        }

        //Put new Piece in the game
        public void PutNewPiece (char column, int row, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            pieces.Add(piece);
        }

        //Put all Pieces of the game
        private void PutAllPiecesForPlay()
        {
            PutNewPiece('c', 1, new Tower(Board, Color.White));
            PutNewPiece('c', 2, new Tower(Board, Color.White));
            PutNewPiece('d', 2, new Tower(Board, Color.White));
            PutNewPiece('e', 2, new Tower(Board, Color.White));
            PutNewPiece('e', 1, new Tower(Board, Color.White));
            PutNewPiece('d', 1, new King(Board, Color.White));

            PutNewPiece('c', 7, new Tower(Board, Color.Black));
            PutNewPiece('c', 8, new Tower(Board, Color.Black));
            PutNewPiece('d', 7, new Tower(Board, Color.Black));
            PutNewPiece('e', 7, new Tower(Board, Color.Black));
            PutNewPiece('e', 8, new Tower(Board, Color.Black));
            PutNewPiece('d', 8, new King(Board, Color.Black));
        }
    }
}
