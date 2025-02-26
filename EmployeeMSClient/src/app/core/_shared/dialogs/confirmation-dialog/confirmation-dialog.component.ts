import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';  // Import NgbModalRef

@Component({
  selector: 'app-confirmation-dialog',
  standalone:false,
  templateUrl: './confirmation-dialog.component.html',
  styleUrls: ['./confirmation-dialog.component.css'],
})
export class ConfirmationDialogComponent {
  @Input() title: string = 'Confirmation';
  @Input() message: string = 'Are you sure you want to proceed?';
  @Output() confirm: EventEmitter<boolean> = new EventEmitter<boolean>();

  private modalRef!: NgbModalRef;  // Modal reference

  // Constructor is not required to hold modalRef as it will be injected by NgbModal
  constructor() {}

  // Close the modal when Confirm button is clicked
  onConfirm() {
    this.confirm.emit(true);
    this.modalRef.close();  // Close the modal after confirmation
  }

  // Close the modal when Cancel button is clicked
  onCancel() {
    this.confirm.emit(false);
    this.modalRef.close();  // Close the modal after cancel
  }

  // Called from ConfirmationDialogService to set the modal reference
  setModalRef(modalRef: NgbModalRef) {
    this.modalRef = modalRef;  // Store the modal reference
  }
}
