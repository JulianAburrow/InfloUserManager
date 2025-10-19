namespace InfloUserManagerWebAPI.MiddleWare;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string APIKEYNAME = "X-Api-Key"; // needs to be somewhere else, of course
    private readonly string _apiKey;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _apiKey = configuration["ApiKey"] ?? throw new ArgumentNullException("ApiKey is not configured in appsettings.json");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey) ||
            _apiKey != extractedApiKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Unauthorized
            await context.Response.WriteAsync("Unauthorized access. Invalid API Key.");
            return;
        }

        await _next(context); // Call the next middleware in the pipeline
    }
}
