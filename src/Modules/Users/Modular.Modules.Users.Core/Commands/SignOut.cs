using System;
using Modular.Abstractions.Commands;

namespace Modular.Modules.Users.Core.Commands
{
    internal record SignOut(Guid UserId) : ICommand;
}