using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TextExtractProject.Models;

namespace TextExtractProject.Controllers
{
    [EnableCors("CORSPolicy")]
    [Route("api/UserLoginController/")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly UserLoginContext _userLoginContext;
        public UserLoginController(UserLoginContext userLoginContext)
        {
            _userLoginContext = userLoginContext;
        }


        // GET: api/User/5
        [HttpGet("UserLogin/{id}")]
        public async Task<ActionResult<UserLogin>> GetUserLogin(long id)
        {
            if (_userLoginContext.UserLoginCredentials == null)
            {
                return NotFound();
            }
            var userLogin = await _userLoginContext.UserLoginCredentials.FindAsync(id);

            if (userLogin == null)
            {
                return NotFound();
            }

            return userLogin;
        }

        // GET: api/User
        [HttpGet("allUserLogins")]
        public async Task<ActionResult<IEnumerable<UserLogin>>> GetAllUsers()
        {
            if (_userLoginContext.UserLoginCredentials == null)
            {
                return NotFound();
            }
            return await _userLoginContext.UserLoginCredentials.ToListAsync();
        }

        // POST: api/UserLogin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserLogin>> PostUser(UserLogin userLogin)
        {
            if (_userLoginContext.UserLoginCredentials == null)
            {
                return Problem("Entity set 'UserLoginContext.UserLogin'  is null.");
            }
            _userLoginContext.UserLoginCredentials.Add(userLogin);
            await _userLoginContext.SaveChangesAsync();
            Console.WriteLine("UserLogin Table object created.");

            return CreatedAtAction(nameof(GetUserLogin), new { id = userLogin.Id }, userLogin);
        }

        
        [HttpPost("authentication")]
        public bool ValidateLogin([FromForm] string userName, [FromForm] string password)
        {
            return _userLoginContext.ValidateUserCredentials(userName, password);
        } 

    }   
}

