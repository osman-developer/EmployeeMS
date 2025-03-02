import { NgModule } from '@angular/core';
import { ContractDashboardComponent } from './contract-dashboard/contract-dashboard.component';
import { SharedModule } from '../../_shared/shared.module';
import { RouterModule } from '@angular/router';
import { ContractCardComponent } from './contract-card/contract-card.component';

@NgModule({
  declarations: [ContractDashboardComponent],
  imports: [
    SharedModule,
    RouterModule.forChild([
      // Define child routes for lazy-loaded module
      {
        path: '', // Default route when accessing '/contracts'
        component: ContractDashboardComponent,
      },
      {
        path: ':id', // Route for individual contract details
        component: ContractCardComponent,
      },
    ]),
  ],
})
export class ContractModule {}
