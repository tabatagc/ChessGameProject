using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameProject.board
{
    class BoardException: Exception
    {
        //For use Exception in Position
        public BoardException(string message) : base(message)
        {
        }
    }
}
