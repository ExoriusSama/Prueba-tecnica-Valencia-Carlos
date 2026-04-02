using CineApi.Models;

namespace CineApi.Repository
{
    public interface ISalaCineRepository
    {
        Task<List<sala_cine>> GetAll();
        Task<sala_cine?> GetById(int id);
        Task<sala_cine> Create(sala_cine sala);
        Task Update(sala_cine sala);
        Task Delete(sala_cine sala);
        Task Save();
    }
}
