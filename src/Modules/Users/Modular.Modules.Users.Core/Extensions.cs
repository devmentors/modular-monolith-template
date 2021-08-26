using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Modular.Modules.Users.Core.DAL;
using Modular.Modules.Users.Core.DAL.Repositories;
using Modular.Modules.Users.Core.Entities;
using Modular.Modules.Users.Core.Repositories;
using Modular.Modules.Users.Core.Services;
using Modular.Infrastructure;
using Modular.Infrastructure.Messaging.Outbox;
using Modular.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("Modular.Modules.Users.Api")]
[assembly: InternalsVisibleTo("Modular.Modules.Users.Tests.Integration")]
[assembly: InternalsVisibleTo("Modular.Modules.Users.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Modular.Modules.Users.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            var registrationOptions = services.GetOptions<RegistrationOptions>("users:registration");
            services.AddSingleton(registrationOptions);

            return services
                .AddSingleton<IUserRequestStorage, UserRequestStorage>()
                .AddScoped<IRoleRepository, RoleRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
                .AddPostgres<UsersDbContext>()
                .AddOutbox<UsersDbContext>()
                .AddUnitOfWork<UsersUnitOfWork>()
                .AddInitializer<UsersInitializer>();
        }
    }
}