using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Modular.Abstractions.Modules;

namespace Modular.Modules.Second.Api
{
    internal class SecondModule : IModule
    {
        public string Name { get; } = "Second";
        
        public IEnumerable<string> Policies { get; } = new[]
        {
            "second"
        };

        public void Register(IServiceCollection services)
        {
        }
        
        public void Use(IApplicationBuilder app)
        {
        }
    }
}