using CineApi.DTO;
using CineApi.Models;

namespace CineApi.Services
{
    public interface IPeliculaSalaService
    {
        Task<List<pelicula_sala_cine>> GetAll();
        Task<pelicula_sala_cine?> GetById(int id);
        Task<pelicula_sala_cine> Create(PeliculaSalaDTO dto);
        Task<bool> Update(int id, PeliculaSalaDTO dto);
        Task<bool> Delete(int id);
    }
}
