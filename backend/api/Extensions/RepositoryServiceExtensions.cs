namespace api.Extensions;

public static class RepositoryServiceExtensions
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        #region Dependency Injections
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IAccountRepository, AccountRepository>();

        #endregion Dependency Injections

        return services;
    }
}
