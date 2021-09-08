using System;

namespace SwashbuckleTest.Infrastructure.ObjectId
{
    public class InvalidObjectIdException : Exception
    {
        public InvalidObjectIdException()
            : base("Invalid ObjectId") { }

        public InvalidObjectIdException(string? id)
            : base($"Invalid ObjectId ({id})") { }
    }
}
