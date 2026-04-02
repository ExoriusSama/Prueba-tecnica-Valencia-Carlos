using CineApi.DTO;
using CineApi.Models;
using CineApi.Repository;

namespace CineApi.Services
{
    public class SalaCineService : ISalaCineService
    {
        private readonly ISalaCineRepository _repo;

        public SalaCineService(ISalaCineRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<sala_cine>> GetAll()
            => await _repo.GetAll();

        public async Task<sala_cine?> GetById(int id)
            => await _repo.GetById(id);

        public async Task<sala_cine> Create(SalaCineDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.nombre))
                throw new ArgumentException("Nombre requerido");

            var sala = new sala_cine
            {
                nombre = dto.nombre,
                estado = true
            };

            return await _repo.Create(sala);
        }

        public async Task<bool> Update(int id, SalaCineDTO dto)
        {
            var sala = await _repo.GetById(id);

            if (sala == null)
                return false;

            sala.nombre = dto.nombre;
            sala.estado = dto.estado;

            await _repo.Update(sala);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var sala = await _repo.GetById(id);

            if (sala == null)
                return false;

            sala.estado = false;

            await _repo.Update(sala);
            return true;
        }
    }
}