import { NgModule } from '@angular/core';
import { DepartmentDashboardComponent } from './department-dashboard/department-dashboard.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../../_shared/shared.module';
import { DepartmentCardComponent } from './department-card/department-card.component';
import { AddDepartmentComponent } from './add-department/add-department.component';

@NgModule({
  declarations: [
    DepartmentDashboardComponent,
    DepartmentCardComponent,
    AddDepartmentComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild([
      // Define child routes for lazy-loaded module
      {
        path: '', // Default route when accessing '/departments'
        component: DepartmentDashboardComponent,
      },
      {
        path: ':id', // Route for individual department details
        component: DepartmentCardComponent,
      },
    ]),
  ],
  exports: [
    DepartmentDashboardComponent,
    DepartmentCardComponent,
    AddDepartmentComponent,
  ],
})
export class DepartmentModule {}
