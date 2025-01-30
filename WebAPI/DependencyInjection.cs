using Serilog;
using WebAPI.Middlewares;

namespace WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IHostBuilder host)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration));

        services.AddExceptionHandler<ExceptionHandlingMiddleware>();

        return services;
    }
}