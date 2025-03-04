import { Component, Input, ChangeDetectorRef } from '@angular/core';
import { PagingResponse } from '../../../models/pagination-models/paging-response.model';
import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { AddContractDTO } from '../../../DTOs/contract/AddContractDTO';
import { GetContractDTO } from '../../../DTOs/contract/GetContractDTO';

@Component({
  selector: 'app-kanban-contract',
  standalone: false,
  templateUrl: './kanban-contract.component.html',
  styleUrls: ['./kanban-contract.component.css'],
})
export class KanbanContractComponent {
  @Input() paginationResponse!: PagingResponse;
  @Input() onStatusChange!: (contract: AddContractDTO) => void; // Callback function to handle status change

  contractStatus = ['Active', 'Terminated', 'On Leave'];

  constructor(private cdr: ChangeDetectorRef) {}

  // Get contracts filtered by status
  getContractsByStatus(status: string): GetContractDTO[] {
    // Check if paginationResponse is available and contains items
    if (this.paginationResponse && this.paginationResponse.items) {
      return this.paginationResponse.items.filter(
        (contract) => contract.contractStatus === status
      );
    }
    return [];
  }

  // Filter statuses to avoid current status in the connected list
  getConnectedStatuses(status: string): string[] {
    return this.contractStatus.filter((s) => s !== status);
  }

  // Handle the drop action (when a contract is dropped into a new column)
  onDrop(event: CdkDragDrop<GetContractDTO[]>) {
    const movedContract = event.item.data as GetContractDTO; // Extract the dropped contract

    // Update the contract's status to the ID of the drop container (the new status)
    movedContract.contractStatus = event.container.id;
    // Map the GetContractDTO to AddContractDTO
    const updatedContract = this.prepareUpdatedContract(movedContract);

    // Trigger the status change callback function passed from the parent component
    this.onStatusChange(updatedContract);

    // Manually trigger change detection to ensure the view updates
    this.cdr.detectChanges();
  }

  prepareUpdatedContract(movedContract: GetContractDTO) {
    const updatedContract: AddContractDTO = {
      id: movedContract.id,
      employeeId: movedContract.employeeId, // Keep only necessary properties
      startDate: movedContract.startDate,
      endDate: movedContract.endDate, // If null, set an empty string
      contractTypeId: movedContract.contractTypeId,
      salary: movedContract.salary,
      position: movedContract.position,
      contractStatusId: this.getStatusId(movedContract.contractStatus), // Map contractStatus to ID
      signingDate: movedContract.signingDate,
      contractTerms: movedContract.contractTerms,
    };

    return updatedContract;
  }
  // Helper function to get the contractStatusId based on the status string
  getStatusId(status: string): number {
    switch (status) {
      case 'Active':
        return 1;
      case 'Terminated':
        return 2;
      case 'On Leave':
        return 3;
      default:
        return 0;
    }
  }
}
