import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router'; // Ensure RouterModule is imported
import { EmployeeDashboardComponent } from './employee-dashboard/employee-dashboard.component';
import { EmployeeCardComponent } from './employee-card/employee-card.component';
import { SharedModule } from '../../_shared/shared.module';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
@NgModule({
  declarations: [
    AddEmployeeComponent,
    EmployeeDashboardComponent,
    EmployeeCardComponent,
  ],
  imports: [
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
  exports: [AddEmployeeComponent],
})
export class EmployeeModule {}
