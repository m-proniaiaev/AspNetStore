using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Core.Contracts.Models;
using Store.Core.Contracts.Responses;
using Store.Core.Services.Authorization.Roles.Queries.CreateRole;
using Store.Core.Services.Authorization.Roles.Queries.DeleteRole;
using Store.Core.Services.Authorization.Roles.Queries.DisableRole;
using Store.Core.Services.Authorization.Roles.Queries.GetActions;
using Store.Core.Services.Authorization.Roles.Queries.GetRoles;
using Store.Core.Services.Authorization.Roles.Queries.UpdateRole;

namespace Store.WebApi.Authorization.Controllers
{
    [ApiController]
    [Route("api/auth/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetRolesResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetRolesResponse>> GetRoles([FromQuery] GetRolesQuery query, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        
        [HttpGet("getRole/{id:guid}")]
        [ProducesResponseType(typeof(Role), StatusCodes.Status200OK)]
        public async Task<ActionResult<Role>> GetRole([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRoleByIdQuery { Id = id }, cancellationToken);
            return Ok(result);
        }

        [HttpGet("GetActions")]
        [ProducesResponseType(typeof(GetActionsResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetActionsResponse>> GetActions([FromQuery] GetActionsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost("addRole")]
        [ProducesResponseType(typeof(Role), StatusCodes.Status201Created)]
        public async Task<ActionResult<Role>> CreateRole([FromBody] CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        
        [HttpPut("updateRole")]
        [ProducesResponseType(typeof(Role), StatusCodes.Status200OK)]
        public async Task<ActionResult<Role>> UpdateRole([FromBody] UpdateRoleCommand command, CancellationToken cts)
        {
            var result = await _mediator.Send(command, cts);
            return Ok(result);
        }
        
        [HttpPut("disableRole/{id:guid}")]
        [ProducesResponseType(typeof(Role), StatusCodes.Status200OK)]
        public async Task<ActionResult<Role>> DisableRole([FromRoute] Guid id, CancellationToken cts)
        {
            var result = await _mediator.Send(new DisableRoleCommand { Id = id }, cts);
            return Ok(result);
        }

        [HttpDelete("deleteRole/{id:guid}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Role>> DeleteRole([FromRoute] Guid id, CancellationToken cts)
        {
            await _mediator.Send(new DeleteRoleCommand { Id = id }, cts);

            return NoContent();
        }


    }
}