using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthRepository authRepository;
        public AuthController(AuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string usarname, string password) {
            usarname = usarname.ToLower();

            if(await this.authRepository.UserExists(usarname))
            {
                return BadRequest("Username is already taken");
            }
            await this.authRepository.Resgister(usarname, password);
            return StatusCode(201);
        }

    }
}