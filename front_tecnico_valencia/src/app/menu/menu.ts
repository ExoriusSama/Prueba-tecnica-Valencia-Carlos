import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './menu.html',
  styleUrls: ['./menu.css'],
})
export class Menu {

  constructor(private router: Router) {}

  cerrarSesion() {
    const confirmacion = confirm('¿Está seguro que desea cerrar sesión?');

    if (confirmacion) {
      this.router.navigate(['/']);
    }
  }
}
