namespace DeskFlowAPI.DTO;

public class ErrorDTO
{
    public string Message { get; set; }
    public int StatusCode { get; set; }

    public ErrorDTO(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }
}