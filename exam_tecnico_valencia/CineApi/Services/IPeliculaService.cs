using CineApi.DTO;
using CineApi.Models;

namespace CineApi.Services
{
    public interface IPeliculaService
    {
        Task<List<pelicula>> GetAll();
        Task<pelicula?> GetById(int id);
        Task<List<pelicula>> Buscar(string nombre);
        Task<List<EstrenoDTO>> GetEstrenos(string fecha);
        Task<EstadoSalaDTO> GetEstadoSala(string nombreSala);
        Task<pelicula> Create(PeliculaDTO dto);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, pelicula pelicula);
    }
}