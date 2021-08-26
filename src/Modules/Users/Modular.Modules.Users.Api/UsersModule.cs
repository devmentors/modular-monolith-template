using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Modular.Modules.Users.Core;
using Modular.Abstractions.Modules;

namespace Modular.Modules.Users.Api
{
    internal class UsersModule : IModule
    {
        public string Name { get; } = "Users";
        
        public IEnumerable<string> Policies { get; } = new[]
        {
            "users"
        };

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }
        
        public void Use(IApplicationBuilder app)
        {
        }
    }
}