using CineApi.Models;

namespace CineApi.Repository
{
    public interface IPeliculaSalaRepository
    {
        Task<List<pelicula_sala_cine>> GetAll();
        Task<pelicula_sala_cine?> GetById(int id);
        Task<pelicula_sala_cine> Create(pelicula_sala_cine entity);
        Task Update(pelicula_sala_cine entity);
        Task Delete(pelicula_sala_cine entity);
        Task Save();
    }
}
