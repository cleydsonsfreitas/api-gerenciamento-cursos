import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CursoService } from '../services/curso.service';
import { Curso } from '../models/curso.model';

@Component({
  selector: 'app-cursos',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div style="max-width: 1100px; margin: 40px auto; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; padding: 0 20px;">
      <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 25px;">
        <h2 style="color: #2c3e50; margin: 0; font-size: 24px;">Gerenciamento de Cursos</h2>
        <span style="background-color: #3498db; color: white; padding: 6px 14px; border-radius: 20px; font-size: 14px; font-weight: 600;">
          Total: {{ cursos().length }} Cursos
        </span>
      </div>

      <div style="background: white; border-radius: 8px; box-shadow: 0 4px 6px rgba(0,0,0,0.1); overflow: hidden;">
        <table style="width: 100%; border-collapse: collapse; text-align: left;">
          <thead>
            <tr style="background-color: #2c3e50; color: white; font-size: 14px;">
              <th style="padding: 15px;">ID</th>
              <th style="padding: 15px;">Nome do Curso</th>
              <th style="padding: 15px;">Carga Horária</th>
              <th style="padding: 15px;">Valor</th>
              <th style="padding: 15px;">Início</th>
              <th style="padding: 15px; text-align: center;">Modalidade</th>
              <th style="padding: 15px; text-align: center;">Status</th>
            </tr>
          </thead>
          <tbody>
            <tr *ngFor="let c of cursos(); let i = index" [style.background-color]="i % 2 === 0 ? '#fcfcfc' : '#ffffff'" style="border-bottom: 1px solid #e1e8ed; font-size: 14px; color: #4a5568;">
              <td style="padding: 15px; font-weight: bold;">#{{ c.id }}</td>
              <td style="padding: 15px; font-weight: 500; color: #2d3748;">{{ c.nome }}</td>
              <td style="padding: 15px;">{{ c.cargaHoraria }}h</td>
              <td style="padding: 15px; color: #27ae60; font-weight: 600;">R$ {{ c.valor | number:'1.2-2' }}</td>
              <td style="padding: 15px;">{{ c.dataInicio | date:'dd/MM/yyyy' }}</td>
              <td style="padding: 15px; text-align: center;">
                <span [style.background-color]="c.online ? '#ebf8ff' : '#fef3c7'"
                      [style.color]="c.online ? '#2b6cb0' : '#d97706'"
                      style="padding: 4px 10px; border-radius: 12px; font-size: 12px; font-weight: 600;">
                  {{ c.online ? 'Online' : 'Presencial' }}
                </span>
              </td>
              <td style="padding: 15px; text-align: center;">
                <span [style.background-color]="c.ativo ? '#def7ec' : '#fde8e8'"
                      [style.color]="c.ativo ? '#03543f' : '#9b1c1c'"
                      style="padding: 4px 10px; border-radius: 12px; font-size: 12px; font-weight: 600;">
                  {{ c.ativo ? 'Ativo' : 'Inativo' }}
                </span>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  `
})
export class CursosComponent implements OnInit {
  cursos = signal<Curso[]>([]);

  constructor(private cursoService: CursoService) {}

  ngOnInit(): void {
    this.carregarCursos();
  }

  carregarCursos(): void {
    this.cursoService.listar().subscribe({
      next: (dados: Curso[]) => {
        this.cursos.set(dados);
      },
      error: (err: any) => {
        console.error('Erro ao buscar cursos:', err);
      }
    });
  }
}