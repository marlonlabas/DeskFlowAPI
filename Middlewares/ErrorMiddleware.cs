using DeskFlowAPI.Exceptions;
using DeskFlowAPI.DTO;

namespace DeskFlowAPI.Middlewares;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            var resposta = new ErrorDTO(ex.Message, ex.StatusCode);
            await context.Response.WriteAsJsonAsync(resposta);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado ao processar a requisição: {ex.Message}");
            var resposta = new ErrorDTO("Ocorreu um erro na chamada por favor tente novamente mais tarde", 500);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(resposta);

        }

    }
}

