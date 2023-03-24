using Microsoft.AspNetCore.Mvc;
using NetCoreAPIPostgre.Data.Data;
using NetCoreAPIPostgre.Model;

namespace NetCoreAPIPostgreSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserData _userData;

        public UserController(IUserData userData)
        {
            _userData = userData;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() 
        {
            return Ok(await _userData.GetAllUser()); // 200 = Está bien o 500 = Está mal
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsersDetails(int id)
        {
            return Ok(await _userData.GetUserDetails(id)); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user) // FromBody = El objeto debe ser cargado desde el cuerpo del request
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _userData.InsertUser(user);

            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user) // FromBody = El objeto debe ser cargado desde el cuerpo del request
        {
            if (user == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userData.UpdateUser(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id) // FromBody = El objeto debe ser cargado desde el cuerpo del request
        {
            
            await _userData.DeleteUser(new User { UserId = id });

            return NoContent();
        }
    }
}
