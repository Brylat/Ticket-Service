using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketomat.Infrastructure.Commands.Users;
using Ticketomat.Infrastructure.Services;

namespace Ticketomat.Api.Controllers
{
    [Route("[controller]")]
    public class AccountController : ApiControlerBase
    {
        private readonly IUserServices _userServices;

        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Get()
            => Json(await _userServices.GetAcountAsync(UserId));

        [HttpGet("tickets")]
        public async Task<ActionResult> GetTickets()
        {
            throw new NotImplementedException();
        }
        [HttpPost("register")]
        public async Task<ActionResult> Post([FromBody]Register command)
        {
            await _userServices.RegisterAsync(Guid.NewGuid(), command.Email, command.Name, command.Password, command.Role);

            return Created("/account", null);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Post([FromBody]Login command)
            => Json(await _userServices.LoginAsync(command.Email, command.Password));
    }
}