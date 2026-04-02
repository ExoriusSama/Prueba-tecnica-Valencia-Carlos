using CineApi.Contexts;
using CineApi.DTO;
using CineApi.Models;
using CineApi.Repository;
using Microsoft.EntityFrameworkCore;


namespace CineApi.Services
{
    public class PeliculaSalaService : IPeliculaSalaService
    {
        private readonly IPeliculaSalaRepository _repo;
        private readonly CineContext _context;

        public PeliculaSalaService(IPeliculaSalaRepository repo, CineContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<List<pelicula_sala_cine>> GetAll()
            => await _repo.GetAll();

        public async Task<pelicula_sala_cine?> GetById(int id)
            => await _repo.GetById(id);

        public async Task<pelicula_sala_cine> Create(PeliculaSalaDTO dto)
        {

            if (dto.FechaFin < dto.FechaPublicacion)
                throw new ArgumentException("Fecha fin no puede ser menor");

            var peliculaExiste = await _context.peliculas.AnyAsync(p => p.id_pelicula == dto.IdPelicula);
            if (!peliculaExiste)
                throw new ArgumentException("Película no existe");

            var salaExiste = await _context.sala_cines.AnyAsync(s => s.id_sala == dto.IdSala);
            if (!salaExiste)
                throw new ArgumentException("Sala no existe");

            var entity = new pelicula_sala_cine
            {
                id_pelicula = dto.IdPelicula,
                id_sala = dto.IdSala,
                fecha_publicacion = dto.FechaPublicacion,
                fecha_fin = dto.FechaFin,
                estado = true
            };

            return await _repo.Create(entity);
        }

        public async Task<bool> Update(int id, PeliculaSalaDTO dto)
        {
            var entity = await _repo.GetById(id);

            if (entity == null)
                return false;

            if (dto.FechaFin < dto.FechaPublicacion)
                throw new ArgumentException("Fecha inválida");

            var peliculaExiste = await _context.peliculas
                .AnyAsync(p => p.id_pelicula == dto.IdPelicula);

            if (!peliculaExiste)
                throw new ArgumentException("Película no existe");

            var salaExiste = await _context.sala_cines
                .AnyAsync(s => s.id_sala == dto.IdSala);

            if (!salaExiste)
                throw new ArgumentException("Sala no existe");

            entity.id_pelicula = dto.IdPelicula;
            entity.id_sala = dto.IdSala;
            entity.fecha_publicacion = dto.FechaPublicacion;
            entity.fecha_fin = dto.FechaFin;

            await _repo.Update(entity);

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repo.GetById(id);

            if (entity == null)
                return false;

            entity.estado = false;

            await _repo.Update(entity);
            return true;
        }
    }
}