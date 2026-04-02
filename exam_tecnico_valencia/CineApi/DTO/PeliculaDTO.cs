using System.ComponentModel.DataAnnotations;

namespace CineApi.DTO
{
    public class PeliculaDTO
    {
        [Required]
        [MaxLength(150)]
        public string nombre { get; set; }

        [Range(1, 1000)]
        public int duracion { get; set; }
    }

    public class EstrenoDTO
    {
        public int idPelicula { get; set; }
        public string nombre { get; set; }
        public int duracion { get; set; }
        public List<FechaDTO> fechas { get; set; }
    }

    public class FechaDTO
    {
        public DateOnly fechaPublicacion { get; set; }
        public int IdSala { get; set; }
    }

    public class EstadoSalaDTO
    {
        public string sala { get; set; }
        public int totalPeliculas { get; set; }
        public string estado { get; set; }
    }
}