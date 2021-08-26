using Modular.Abstractions.Exceptions;

namespace Modular.Modules.Users.Core.Exceptions
{
    internal class EmailInUseException : ModularException
    {
        public EmailInUseException() : base("Email is already in use.")
        {
        }
    }
}