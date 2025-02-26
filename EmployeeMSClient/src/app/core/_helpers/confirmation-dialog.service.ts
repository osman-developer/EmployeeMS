import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'; // Import NgbModal for dynamic modal opening
import { ConfirmationDialogComponent } from '../_shared/dialogs/confirmation-dialog/confirmation-dialog.component'; // Import your dialog component

@Injectable({
  providedIn: 'root',
})
export class ConfirmationDialogService {
  constructor(private modalService: NgbModal) {}

  // Method to open the confirmation dialog dynamically
  openDialog(title: string, message: string): Promise<boolean> {
    const modalRef = this.modalService.open(ConfirmationDialogComponent, {
      centered: true,
      backdrop: 'static', // Disable closing on clicking outside
      keyboard: true, // Enable closing via ESC key
    });

    // Pass modalRef to the dialog component to manage closing the modal from within
    modalRef.componentInstance.setModalRef(modalRef);

    // Set the dialog's title and message
    modalRef.componentInstance.title = title;
    modalRef.componentInstance.message = message;

    // Return a promise that resolves with the user's response (true/false)
    return new Promise((resolve) => {
      modalRef.componentInstance.confirm.subscribe((response: boolean) => {
        resolve(response); // Resolve the promise with the response
      });
    });
  }
}
