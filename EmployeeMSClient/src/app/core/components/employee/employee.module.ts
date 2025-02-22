// src/app/core/components/employee/employee.module.ts
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router'; // Ensure RouterModule is imported
import { ReactiveFormsModule } from '@angular/forms';
import { EmployeeDashboardComponent } from './employee-dashboard/employee-dashboard.component';
import { EmployeeCardComponent } from './employee-card/employee-card.component';
import { SharedModule } from '../../_shared/shared.module';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { MatIconModule } from '@angular/material/icon'; // Import the MatIconModule
@NgModule({
  declarations: [
    AddEmployeeComponent,
    EmployeeDashboardComponent,
    EmployeeCardComponent,
  ],
  imports: [
    SharedModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    MatFormFieldModule,
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
  exports: [AddEmployeeComponent],
})
export class EmployeeModule {}
