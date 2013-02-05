using System;

namespace MiniatureBottleWPFDesktopClient
{
    class EmptyFieldException : Exception
    {
        public EmptyFieldException()
        {

        }

        public EmptyFieldException(string message) : base(message)
        {

        }
    }    
}
