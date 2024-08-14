namespace api.Extensions;

public static class RepositoryServiceExtensions
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        #region Dependency Injections
        // services.AddScoped<ITokenService, TokenService>();

        #endregion Dependency Injections

        return services;
    }
}
