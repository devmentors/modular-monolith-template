using System;
using Modular.Abstractions.Commands;

namespace Modular.Modules.Users.Core.Commands
{
    internal record UpdateUserState(Guid UserId, string State) : ICommand;
}