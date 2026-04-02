using System.ComponentModel.DataAnnotations;

namespace CineApi.DTO
{
    public class PeliculaSalaDTO
    {
        [Required]
        public int IdPelicula { get; set; }

        [Required]
        public int IdSala { get; set; }

        [Required]
        public DateOnly FechaPublicacion { get; set; }

        [Required]
        public DateOnly FechaFin { get; set; }
    }
}
