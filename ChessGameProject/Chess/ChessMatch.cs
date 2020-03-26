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
        public  Piece vulnerablePieceEnPassant { get; private set; }


        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            ActualPlayer = Color.White;
            Finished = false;
            Check = false;
            vulnerablePieceEnPassant = null;
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

            // #Special Move -> Rock Move small
            if(p is King && destiny.Column== initial.Column +2)
            {
                Position initialTower = new Position(initial.Row, initial.Column + 3);
                Position destinyTower = new Position(initial.Row, initial.Column + 1);
                Piece T = Board.RemovePiece(initialTower);
                T.IncreaseQuantityMovements();
                Board.PutPiece(T, destinyTower);
            }

            // #Special Move -> Rock Move big
            if (p is King && destiny.Column == initial.Column - 2)
            {
                Position initialTower = new Position(initial.Row, initial.Column - 4);
                Position destinyTower = new Position(initial.Row, initial.Column - 1);
                Piece T = Board.RemovePiece(initialTower);
                T.IncreaseQuantityMovements();
                Board.PutPiece(T, destinyTower);
            }

            // #Special Move -> En Passant
            if (p is Pawn)
            {
                if(initial.Column != destiny.Column && capturedPiece== null)
                {
                    Position positionP;
                    if (p.Color == Color.White)
                        positionP = new Position(destiny.Row + 1, destiny.Column);
                    else
                        positionP = new Position(destiny.Row - 1, destiny.Column);

                    capturedPiece = Board.RemovePiece(positionP);
                    capturedPieces.Add(capturedPiece);
                }
            }

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

            // #Special Move -> Rock Move small
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position initialTower = new Position(origin.Row, origin.Column + 3);
                Position destinyTower = new Position(origin.Row, origin.Column + 1);
                Piece T = Board.RemovePiece(destinyTower);
                T.DecreaseQuantityMovements();
                Board.PutPiece(T, initialTower);
            }

            // #Special Move -> Rock Move big
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position initialTower = new Position(origin.Row, origin.Column - 4);
                Position destinyTower = new Position(origin.Row, origin.Column - 1);
                Piece T = Board.RemovePiece(destinyTower);
                T.DecreaseQuantityMovements();
                Board.PutPiece(T, initialTower);
            }

            // #Special Move -> En Passant
            if (p is Pawn)
            {
                if(origin.Column != destiny.Column && capturedPiece== vulnerablePieceEnPassant)
                {
                    Piece Piecepawn = Board.RemovePiece(destiny);
                    Position positionP;
                    if(p.Color== Color.White)
                        positionP = new Position(3, destiny.Column);
                    else
                        positionP = new Position(4, destiny.Column);

                    Board.PutPiece(Piecepawn, positionP);

                }

            }

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

            Piece p = Board.piece(destiny);

            // # Special Move -> promotion
            if(p is Pawn)
            {
                if(p.Color== Color.White && destiny.Row==0 || p.Color == Color.Black && destiny.Row == 7)
                {
                    p = Board.RemovePiece(destiny);
                    pieces.Remove(p);
                    Piece queen = new Queen(Board, p.Color);
                    Board.PutPiece(queen, destiny);
                    pieces.Add(queen);
                }

            }


            if (IsInCheck(AdversaryColor(ActualPlayer)))
                Check = true;
            else
                Check = false;

            if (CheckMateTest(AdversaryColor(ActualPlayer)))
                Finished = true;
            else
            {

                Turn++;
                ChangePlayer();
            }

            // #Special Move En Passant
            if (p is Pawn && (destiny.Row == destiny.Row - 2 || destiny.Row == destiny.Row + 2))
                vulnerablePieceEnPassant = p;
            else
                vulnerablePieceEnPassant = null;
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


        //Verify if exsits checkMate
        public bool CheckMateTest (Color color)
        {
            if (!IsInCheck(color))
                return false;

            foreach(Piece x in PiecesOnGame(color))
            {
                bool[,] mat = x.PossibleMovements();
                for (int i=0; i< Board.Rows; i++)
                {
                    for (int j = 0; i < Board.Columns; i++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = MovePiece(origin, destiny);
                            bool CheckTest = IsInCheck(color);
                            UndoMovement(origin, destiny, capturedPiece);
                            if (!CheckTest)
                                return false;
                        }

                    }
                }
            }
            return true;
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
            PutNewPiece('a', 1, new Tower(Board, Color.White));
            PutNewPiece('b', 1, new Horse(Board, Color.White));
            PutNewPiece('c', 1, new Bishop(Board, Color.White));
            PutNewPiece('d', 1, new Queen(Board, Color.White));
            PutNewPiece('e', 1, new King(Board, Color.White, this));
            PutNewPiece('f', 1, new Bishop(Board, Color.White));
            PutNewPiece('g', 1, new Horse(Board, Color.White));
            PutNewPiece('h', 1, new Tower(Board, Color.White));
            PutNewPiece('a', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('b', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('c', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('d', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('e', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('f', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('g', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('h', 2, new Pawn(Board, Color.White, this));

            PutNewPiece('a', 8, new Tower(Board, Color.Black));
            PutNewPiece('b', 8, new Horse(Board, Color.Black));
            PutNewPiece('c', 8, new Bishop(Board, Color.Black));
            PutNewPiece('d', 8, new Queen(Board, Color.Black));
            PutNewPiece('e', 8, new King(Board, Color.Black, this));
            PutNewPiece('f', 8, new Bishop(Board, Color.Black));
            PutNewPiece('g', 8, new Horse(Board, Color.Black));
            PutNewPiece('h', 8, new Tower(Board, Color.Black));
            PutNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('h', 7, new Pawn(Board, Color.Black, this));


        }
    }
}
