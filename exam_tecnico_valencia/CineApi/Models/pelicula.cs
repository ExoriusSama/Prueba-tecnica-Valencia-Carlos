using System;
using System.Collections.Generic;

namespace CineApi.Models;

public partial class pelicula
{
    public int id_pelicula { get; set; }

    public string nombre { get; set; } = null!;

    public int duracion { get; set; }

    public bool estado { get; set; }

    public virtual ICollection<pelicula_sala_cine> pelicula_sala_cines { get; set; } = new List<pelicula_sala_cine>();
}
