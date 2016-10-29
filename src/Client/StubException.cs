using System;

namespace EasyStub.Client
{
    public class StubException : Exception
    {
 
        public StubException(string errorMessage):base(errorMessage)
        {
        }
    }
}