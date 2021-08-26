using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Modular.Modules.Users.Core.Entities;
using Modular.Modules.Users.Core.Events;
using Modular.Modules.Users.Core.Exceptions;
using Modular.Modules.Users.Core.Repositories;
using Modular.Abstractions;
using Modular.Abstractions.Commands;
using Modular.Abstractions.Messaging;

namespace Modular.Modules.Users.Core.Commands.Handlers
{
    internal sealed class UpdateUserStateHandler : ICommandHandler<UpdateUserState>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<UpdateUserStateHandler> _logger;

        public UpdateUserStateHandler(IUserRepository userRepository, IMessageBroker messageBroker,
            ILogger<UpdateUserStateHandler> logger)
        {
            _userRepository = userRepository;
            _messageBroker = messageBroker;
            _logger = logger;
        }

        public async Task HandleAsync(UpdateUserState command, CancellationToken cancellationToken = default)
        {
            if (!Enum.TryParse<UserState>(command.State, true, out var state))
            {
                throw new InvalidUserStateException(command.State);
            }

            var user = await _userRepository.GetAsync(command.UserId)
                .NotNull(() => new UserNotFoundException(command.UserId));

            var previousState = user.State;
            if (previousState == state)
            {
                return;
            }
            
            if (user.RoleId == Role.Admin)
            {
                throw new UserStateCannotBeChangedException(command.State, command.UserId);
            }

            user.State = state;
            await _userRepository.UpdateAsync(user);
            await _messageBroker.PublishAsync(new UserStateUpdated(user.Id, state.ToString().ToLowerInvariant()));
            _logger.LogInformation($"Updated state for user with ID: '{user.Id}', '{previousState}' -> '{user.State}'.");
        }
    }
}