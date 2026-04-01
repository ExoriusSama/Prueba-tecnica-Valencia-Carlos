import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { ServCine } from '../../Services/serv-cine';
import { Menu } from '../../menu/menu';

@Component({
  selector: 'app-dashboard',
  imports: [Menu],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  totalSalas: number = 0;
  salasDisponibles: number = 0;
  totalPeliculas: number = 0;

  private cdr = inject(ChangeDetectorRef);

  constructor(private api: ServCine) {}

  ngOnInit(): void {
    this.cargarIndicadores();
  }

  cargarIndicadores() {
    this.api.getSalas().subscribe({
      next: (salas: any[]) => {
        this.totalSalas = salas.length;
        this.salasDisponibles = salas.filter(s => s.estado === true || s.estado === 1).length;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error al cargar salas', err);
        this.cdr.detectChanges();
      }
    });

    // Total de películas
    this.api.getPeliculas().subscribe({
      next: (peliculas: any[]) => {
        this.totalPeliculas = peliculas.length;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error al cargar películas', err);
        this.cdr.detectChanges();
      }
    });
  }
}
