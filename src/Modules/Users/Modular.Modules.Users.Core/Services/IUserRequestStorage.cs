using System;
using Modular.Abstractions.Auth;

namespace Modular.Modules.Users.Core.Services
{
    internal interface IUserRequestStorage
    {
        void SetToken(Guid commandId, JsonWebToken jwt);
        JsonWebToken GetToken(Guid commandId);
    }
}