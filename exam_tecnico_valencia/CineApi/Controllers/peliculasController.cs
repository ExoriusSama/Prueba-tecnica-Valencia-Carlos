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
    public class peliculasController : ControllerBase
    {
        private readonly CineContext _context;

        public peliculasController(CineContext context)
        {
            _context = context;
        }

        // GET: api/peliculas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<pelicula>>> Getpeliculas()
        {
            return await _context.peliculas.ToListAsync();
        }

        // para buscar la pelicula con el nombre
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<pelicula>>> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest("Ingrese un nombre de la Pelicula a buscar");
            }

            var peliculas = await _context.peliculas
                .Where(p => p.nombre.Contains(nombre))
                .ToListAsync();

            if (peliculas.Count == 0)
            {
                return NotFound("No existe esa pelicula disponible");
            }
            return peliculas;
        }


        // GET: api/peliculas/estreno?fecha=2025-03-10
        [HttpGet("estreno")]
        public async Task<ActionResult<IEnumerable<object>>> GetPeliculasPorEstreno(string fecha)
        {
            if (string.IsNullOrEmpty(fecha))
            {
                return BadRequest("La fecha es requerida.");
            }

            // Convertimos a DateTime primero
            if (!DateTime.TryParse(fecha, out DateTime fechaParseada))
            {
                return BadRequest("El formato de fecha es invalido. Use YYYY-MM-DD.");
            }

            var fechaOnly = DateOnly.FromDateTime(fechaParseada);

            var peliculas = await _context.peliculas
                .Where(p => p.pelicula_sala_cines
                    .Any(psc => psc.fecha_publicacion == fechaOnly))
                .Select(p => new
                {
                    p.id_pelicula,
                    p.nombre,
                    p.duracion,
                    Fechas = p.pelicula_sala_cines
                        .Where(psc => psc.fecha_publicacion == fechaOnly)
                        .Select(psc => new
                        {
                            psc.fecha_publicacion,
                            psc.id_sala
                        })
                })
                .ToListAsync();

            if (peliculas.Count == 0)
            {
                return NotFound("No hay peliculas q se estrenen para esa fecha");
            }

            return peliculas;
        }
        // buscar por el nombre de la sala de cine y presentar
        [HttpGet("estado-sala")]
        public async Task<ActionResult<object>> GetEstadoSala(string nombreSala)
        {
            if (string.IsNullOrEmpty(nombreSala))
            {
                return BadRequest("Debe ingresar el nombre de la sala");
            }

            var sala = await _context.sala_cines
                .FirstOrDefaultAsync(s => s.nombre == nombreSala && s.estado);

            if (sala == null)
            {
                return NotFound("La sala que ingreso no existe");
            }

            var cantidadPeliculas = await _context.pelicula_sala_cines
                .CountAsync(psc => psc.id_sala == sala.id_sala && psc.estado);

            string mensaje;

            if (cantidadPeliculas < 3)
            {
                mensaje = "Sala disponible";
            }
            else if (cantidadPeliculas >= 3 && cantidadPeliculas <= 5)
            {
                mensaje = $"Sala con {cantidadPeliculas} peliculas asignadas";
            }
            else
            {
                mensaje = "Sala no disponible";
            }
            return new
            {
                sala = sala.nombre,
                totalPeliculas = cantidadPeliculas,
                estado = mensaje
            };
        }
        // DELETE: api/peliculas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepelicula(int id)
        {
            var pelicula = await _context.peliculas.FindAsync(id);

            if (pelicula == null)
            {
                return NotFound();
            }

            //Eliminación lógica
            pelicula.estado = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/peliculas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<pelicula>> Getpelicula(int id)
        {
            var pelicula = await _context.peliculas.FindAsync(id);

            if (pelicula == null)
            {
                return NotFound();
            }

            return pelicula;
        }

        // PUT: api/peliculas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpelicula(int id, pelicula pelicula)
        {
            if (id != pelicula.id_pelicula)
            {
                return BadRequest();
            }

            _context.Entry(pelicula).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!peliculaExists(id))
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

        // POST: api/peliculas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<pelicula>> Postpelicula(pelicula pelicula)
        {
            _context.peliculas.Add(pelicula);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getpelicula", new { id = pelicula.id_pelicula }, pelicula);
        }


        private bool peliculaExists(int id)
        {
            return _context.peliculas.Any(e => e.id_pelicula == id);
        }
    }
}
