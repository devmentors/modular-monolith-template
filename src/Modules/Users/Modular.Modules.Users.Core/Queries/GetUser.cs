using System;
using Modular.Modules.Users.Core.DTO;
using Modular.Abstractions.Queries;

namespace Modular.Modules.Users.Core.Queries
{
    internal class GetUser : IQuery<UserDetailsDto>
    {
        public Guid UserId { get; set; }
    }
}