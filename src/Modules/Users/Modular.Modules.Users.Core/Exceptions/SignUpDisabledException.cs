using Modular.Abstractions.Exceptions;

namespace Modular.Modules.Users.Core.Exceptions
{
    internal class SignUpDisabledException : ModularException
    {
        public SignUpDisabledException() : base("Sign up is disabled.")
        {
        }
    }
}