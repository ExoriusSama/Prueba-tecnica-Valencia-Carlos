using CineApi.Contexts;
using CineApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CineApi.Repository
{
    public class PeliculaSalaRepository : IPeliculaSalaRepository
    {
        private readonly CineContext _context;

        public PeliculaSalaRepository(CineContext context)
        {
            _context = context;
        }

        public async Task<List<pelicula_sala_cine>> GetAll()
        {
            return await _context.pelicula_sala_cines.ToListAsync();
        }

        public async Task<pelicula_sala_cine?> GetById(int id)
        {
            return await _context.pelicula_sala_cines.FindAsync(id);
        }

        public async Task<pelicula_sala_cine> Create(pelicula_sala_cine entity)
        {
            _context.pelicula_sala_cines.Add(entity);
            await Save();
            return entity;
        }

        public async Task Update(pelicula_sala_cine entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Save();
        }

        public async Task Delete(pelicula_sala_cine entity)
        {
            _context.pelicula_sala_cines.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
