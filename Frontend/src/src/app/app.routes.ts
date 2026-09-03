import { Routes } from '@angular/router';
import { CursosComponent } from './pages/cursos.component';

export const routes: Routes = [
  { path: '', component: CursosComponent },
  { path: '**', redirectTo: '' }
];