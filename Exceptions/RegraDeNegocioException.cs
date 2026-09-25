namespace DeskFlowAPI.Exceptions;

public class RegraDeNegocioException : AppException
{
    public override int StatusCode => 400;
    public RegraDeNegocioException(string message) : base(message)
    {
    }
}