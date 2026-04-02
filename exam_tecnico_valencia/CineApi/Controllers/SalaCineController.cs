using CineApi.DTO;
using CineApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaCineController : ControllerBase
    {
        private readonly ISalaCineService _service;

        public SalaCineController(ISalaCineService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _service.GetById(id);
            return data == null ? NotFound() : Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SalaCineDTO dto)
        {
            var result = await _service.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = result.id_sala }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SalaCineDTO dto)
        {
            var ok = await _service.Update(id, dto);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.Delete(id);
            return ok ? NoContent() : NotFound();
        }
    }
}