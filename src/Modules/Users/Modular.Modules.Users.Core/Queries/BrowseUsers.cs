using Modular.Modules.Users.Core.DTO;
using Modular.Abstractions.Queries;

namespace Modular.Modules.Users.Core.Queries
{
    internal class BrowseUsers : PagedQuery<UserDto>
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string State { get; set; }
    }
}