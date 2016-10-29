using System;

namespace EasyStub.Client
{
    public class StubApiException : Exception
    {
 
        public StubApiException(string errorMessage):base(errorMessage)
        {
        }
    }
}