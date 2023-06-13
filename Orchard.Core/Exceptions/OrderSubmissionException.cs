namespace Orchard.Core.Exceptions;

public class OrderSubmissionException : Exception
{
    public OrderSubmissionException(string? message) : base(message)
    {
    }
}