using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CineApi.Models;

public partial class pelicula_sala_cine
{
    public int id_pelicula_sala { get; set; }

    public int id_pelicula { get; set; }

    public int id_sala { get; set; }

    public DateOnly fecha_publicacion { get; set; }

    public DateOnly fecha_fin { get; set; }

    public bool estado { get; set; }

    [JsonIgnore]
    public virtual pelicula? id_peliculaNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual sala_cine? id_salaNavigation { get; set; } = null!;
}
