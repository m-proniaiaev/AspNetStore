using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Contracts.Responses;
using Store.Core.Services.Authorization.Users.Commands;

namespace Store.WebApi.Authorization.Controllers
{
    [ApiController]
    [Route("api/internal/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [AllowAnonymous]
        [HttpPost("LogIn")]
        [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
        public async Task<ActionResult<LoginResult>> LogIn([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        
        [Authorize]
        [HttpPost("LogOut")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status200OK)]
        public async Task<ActionResult> LogOut(CancellationToken cancellationToken)
        {
            await _mediator.Send(new LogoutCommand(), cancellationToken);
            return NoContent();
        }
    }
}