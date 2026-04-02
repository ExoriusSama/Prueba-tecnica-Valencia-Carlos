import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Menu } from '../../menu/menu';
import { CommonModule } from '@angular/common';
import { ServCine } from '../../Services/serv-cine';

@Component({
  selector: 'app-asignar-peli-cine',
  imports: [CommonModule, FormsModule, Menu],
  templateUrl: './asignar-peli-cine.html',
  styleUrl: './asignar-peli-cine.css',
})
export class AsignarPeliCine implements OnInit {
  peliculas: any[] = [];
  salas: any[] = [];
  asignaciones: any[] = [];
  searchText: string = '';

  asignacionModel: any = {
    id_pelicula_sala: 0,
    id_pelicula: 0,
    id_sala: 0,
    fecha_publicacion: '',
    fecha_fin: '',
    estado: true
  };

  isEdit: boolean = false;
  private cdr = inject(ChangeDetectorRef);
  constructor(private api: ServCine) { }

  ngOnInit(): void {
    this.cargarPeliculas();
    this.cargarSalas();
    this.cargarAsignaciones();
  }
  getNombrePelicula(id: number): string {
    const peli = this.peliculas.find(p => p.id_pelicula === id);
    return peli ? peli.nombre : 'N/A';
  }
  getNombreSala(id: number): string {
    const sala = this.salas.find(s => s.id_sala === id);
    return sala ? sala.nombre : 'N/A';
  }
  cargarPeliculas() {
    this.api.getPeliculas().subscribe({
      next: (data: any) => {
        this.peliculas = data.filter((p: any) => p.estado);
        this.cdr.detectChanges();
      },
      error: (err: any) => {
        console.error(err);
        this.cdr.detectChanges();
      }
    });
  }

  cargarSalas() {
    this.api.getSalas().subscribe({
      next: (data: any) => {
        this.salas = data.filter((s: any) => s.estado);
        this.cdr.detectChanges();
      },
      error: (err: any) => {
        console.error(err);
        this.cdr.detectChanges();
      }
    });
  }

  cargarAsignaciones() {
    this.api.getCartelera().subscribe({
      next: (data: any) => {
        this.asignaciones = data;
        this.cdr.detectChanges();
      },
      error: (err: any) => {
        console.error(err);
        this.cdr.detectChanges();
      }
    });
  }

  guardarAsignacion() {
    if (
      this.asignacionModel.id_pelicula === 0 ||
      this.asignacionModel.id_sala === 0 ||
      !this.asignacionModel.fecha_publicacion ||
      !this.asignacionModel.fecha_fin
    ) {
      alert('Todos los campos son obligatorios');
      return;
    }

    if (this.isEdit) {

      const data = {
        idPelicula: this.asignacionModel.id_pelicula,
        idSala: this.asignacionModel.id_sala,
        fechaPublicacion: this.asignacionModel.fecha_publicacion,
        fechaFin: this.asignacionModel.fecha_fin
      };

      this.api.actualizarCartelera(
        this.asignacionModel.id_pelicula_sala,
        data
      ).subscribe({
        next: () => {
          this.cargarAsignaciones();
          this.limpiar();
        },
        error: (err: any) => console.error(err)
      });

    } else {

      const nuevaAsignacion = {
        idPelicula: this.asignacionModel.id_pelicula,
        idSala: this.asignacionModel.id_sala,
        fechaPublicacion: this.asignacionModel.fecha_publicacion,
        fechaFin: this.asignacionModel.fecha_fin
      };

      this.api.crearCartelera(nuevaAsignacion).subscribe({
        next: () => {
          this.cargarAsignaciones();
          this.limpiar();
        },
        error: (err: any) => console.error(err)
      });
    }
  }
  // Editar asignación
  editarAsignacion(asignacion: any) {
    this.asignacionModel = { ...asignacion };
    this.isEdit = true;
    alert("Perfecto, ahora modifique el campo del formulario para actualizarlo");

  }

eliminarAsignacion(asignacion: any) {
  if (confirm(`¿Seguro que quieres eliminar esta asignación?`)) {

    this.api.eliminarCartelera(asignacion.id_pelicula_sala).subscribe({
      next: () => this.cargarAsignaciones(),
      error: (err: any) => console.error(err)
    });

  }
}
  limpiar() {
    this.asignacionModel = {
      id_pelicula_sala: 0,
      id_pelicula: 0,
      id_sala: 0,
      fecha_publicacion: '',
      fecha_fin: '',
      estado: true
    };
    this.isEdit = false;
  }
  asignacionesFiltradas() {
    if (!this.searchText) {
      return this.asignaciones;
    }

    const texto = this.searchText.toLowerCase();

    return this.asignaciones.filter(asig => {
      const nombrePelicula = this.getNombrePelicula(asig.id_pelicula).toLowerCase();
      const nombreSala = this.getNombreSala(asig.id_sala).toLowerCase();

      return (
        nombrePelicula.includes(texto) ||
        nombreSala.includes(texto)
      );
    });
  }
}
