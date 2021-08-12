using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Store.WebApi.Authorization.Controllers
{
    [ApiController]
    [Route("api/auth/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}