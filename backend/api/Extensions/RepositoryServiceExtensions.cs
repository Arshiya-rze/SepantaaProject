namespace api.Extensions;

public static class RepositoryServiceExtensions
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        #region Dependency Injections
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        // services.AddScoped<ITeacherRepository, TeacherRepository>();
        // services.AddScoped<IAdminRepository, AdminRepository>();

        #endregion Dependency Injections

        return services;
    }
}
