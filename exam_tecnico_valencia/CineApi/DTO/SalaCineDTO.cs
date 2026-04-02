using System.ComponentModel.DataAnnotations;

public class SalaCineDTO
{
    [Required]
    [MaxLength(100)]
    public string nombre { get; set; }

    public bool estado { get; set; }
}