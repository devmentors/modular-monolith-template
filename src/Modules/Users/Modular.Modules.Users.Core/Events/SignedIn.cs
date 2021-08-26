using System;
using Modular.Abstractions.Events;

namespace Modular.Modules.Users.Core.Events
{
    internal record SignedIn(Guid UserId) : IEvent;
}