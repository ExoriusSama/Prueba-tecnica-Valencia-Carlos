using System;
using System.Collections.Generic;

namespace CineApi.Models;

public partial class sala_cine
{
    public int id_sala { get; set; }

    public string nombre { get; set; } = null!;

    public bool estado { get; set; }

    public virtual ICollection<pelicula_sala_cine> pelicula_sala_cines { get; set; } = new List<pelicula_sala_cine>();
}
