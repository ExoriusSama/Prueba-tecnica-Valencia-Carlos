using CineApi.DTO;
using CineApi.Models;

namespace CineApi.Services
{
    public interface ISalaCineService
    {
        Task<List<sala_cine>> GetAll();
        Task<sala_cine?> GetById(int id);
        Task<sala_cine> Create(SalaCineDTO dto);
        Task<bool> Update(int id, SalaCineDTO dto);
        Task<bool> Delete(int id);
    }
}