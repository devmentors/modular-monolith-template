using System;
using Modular.Abstractions.Events;

namespace Modular.Modules.Users.Core.Events
{
    internal record UserStateUpdated(Guid UserId, string State) : IEvent;
}