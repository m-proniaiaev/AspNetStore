using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Contracts.Responses;
using Store.Core.Services.Authorization.Users.Queries;
using System.Threading;
using System.Threading.Tasks;
using Store.Core.Contracts.Domain;
using Store.Core.Host.Authorization;
using Store.Core.Services.Authorization.Users.Commands;
using Store.Core.Services.Authorization.Users.Commands.CreateUser;
using Store.Core.Services.Authorization.Users.Commands.Update;

namespace Store.WebApi.Authorization.Controllers
{
    [ApiController]
    [Route("api/internal/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ActionRequired("Users-Get")]
        [HttpGet]
        [ProducesResponseType(typeof(GetUsersResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetUsersResponse>> GetUsers( [FromQuery] GetUsersQuery query, CancellationToken cts)
        {
            var result = await _mediator.Send(query, cts);

            return Ok(result);
        }
        
        [ActionRequired("User-Get")]
        [HttpGet("getUser/{id:guid}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> GetUser([FromRoute] Guid id, CancellationToken cts)
        {
            var result = await _mediator.Send(new GetUserByIdQuery { Id = id}, cts);

            return Ok(result);
        }
        
        [ActionRequired("User-Create")]
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        public async Task<ActionResult<User>> CreateUser([FromBody] CreateUserCommand command, CancellationToken cts)
        {
            var result = await _mediator.Send(command, cts);

            return Ok(result);
        }
        
        [ActionRequired("User-Update")]
        [HttpPut("updateUser")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> UpdateUser([FromBody] UpdateUserCommand command, CancellationToken cts)
        {
            var result = await _mediator.Send(command, cts);

            return Ok(result);
        }
        
        [ActionRequired("User-UpdateRole")]
        [HttpPut("updateUserRole")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> UpdateUserRole([FromBody] ChangeUserRoleCommand command, CancellationToken cts)
        {
            var result = await _mediator.Send(command, cts);

            return Ok(result);
        }
        
        [ActionRequired("User-UpdatePassword")]
        [HttpPut("updateUserPassword")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> UpdateUserPassword([FromBody] ChangeUserPasswordCommand command, CancellationToken cts)
        {
            var result = await _mediator.Send(command, cts);

            return Ok(result);
        }
        
        [ActionRequired("User-Disable")]
        [HttpPut("markAsDisabled/{id:guid}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> MarkAsDisabled([FromRoute] Guid id, CancellationToken cts)
        {
            var result = await _mediator.Send(new MarkUserAsDisabledCommand { Id = id }, cts);

            return Ok(result);
        }
        
        [ActionRequired("User-Delete")]
        [HttpDelete("deleteUser/{id:guid}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken cts)
        {
            await _mediator.Send(new DeleteUserCommand { Id = id }, cts);

            return NoContent();
        }
    }
}