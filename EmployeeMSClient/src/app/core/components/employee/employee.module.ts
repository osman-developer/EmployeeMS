// src/app/core/components/employee/employee.module.ts
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router'; // Ensure RouterModule is imported
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EmployeeDashboardComponent } from './employee-dashboard/employee-dashboard.component';
import { EmployeeCardComponent } from './employee-card/employee-card.component';
import { SharedModule } from '../../_shared/shared.module';

@NgModule({
  declarations: [EmployeeDashboardComponent, EmployeeCardComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    RouterModule.forChild([
      // Define child routes for lazy-loaded module
      {
        path: '', // Default route when accessing '/employee'
        component: EmployeeDashboardComponent,
      },
      {
        path: ':id', // Route for individual employee details
        component: EmployeeCardComponent,
      },
    ]),
  ],
})
export class EmployeeModule {}
