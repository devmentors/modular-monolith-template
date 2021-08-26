using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modular.Modules.Users.Core.Entities;
using Modular.Infrastructure;

namespace Modular.Modules.Users.Core.DAL
{
    internal sealed class UsersInitializer : IInitializer
    {
        private readonly HashSet<string> _permissions = new()
        {
            "first", "second", "third", "users"
        };

        private readonly UsersDbContext _dbContext;
        private readonly ILogger<UsersInitializer> _logger;

        public UsersInitializer(UsersDbContext dbContext, ILogger<UsersInitializer> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task InitAsync()
        {
            if (await _dbContext.Roles.AnyAsync())
            {
                return;
            }

            await AddRolesAsync();
            await _dbContext.SaveChangesAsync();
        }

        private async Task AddRolesAsync()
        {
            await _dbContext.Roles.AddAsync(new Role
            {
                Name = "admin",
                Permissions = _permissions
            });
            await _dbContext.Roles.AddAsync(new Role
            {
                Name = "user",
                Permissions = new List<string>()
            });

            _logger.LogInformation("Initialized roles.");
        }
    }
}