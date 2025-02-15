// src/app/core/components/employee/employee.module.ts
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';  // Ensure RouterModule is imported

import { EmployeeDashboardComponent } from './employee-dashboard/employee-dashboard.component';
import { EmployeeCardComponent } from './employee-card/employee-card.component';

@NgModule({
  declarations: [EmployeeDashboardComponent, EmployeeCardComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([  // Define child routes for lazy-loaded module
      {
        path: '',  // Default route when accessing '/employee'
        component: EmployeeDashboardComponent,
      },
      {
        path: ':id',  // Route for individual employee details
        component: EmployeeCardComponent,
      },
    ]),
  ],
})
export class EmployeeModule {}
