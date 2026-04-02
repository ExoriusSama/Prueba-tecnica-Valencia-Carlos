using CineApi.Contexts;
using CineApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CineApi.Repository
{
    public class SalaCineRepository : ISalaCineRepository
    {
        private readonly CineContext _context;

        public SalaCineRepository(CineContext context)
        {
            _context = context;
        }

        public async Task<List<sala_cine>> GetAll()
        {
            return await _context.sala_cines.ToListAsync();
        }

        public async Task<sala_cine?> GetById(int id)
        {
            return await _context.sala_cines.FindAsync(id);
        }

        public async Task<sala_cine> Create(sala_cine sala)
        {
            _context.sala_cines.Add(sala);
            await Save();
            return sala;
        }

        public async Task Update(sala_cine sala)
        {
            _context.Entry(sala).State = EntityState.Modified;
            await Save();
        }

        public async Task Delete(sala_cine sala)
        {
            _context.sala_cines.Remove(sala);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
