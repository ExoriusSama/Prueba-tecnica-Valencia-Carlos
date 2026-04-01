import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ServCine {
  private peliculasUrl= 'http://localhost:5052/api/peliculas'
  private salacineUrl= 'http://localhost:5052/api/sala_cine'
  private asignarPeliCine= 'http://localhost:5052/api/pelicula_sala_cine'

  constructor(private http: HttpClient) {}

  //de las peliculas
  getPeliculas(): Observable<any> {
    return this.http.get(this.peliculasUrl);
  }

  getPelicula(id: number): Observable<any> {
    return this.http.get(`${this.peliculasUrl}/${id}`);
  }

  crearPelicula(data: any): Observable<any> {
    return this.http.post(this.peliculasUrl, data);
  }

  actualizarPelicula(id: number, data: any): Observable<any> {
    return this.http.put(`${this.peliculasUrl}/${id}`, data);
  }

  eliminarPelicula(id: number): Observable<any> {
    return this.http.delete(`${this.peliculasUrl}/${id}`);
  }


// del sala cine
  getSalas(): Observable<any> {
    return this.http.get(this.salacineUrl);
  }

  getSala(id: number): Observable<any> {
    return this.http.get(`${this.salacineUrl}/${id}`);
  }

  crearSala(data: any): Observable<any> {
    return this.http.post(this.salacineUrl, data);
  }

  actualizarSala(id: number, data: any): Observable<any> {
    return this.http.put(`${this.salacineUrl}/${id}`, data);
  }

  eliminarSala(id: number): Observable<any> {
    return this.http.delete(`${this.salacineUrl}/${id}`);
  }


//el de combinarlos
  getCartelera(): Observable<any> {
    return this.http.get(this.asignarPeliCine);
  }

  getCarteleraById(id: number): Observable<any> {
    return this.http.get(`${this.asignarPeliCine}/${id}`);
  }

  crearCartelera(data: any): Observable<any> {
    return this.http.post(this.asignarPeliCine, data);
  }

  actualizarCartelera(id: number, data: any): Observable<any> {
    return this.http.put(`${this.asignarPeliCine}/${id}`, data);
  }

  eliminarCartelera(id: number): Observable<any> {
    return this.http.delete(`${this.asignarPeliCine}/${id}`);
  }

}
