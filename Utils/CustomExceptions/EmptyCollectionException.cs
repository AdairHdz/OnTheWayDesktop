using System;

namespace Utils.CustomExceptions
{
    public class EmptyCollectionException : Exception
    {
        public EmptyCollectionException() : base() { }
    }
}
