using CrossCutting.Exceptions;

namespace App.Middlewares;

public class InterceptadorDeExcecao
{
    private readonly RequestDelegate _next;

    public InterceptadorDeExcecao(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Pass the context to the next middleware in the pipeline
            await _next(context);


        }
        catch (Exception ex)
        {
            // Handle the exception
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {

        string mensagem = "Houve um erro na aplicação.";
        int statusCode = 500;

        if (exception is JaExisteException)
        {
            statusCode = 409;
            mensagem = "Já existe";
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new
        {
            StatusCode = statusCode,
            Message = mensagem
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}