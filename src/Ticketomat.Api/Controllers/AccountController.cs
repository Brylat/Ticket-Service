using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ticketomat.Infrastructure.Commands.Users;
using Ticketomat.Infrastructure.Services;

namespace Ticketomat.Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserServices _userServices;

        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            throw new NotImplementedException();
        }
        [HttpGet("tickets")]
        public async Task<ActionResult> GetTickets()
        {
            throw new NotImplementedException();
        }
        [HttpGet("register")]
        public async Task<ActionResult> Post(Register command)
        {
            await _userServices.RegisterAsync(Guid.NewGuid(), command.Email, command.Name, command.Password, command.Role);

            return Created("/account", null);
        }
        [HttpGet("login")]
        public async Task<ActionResult> Post(Login command)
            => Json(await _userServices.LoginAsync(command.Email, command.Password));
    }
}