using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Api.Interceptors;

public class LoggingInterceptor : Interceptor
{
    private readonly ILogger<LoggingInterceptor> _logger;

    public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        _logger.LogInformation($"Starting receiving call. Method: {context.Method}");

        try
        {
            var response = await continuation(request, context);
            _logger.LogInformation($"Method: {context.Method} successfully returned response");

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error thrown by {context.Method}");
            throw;
        }
    }
}