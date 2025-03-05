import { NgModule } from '@angular/core';
import { ContractDashboardComponent } from './contract-dashboard/contract-dashboard.component';
import { SharedModule } from '../../_shared/shared.module';
import { RouterModule } from '@angular/router';
import { ContractCardComponent } from './contract-card/contract-card.component';
import { ListContractComponent } from './list-contract/list-contract.component';
import { KanbanContractComponent } from './kanban-contract/kanban-contract.component';
import { DragDropModule } from '@angular/cdk/drag-drop';

@NgModule({
  declarations: [
    ContractCardComponent,
    ContractDashboardComponent,
    ListContractComponent,
    KanbanContractComponent,
  ],
  imports: [
    DragDropModule,
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
