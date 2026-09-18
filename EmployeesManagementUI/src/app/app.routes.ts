import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'employees' },
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login/login').then((m) => m.Login),
  },
  {
    path: 'register',
    loadComponent: () => import('./features/auth/register/register').then((m) => m.Register),
  },
  {
    path: 'employees',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./features/employees/employee-list/employee-list').then((m) => m.EmployeeList),
  },
  {
    path: 'employees/new',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./features/employees/employee-form/employee-form').then((m) => m.EmployeeForm),
  },
  {
    path: 'employees/:id',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./features/employees/employee-detail/employee-detail').then((m) => m.EmployeeDetail),
  },
  {
    path: 'employees/:id/edit',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./features/employees/employee-form/employee-form').then((m) => m.EmployeeForm),
  },
  { path: '**', redirectTo: 'employees' },
];
