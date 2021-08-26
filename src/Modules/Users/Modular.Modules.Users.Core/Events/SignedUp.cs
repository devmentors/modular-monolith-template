using System;
using Modular.Abstractions.Events;

namespace Modular.Modules.Users.Core.Events
{
    internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;
}