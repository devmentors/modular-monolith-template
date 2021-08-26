using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modular.Modules.Users.Core.Commands;
using Modular.Modules.Users.Core.DTO;
using Modular.Modules.Users.Core.Queries;
using Modular.Abstractions.Dispatchers;
using Modular.Abstractions.Queries;
using Modular.Infrastructure.Api;
using Swashbuckle.AspNetCore.Annotations;

namespace Modular.Modules.Users.Api.Controllers
{
    [Authorize(Policy)]
    internal class UsersController : BaseController
    {
        private const string Policy = "users";
        private readonly IDispatcher _dispatcher;

        public UsersController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        
        [HttpGet("{userId:guid}")]
        [SwaggerOperation("Get user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UserDetailsDto>> GetAsync(Guid userId)
            => OkOrNotFound(await _dispatcher.QueryAsync(new GetUser {UserId = userId}));

        [HttpGet]
        [SwaggerOperation("Browse users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Paged<UserDto>>> BrowseAsync([FromQuery] BrowseUsers query)
            => Ok(await _dispatcher.QueryAsync(query));

        [HttpPut("{userId:guid}/state")]
        [SwaggerOperation("Update user state")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateStateAsync(Guid userId, UpdateUserState command)
        {
            await _dispatcher.SendAsync(command.Bind(x => x.UserId, userId));
            return NoContent();
        }
    }
}