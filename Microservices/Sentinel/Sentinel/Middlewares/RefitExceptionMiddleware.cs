using Refit;
using System.Text.Json;

namespace Sentinel.Middlewares;

public class RefitExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public RefitExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException ex)
        {
            ProblemDetails inner = null!;
            if (!string.IsNullOrEmpty(ex.Content) && ex.Content.TrimStart().StartsWith("{"))
            {
                try
                {
                    inner = JsonSerializer.Deserialize<ProblemDetails>(
                        ex.Content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    )!;
                }
                catch
                {
                }
            }

            var problem = new ProblemDetails
            {
                Type = inner?.Type ?? "about:blank",
                Title = inner?.Title ?? "Error calling microservice",
                Status = inner?.Status ?? (int)ex.StatusCode,
                Detail = inner?.Detail ?? inner?.Extensions?["traceId"]?.ToString()
                            ?? ex.ReasonPhrase,
                Instance = ex.Uri?.ToString()
            };

            if (inner?.Extensions != null)
            {
                foreach (var kv in inner.Extensions)
                {
                    if (!problem.Extensions.ContainsKey(kv.Key))
                        problem.Extensions[kv.Key] = kv.Value;
                }
            }

            context.Response.StatusCode = problem.Status;
            context.Response.ContentType = "application/problem+json";

            var json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }
    }
}


