using Modular.Abstractions.Exceptions;

namespace Modular.Modules.Users.Core.Exceptions
{
    internal class InvalidPasswordException : ModularException
    {
        public string Reason { get; }

        public InvalidPasswordException(string reason) : base($"Invalid password: {reason}.")
        {
            Reason = reason;
        }
    }
}