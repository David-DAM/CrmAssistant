using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrmAssistant.Models;

namespace CrmAssistant.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class ApiUsersController : ControllerBase
    {
        private readonly PubContext _context;

        public ApiUsersController(PubContext context)
        {
            _context = context;
        }

        // GET: api/ApiUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/ApiUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
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

        // PUT: api/ApiUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
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

        // POST: api/ApiUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'PubContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/ApiUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
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

        [HttpPost("{idUser}/hobbies/{idHobbie}")]
        public async Task<IActionResult> AddHobbie(int idUser, int idHobbie)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Include(u => u.Hobbies).Where(u => u.Id == idUser).FirstAsync();

            if (user == null)
            {
                return NotFound();
            }

            var hobbie = await _context.Hobbies.FindAsync(idHobbie);

            if (hobbie == null)
            {
                return NotFound();
            }

            user.Hobbies.Add(hobbie);

            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(idUser))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(hobbie);
        }

        [HttpDelete("{idUser}/hobbies/{idHobbie}")]
        public async Task<IActionResult> DeleteHobbie(int idUser, int idHobbie)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Include(u => u.Hobbies).Where(u => u.Id == idUser).FirstAsync();

            if (user == null)
            {
                return NotFound();
            }

            _context.Entry(user).State = EntityState.Modified;

            var hobbie = await _context.Hobbies.FindAsync(idHobbie);

            if (hobbie == null)
            {
                return NotFound();
            }

            user.Hobbies.Remove(hobbie);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(idUser))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
