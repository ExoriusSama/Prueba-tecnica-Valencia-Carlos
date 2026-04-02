using CineApi.Contexts;
using CineApi.DTO;
using CineApi.Models;
using CineApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace CineApi.Services
{
    public class PeliculaService : IPeliculaService
    {
        private readonly IPeliculaRepository _repo;

        public PeliculaService(IPeliculaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<pelicula>> GetAll()
            => await _repo.GetAll();

        public async Task<pelicula?> GetById(int id)
            => await _repo.GetById(id);

        public async Task<List<pelicula>> Buscar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("Ingrese un nombre");

            return await _repo.Buscar(nombre);
        }

        public async Task<List<EstrenoDTO>> GetEstrenos(string fecha)
        {
            if (!DateTime.TryParse(fecha, out DateTime fechaParseada))
                throw new ArgumentException("Formato inválido");

            return await _repo.GetEstrenos(DateOnly.FromDateTime(fechaParseada));
        }

        public async Task<EstadoSalaDTO> GetEstadoSala(string nombreSala)
        {
            var result = await _repo.GetEstadoSala(nombreSala);

            if (result == null)
                throw new ArgumentException("Sala no existe");

            return result;
        }

        public async Task<pelicula> Create(PeliculaDTO dto)
        {
            var peli = new pelicula
            {
                nombre = dto.nombre,
                duracion = dto.duracion,
                estado = true
            };

            return await _repo.Create(peli);
        }

        public async Task<bool> Delete(int id)
        {
            var pelicula = await _repo.GetById(id);

            if (pelicula == null)
                return false;

            pelicula.estado = false;
            await _repo.Save();

            return true;
        }

        public async Task<bool> Update(int id, pelicula pelicula)
        {
            if (id != pelicula.id_pelicula)
                return false;

            await _repo.Update(pelicula);
            return true;
        }
    }
}
