using CineApi.Contexts;
using CineApi.Models;
using CineApi.DTO;
using Microsoft.EntityFrameworkCore;

namespace CineApi.Repository
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly CineContext _context;

        public PeliculaRepository(CineContext context)
        {
            _context = context;
        }

        public async Task<List<pelicula>> GetAll()
        {
            return await _context.peliculas.ToListAsync();
        }

        public async Task<pelicula?> GetById(int id)
        {
            return await _context.peliculas.FindAsync(id);
        }

        public async Task<List<pelicula>> Buscar(string nombre)
        {
            return await _context.peliculas
                .Where(p => p.nombre.Contains(nombre))
                .ToListAsync();
        }

        public async Task<List<EstrenoDTO>> GetEstrenos(DateOnly fecha)
        {
            return await _context.peliculas
                .Where(p => p.pelicula_sala_cines
                    .Any(psc => psc.fecha_publicacion == fecha))
                .Select(p => new EstrenoDTO
                {
                    idPelicula = p.id_pelicula,
                    nombre = p.nombre,
                    duracion = p.duracion,
                    fechas = p.pelicula_sala_cines
                        .Where(psc => psc.fecha_publicacion == fecha)
                        .Select(psc => new FechaDTO
                        {
                            fechaPublicacion = psc.fecha_publicacion,
                            IdSala = psc.id_sala
                        }).ToList()
                })
                .ToListAsync();
        }
        public async Task<EstadoSalaDTO?> GetEstadoSala(string nombreSala)
        {
            var sala = await _context.sala_cines
                .FirstOrDefaultAsync(s => s.nombre == nombreSala && s.estado);

            if (sala == null)
                return null;

            var cantidad = await _context.pelicula_sala_cines
                .CountAsync(psc => psc.id_sala == sala.id_sala && psc.estado);

            string estado = cantidad < 3
                ? "Sala disponible"
                : cantidad <= 5
                    ? $"Sala con {cantidad} peliculas asignadas"
                    : "Sala no disponible";

            return new EstadoSalaDTO
            {
                sala = sala.nombre,
                totalPeliculas = cantidad,
                estado = estado
            };
        }

        public async Task<pelicula> Create(pelicula entity)
        {
            _context.peliculas.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(pelicula entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}