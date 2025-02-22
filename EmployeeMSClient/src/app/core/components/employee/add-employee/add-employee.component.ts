import { Component, EventEmitter, Output } from '@angular/core';
import { EmployeeDTO, EmployeeFileDTO } from '../../../DTOs/employeeDTO';

@Component({
  selector: 'app-add-employee',
  standalone: false,
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css'],
})
export class AddEmployeeComponent {
  employee = {} as EmployeeDTO;
  previewUrl: string | ArrayBuffer | null = null; // For previewing the selected image
  selectedImage: File | null = null; // To store the selected image file

  @Output() employeeAdded = new EventEmitter<EmployeeDTO>();
  @Output() closeModal = new EventEmitter<void>();

  constructor() {}

  // Close the modal
  close() {
    this.closeModal.emit();
  }

  onSubmit() {
    console.log('Employee details submitted:', this.employee);
    this.employeeAdded.emit(this.employee);
    this.close(); // Close the dialog after submission
  }

  // Handle file change (for employee profile picture)
  onFileChange(event: any) {
    const files: FileList = event.target.files; // Get the selected files
    console.log('Files selected:', files); // For debugging

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

      // Create EmployeeFileDTO and assign the file
      const employeeFiles: EmployeeFileDTO[] = Array.from(files).map(
        (file: File) => ({
          filePath: file,
          employeeId: this.employee.id, // Assuming the employee ID is available
          employeeFileTypeId: 1, // Default file type ID
        })
      );

      this.employee.employeeFiles = employeeFiles; // Assign the files to the employee

      // Add the scrollable class to the dialog container if there is an image
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
