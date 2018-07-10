using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository authRepository;
        public AuthController(IAuthRepository authRepository)
        {
             this.authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto dto)
        {
            if (await this.authRepository.UserExists(dto.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await this.authRepository.Resgister(dto.Username, dto.Password);
            return StatusCode(201);
        }
    }
}