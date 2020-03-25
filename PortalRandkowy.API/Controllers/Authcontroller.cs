using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Models;

namespace PortalRandkowy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authcontroller : ControllerBase
    {
        private readonly IAuthRepository _repository;
        public Authcontroller(IAuthRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto) 
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if(await _repository.UserExists(userForRegisterDto.Username))
                return BadRequest("Użytkownik o takiej nazwie juz istnieje!");

            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repository.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }
    }
}