using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TextExtractProject.Models;

namespace TextExtractProject.Controllers
{
    [EnableCors("CORSPolicy")]
    [Route("api/UserController/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        private UserLoginController loginController;

        public UserController(UserContext context, UserLoginContext userLoginContext)
        {
            _context = context;
            loginController = new UserLoginController(userLoginContext);
        }


        // GET: api/User
        [HttpGet("allUser")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'UserContext.User'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult<User>> PostUserFirstTime([FromForm] string fullName, [FromForm] string userName, [FromForm] string password)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'UserContext.User'  is null.");
            }
            User user = new User(userName, fullName);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            Console.WriteLine("User Table object created.");

            long userId = user.Id;
            UserLogin userLogin = new UserLogin(userId, userName, password);
            await loginController.PostUser(userLogin);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
