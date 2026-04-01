using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CineApi.Contexts;
using CineApi.Models;

namespace CineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sala_cineController : ControllerBase
    {
        private readonly CineContext _context;

        public sala_cineController(CineContext context)
        {
            _context = context;
        }

        // GET: api/sala_cine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<sala_cine>>> Getsala_cines()
        {
            return await _context.sala_cines.ToListAsync();
        }

        // GET: api/sala_cine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<sala_cine>> Getsala_cine(int id)
        {
            var sala_cine = await _context.sala_cines.FindAsync(id);

            if (sala_cine == null)
            {
                return NotFound();
            }

            return sala_cine;
        }

        // PUT: api/sala_cine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putsala_cine(int id, sala_cine sala_cine)
        {
            if (id != sala_cine.id_sala)
            {
                return BadRequest();
            }

            _context.Entry(sala_cine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sala_cineExists(id))
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

        // POST: api/sala_cine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<sala_cine>> Postsala_cine(sala_cine sala_cine)
        {
            _context.sala_cines.Add(sala_cine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getsala_cine", new { id = sala_cine.id_sala }, sala_cine);
        }

        // DELETE: api/sala_cine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletesala_cine(int id)
        {
            var sala_cine = await _context.sala_cines.FindAsync(id);
            if (sala_cine == null)
            {
                return NotFound();
            }

            _context.sala_cines.Remove(sala_cine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool sala_cineExists(int id)
        {
            return _context.sala_cines.Any(e => e.id_sala == id);
        }
    }
}
