using System;
using System.ComponentModel.DataAnnotations;
using Modular.Abstractions.Commands;

namespace Modular.Modules.Users.Core.Commands
{
    internal record SignIn([Required] [EmailAddress] string Email, [Required] string Password) : ICommand
    {
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}