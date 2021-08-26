using Modular.Abstractions.Exceptions;

namespace Modular.Modules.Users.Core.Exceptions
{
    internal class InvalidEmailException : ModularException
    {
        public string Email { get; }

        public InvalidEmailException(string email) : base($"State is invalid: '{email}'.")
        {
            Email = email;
        }
    }
}