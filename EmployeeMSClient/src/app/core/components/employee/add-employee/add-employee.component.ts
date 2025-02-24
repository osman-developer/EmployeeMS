import { Component, EventEmitter, Output } from '@angular/core';
import {
  AddEmployeeDTO,
  AddEmployeeFileDTO,
} from '../../../DTOs/AddEmployeeDTO';

@Component({
  selector: 'app-add-employee',
  standalone: false,
  // standalone: true,
  // imports: [],
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css'],
})
export class AddEmployeeComponent {
  employee = {} as AddEmployeeDTO;
  previewUrl: string | ArrayBuffer | null = null; // For previewing the selected image
  selectedImage: File | null = null; // To store the selected image file

  @Output() employeeAdded = new EventEmitter<AddEmployeeDTO>();
  @Output() closeModal = new EventEmitter<void>();

  constructor() {}

  // Close the modal
  close() {
    this.closeModal.emit();
  }

  onSubmit() {
    this.employeeAdded.emit(this.employee);
    this.close(); // Close the dialog after submission
  }

  // Handle profile pic change (for employee profile picture)
  onProfilePicChange(event: any) {
    const files: FileList = event.target.files; // Get the selected files

    if (files && files.length > 0) {
      const selectedFile = files[0]; // Assuming only one file is selected
      const fileReader = new FileReader();

      fileReader.onload = () => {
        this.previewUrl = fileReader.result; // Store the result in previewUrl for image preview
      };

      // If the selected file is an image, generate a preview
      if (selectedFile.type.startsWith('image/')) {
        fileReader.readAsDataURL(selectedFile);
      }

      // Store the selected image file
      this.selectedImage = selectedFile;

      // Create employeeFiles array with file objects and fileTypeId metadata
      const employeeFiles: AddEmployeeFileDTO[] = Array.from(files).map(
        (file: File) => ({
          file: file, // Actual File object
          employeeFileTypeId: 1, // Default file type ID
        })
      );

      // Assign the employeeFiles array to the employee object
      this.employee.employeeFiles = employeeFiles;

      // Update the dialog scroll if necessary
      this.toggleDialogScroll();
    }
  }

  // Toggle the scrollable class on the dialog container based on whether an image is selected
  toggleDialogScroll() {
    const dialogContainer = document.querySelector('.mat-dialog-container');
    if (dialogContainer) {
      if (this.selectedImage) {
        dialogContainer.classList.add('scrollable');
      } else {
        dialogContainer.classList.remove('scrollable');
      }
    }
  }

  // Remove selected image (if user wants to remove it)
  removeImage() {
    this.previewUrl = null;
    this.selectedImage = null;
    this.toggleDialogScroll(); // Update dialog scroll class
  }
}
