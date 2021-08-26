using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Modular.Abstractions.Modules;

namespace Modular.Modules.Third.Api
{
    internal class ThirdModule : IModule
    {
        public string Name { get; } = "Third";
        
        public IEnumerable<string> Policies { get; } = new[]
        {
            "third"
        };

        public void Register(IServiceCollection services)
        {
        }
        
        public void Use(IApplicationBuilder app)
        {
        }
    }
}