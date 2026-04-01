import { CommonModule } from '@angular/common';
import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { ServCine } from '../../Services/serv-cine';
import { FormsModule } from '@angular/forms';
import { Menu } from "../../menu/menu";

@Component({
  selector: 'app-peliculas',
  imports: [CommonModule, FormsModule, Menu],
  templateUrl: './peliculas.html',
  styleUrls: ['./peliculas.css'],
})
export class Peliculas implements OnInit {

  peliculas: any[] = [];
searchText: string = '';

  nuevaPelicula = {
    id_pelicula: 0,
    nombre: '',
    duracion: 0,
    estado: true
  };

  isEdit: boolean = false;

  private cdr = inject(ChangeDetectorRef);

  constructor(private api: ServCine) {}

  ngOnInit(): void {
    this.cargarPeliculas();
  }

  cargarPeliculas() {
    this.api.getPeliculas().subscribe({
      next: (data: any[]) => {
        this.peliculas = [...data];
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error al cargar películas', err);
        this.cdr.detectChanges();
      }
    });
  }
guardarPelicula() {

  if (!this.nuevaPelicula.nombre || this.nuevaPelicula.nombre.trim() === '') {
    alert('El nombre de la película es obligatorio');
    return;
  }
  if (this.nuevaPelicula.nombre.trim().length < 2) {
    alert('El nombre debe tener al menos 2 caracteres');
    return;
  }
  if (!this.nuevaPelicula.duracion || this.nuevaPelicula.duracion <= 0) {
    alert('La duración debe ser mayor a 0 minutos');
    return;
  }

  if (this.isEdit) {
    this.api.actualizarPelicula(
      this.nuevaPelicula.id_pelicula,
      this.nuevaPelicula
    ).subscribe({
      next: () => {
        alert('Película actualizada correctamente');
        this.resetForm();
        this.cargarPeliculas();
      },
    });

  } else {
    this.api.crearPelicula(this.nuevaPelicula).subscribe({
      next: () => {
        alert('Película creada correctamente');
        this.resetForm();
        this.cargarPeliculas();
      },
    });
  }
}

  editarPelicula(pelicula: any) {
    this.nuevaPelicula = { ...pelicula };
    this.isEdit = true;
    alert("Perfecto, ahora modifique el campo del formulario para actualizarlo");

  }

  eliminarPelicula(pelicula: any) {
    if (confirm(`¿Seguro que quieres eliminar "${pelicula.nombre}"?`)) {
      // Eliminación lógica (cambiar estado a false)
      this.api.actualizarPelicula(pelicula.id_pelicula, { ...pelicula, estado: false }).subscribe({
        next: () => {
          alert('Película eliminada correctamente');
          this.cargarPeliculas();
        },
        error: (err) => console.error('Error al eliminar', err)
      });
    }
  }

  resetForm() {
    this.nuevaPelicula = {
      id_pelicula: 0,
      nombre: '',
      duracion: 0,
      estado: true
    };
    this.isEdit = false;
  }
  peliculasFiltradas() {
  if (!this.searchText) {
    return this.peliculas;
  }

  return this.peliculas.filter(peli =>
    peli.nombre.toLowerCase().includes(this.searchText.toLowerCase())
  );
}
}
