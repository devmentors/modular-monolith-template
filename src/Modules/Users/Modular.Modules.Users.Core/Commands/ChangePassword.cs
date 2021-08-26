using System;
using Modular.Abstractions.Commands;

namespace Modular.Modules.Users.Core.Commands
{
    internal record ChangePassword(Guid UserId, string CurrentPassword, string NewPassword) : ICommand;
}