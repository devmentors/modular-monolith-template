using System.Collections.Generic;
using System.Threading.Tasks;
using Modular.Modules.Users.Core.Entities;

namespace Modular.Modules.Users.Core.Repositories
{
    internal interface IRoleRepository
    {
        Task<Role> GetAsync(string name);
        Task<IReadOnlyList<Role>> GetAllAsync();
        Task AddAsync(Role role);
    }
}