using CineApi.Contexts;
using CineApi.DTO;
using CineApi.Models;
using CineApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaService _service;

        public PeliculasController(IPeliculaService service)
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

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar(string nombre)
            => Ok(await _service.Buscar(nombre));

        [HttpGet("estreno")]
        public async Task<IActionResult> Estreno(string fecha)
            => Ok(await _service.GetEstrenos(fecha));

        [HttpGet("estado-sala")]
        public async Task<IActionResult> EstadoSala(string nombreSala)
            => Ok(await _service.GetEstadoSala(nombreSala));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PeliculaDTO dto)
        {
            var result = await _service.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = result.id_pelicula }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, pelicula pelicula)
            => await _service.Update(id, pelicula) ? NoContent() : BadRequest();

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => await _service.Delete(id) ? NoContent() : NotFound();
    }
}
