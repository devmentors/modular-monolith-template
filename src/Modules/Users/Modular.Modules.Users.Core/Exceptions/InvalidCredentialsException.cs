using Modular.Abstractions.Exceptions;

namespace Modular.Modules.Users.Core.Exceptions
{
    internal class InvalidCredentialsException : ModularException
    {
        public InvalidCredentialsException() : base("Invalid credentials.")
        {
        }
    }
}