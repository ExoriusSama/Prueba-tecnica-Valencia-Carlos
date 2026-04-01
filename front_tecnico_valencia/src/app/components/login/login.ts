import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
})
export class Login {
  usuario: string = '';
  password: string = '';
  mensaje: string = '';

  constructor(private router: Router) {}

  login() {
    const userDefault = 'admin';
    const passDefault = '1234';

    if (this.usuario === userDefault && this.password === passDefault) {
      localStorage.setItem('username', this.usuario);
      this.mensaje = '';
      this.router.navigate(['/dashboard']);
    } else {
      this.mensaje = 'Usuario o contraseña incorrectos';
    }
  }
}
