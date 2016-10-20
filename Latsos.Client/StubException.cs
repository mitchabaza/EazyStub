using System;

namespace Latsos.Client
{
    public class StubException : Exception
    {
 
        public StubException(string errorMessage):base(errorMessage)
        {
        }
    }
}