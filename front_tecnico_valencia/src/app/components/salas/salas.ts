import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { ServCine } from '../../Services/serv-cine';
import { Menu } from "../../menu/menu";
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-salas',
  imports: [CommonModule, FormsModule, Menu],
  templateUrl: './salas.html',
  styleUrl: './salas.css',
})
export class Salas implements OnInit {

  salas: any[] = [];
searchText: string = '';
  salaModel = {
    id_sala: 0,
    nombre: '',
    estado: true
  };

  isEdit = false;

  private cdr = inject(ChangeDetectorRef);

  constructor(private api: ServCine) {}

  ngOnInit() {
    this.listarSalas();
  }

  listarSalas() {
    this.api.getSalas().subscribe({
      next: (data: any[]) => {
        this.salas = [...data];
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error al cargar salas', err);
        this.cdr.detectChanges();
      }
    });
  }

 guardarSala() {
  if (!this.salaModel.nombre || this.salaModel.nombre.trim() === '') {
    alert('El nombre de la sala es obligatorio');
    return;
  }

  if (this.isEdit) {
    this.api.actualizarSala(this.salaModel.id_sala, this.salaModel)
      .subscribe({
        next: () => {
          alert('Sala actualizada correctamente');
          this.resetForm();
          this.listarSalas();
        },
      });
  } else {
    this.api.crearSala(this.salaModel)
      .subscribe({
        next: () => {
          alert('Sala creada correctamente');
          this.resetForm();
          this.listarSalas();
        },
      });
  }
}

  editarSala(sala: any) {
    this.salaModel = { ...sala };
    this.isEdit = true;
    alert("Perfecto, ahora modifique el campo del formulario para actualizarlo");
  }

  eliminarSala(sala: any) {
    if (confirm(`¿Deseas desactivar la sala "${sala.nombre}"?`)) {
      const data = { ...sala, estado: false };

      this.api.actualizarSala(sala.id_sala, data)
        .subscribe({
          next: () => {
            alert('Sala desactivada correctamente');
            this.listarSalas();
          },
          error: (err) => console.error('Error al desactivar sala', err)
        });
    }
  }

  resetForm() {
    this.salaModel = {
      id_sala: 0,
      nombre: '',
      estado: true
    };
    this.isEdit = false;
  }
  salasFiltradas() {
  if (!this.searchText) {
    return this.salas;
  }

  return this.salas.filter(sala =>
    sala.nombre.toLowerCase().includes(this.searchText.toLowerCase())
  );
}
}
