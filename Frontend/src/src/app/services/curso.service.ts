import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Curso } from '../models/curso.model';

interface CursoApi {
  Id: number;
  Nome: string;
  CargaHoraria: number;
  Valor: number;
  DataInicio: string;
  Online: boolean;
  Ativo: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class CursoService {
  private apiUrl = 'http://localhost:59869/api/cursos';

  constructor(private http: HttpClient) { }

  listar(): Observable<Curso[]> {
    return this.http.get<CursoApi[]>(this.apiUrl).pipe(
      map(cursos => cursos.map(c => ({
        id: c.Id,
        nome: c.Nome,
        cargaHoraria: c.CargaHoraria,
        valor: c.Valor,
        dataInicio: c.DataInicio,
        online: c.Online,
        ativo: c.Ativo
      })))
    );
  }
}