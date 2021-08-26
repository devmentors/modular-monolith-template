using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Modular.Modules.Users.Core.Entities;
using Modular.Modules.Users.Core.Events;
using Modular.Modules.Users.Core.Exceptions;
using Modular.Modules.Users.Core.Repositories;
using Modular.Modules.Users.Core.Services;
using Modular.Abstractions;
using Modular.Abstractions.Auth;
using Modular.Abstractions.Commands;
using Modular.Abstractions.Messaging;

namespace Modular.Modules.Users.Core.Commands.Handlers
{
    internal sealed class SignInHandler : ICommandHandler<SignIn>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthManager _authManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRequestStorage _userRequestStorage;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<SignInHandler> _logger;

        public SignInHandler(IUserRepository userRepository, IAuthManager authManager,
            IPasswordHasher<User> passwordHasher, IUserRequestStorage userRequestStorage, IMessageBroker messageBroker,
            ILogger<SignInHandler> logger)
        {
            _userRepository = userRepository;
            _authManager = authManager;
            _passwordHasher = passwordHasher;
            _userRequestStorage = userRequestStorage;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task HandleAsync(SignIn command, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetAsync(command.Email.ToLowerInvariant())
                .NotNull(() => new InvalidCredentialsException());

            if (user.State != UserState.Active)
            {
                throw new UserNotActiveException(user.Id);
            }

            if (_passwordHasher.VerifyHashedPassword(default, user.Password, command.Password) ==
                PasswordVerificationResult.Failed)
            {
                throw new InvalidCredentialsException();
            }

            var claims = new Dictionary<string, IEnumerable<string>>
            {
                ["permissions"] = user.Role.Permissions
            };

            var jwt = _authManager.CreateToken(user.Id, user.Role.Name, claims: claims);
            jwt.Email = user.Email;
            await _messageBroker.PublishAsync(new SignedIn(user.Id));
            _logger.LogInformation($"User with ID: '{user.Id}' has signed in.");
            _userRequestStorage.SetToken(command.Id, jwt);
        }
    }
}