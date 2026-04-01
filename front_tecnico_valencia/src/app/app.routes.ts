import { Routes } from '@angular/router';
import { Menu } from './menu/menu';
import { Peliculas } from './components/peliculas/peliculas';
import { Dashboard } from './components/dashboard/dashboard';
import { Salas } from './components/salas/salas';
import { AsignarPeliCine } from './components/asignar-peli-cine/asignar-peli-cine';
import { Login } from './components/login/login';

export const routes: Routes = [
  { path: '', component: Login },
  {path: 'peliculas',component: Peliculas},
  {path:'dashboard',component:Dashboard},
  {path:'salas', component: Salas },
  {path:'asignar-peli-cine',component:AsignarPeliCine},
  { path: '**', redirectTo: '' }
];
