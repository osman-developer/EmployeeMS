import { Component, EventEmitter, Output } from '@angular/core';
import { AddContractDTO } from '../../../DTOs/contract/AddContractDTO';

@Component({
  selector: 'app-add-contract',
  standalone: false,
  templateUrl: './add-contract.component.html',
  styleUrl: './add-contract.component.css',
})
export class AddContractComponent {
  contract = {} as AddContractDTO;

  @Output() contractAdded = new EventEmitter<AddContractDTO>();
  @Output() closeModal = new EventEmitter<void>();

  constructor() {}

  // Close the modal
  close() {
    this.closeModal.emit();
  }

  onSubmit() {
    this.contractAdded.emit(this.contract);
    this.close(); // Close the dialog after submission
  }
}
