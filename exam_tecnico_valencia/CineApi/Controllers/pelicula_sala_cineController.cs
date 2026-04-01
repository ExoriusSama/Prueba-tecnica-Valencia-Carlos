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
    public class pelicula_sala_cineController : ControllerBase
    {
        private readonly CineContext _context;

        public pelicula_sala_cineController(CineContext context)
        {
            _context = context;
        }

        // GET: api/pelicula_sala_cine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<pelicula_sala_cine>>> Getpelicula_sala_cines()
        {
            return await _context.pelicula_sala_cines.ToListAsync();
        }

        // GET: api/pelicula_sala_cine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<pelicula_sala_cine>> Getpelicula_sala_cine(int id)
        {
            var pelicula_sala_cine = await _context.pelicula_sala_cines.FindAsync(id);

            if (pelicula_sala_cine == null)
            {
                return NotFound();
            }

            return pelicula_sala_cine;
        }

        // PUT: api/pelicula_sala_cine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpelicula_sala_cine(int id, pelicula_sala_cine pelicula_sala_cine)
        {
            if (id != pelicula_sala_cine.id_pelicula_sala)
            {
                return BadRequest();
            }

            _context.Entry(pelicula_sala_cine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pelicula_sala_cineExists(id))
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

        // POST: api/pelicula_sala_cine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<pelicula_sala_cine>> Postpelicula_sala_cine(pelicula_sala_cine pelicula_sala_cine)
        {
            _context.pelicula_sala_cines.Add(pelicula_sala_cine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getpelicula_sala_cine", new { id = pelicula_sala_cine.id_pelicula_sala }, pelicula_sala_cine);
        }

        // DELETE: api/pelicula_sala_cine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepelicula_sala_cine(int id)
        {
            var pelicula_sala_cine = await _context.pelicula_sala_cines.FindAsync(id);
            if (pelicula_sala_cine == null)
            {
                return NotFound();
            }

            _context.pelicula_sala_cines.Remove(pelicula_sala_cine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool pelicula_sala_cineExists(int id)
        {
            return _context.pelicula_sala_cines.Any(e => e.id_pelicula_sala == id);
        }
    }
}
