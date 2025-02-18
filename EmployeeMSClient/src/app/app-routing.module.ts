// src/app/app-routing.ts
import { Route } from '@angular/router';
import { EmployeeDashboardComponent } from './core/components/employee/employee-dashboard/employee-dashboard.component';

export const routes: Route[] = [
  { path: '', component: EmployeeDashboardComponent },
  {
    path: 'employee',
    loadChildren: () =>
      import('./core/components/employee/employee.module').then(
        (m) => m.EmployeeModule
      ),
  },
  { path: '**', redirectTo: '', pathMatch: 'full' }, // Catch-all for unmatched routes
];


