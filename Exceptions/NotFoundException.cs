namespace DeskFlowAPI.Exceptions;

public abstract class AppException : Exception
{
    public abstract int StatusCode { get; }
    protected AppException(string message) : base(message)
    {
    }
}

public class NotFoundException : AppException
{
    public override int StatusCode => 404;

    public NotFoundException(string message) : base (message)
    {
    }
}