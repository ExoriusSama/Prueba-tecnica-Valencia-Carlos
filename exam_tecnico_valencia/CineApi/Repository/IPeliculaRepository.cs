using CineApi.DTO;
using CineApi.Models;

namespace CineApi.Repository
{
    public interface IPeliculaRepository
    {
        Task<List<pelicula>> GetAll();
        Task<pelicula?> GetById(int id);
        Task<List<pelicula>> Buscar(string nombre);
        Task<List<EstrenoDTO>> GetEstrenos(DateOnly fecha);
        Task<EstadoSalaDTO?> GetEstadoSala(string nombreSala);
        Task<pelicula> Create(pelicula entity);
        Task Update(pelicula entity);
        Task Save();
    }
}
