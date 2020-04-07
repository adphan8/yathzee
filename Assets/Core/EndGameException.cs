using System;
using System.Runtime.Serialization;

public class EndGameException : Exception
{
    public EndGameException()
    {
    }

    public EndGameException(string message) : base(message)
    {
    }

    public EndGameException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected EndGameException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}