using System;

namespace TGPro.Service.Exceptions
{
    public class TGProException : Exception
    {
        public TGProException() { }

        public TGProException(string message)
            : base(message)
        {
        }

        public TGProException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
