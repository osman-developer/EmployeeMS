import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {
  AddEmployeeDTO,
  AddEmployeeFileDTO,
} from '../../../DTOs/employee/AddEmployeeDTO';
import { appConstants } from '../../../_constants/app-constants';
import { PagingResponse } from '../../../models/pagination-models/paging-response.model';
import { DepartmentService } from '../../../_services/departmentAPI.service';
import { untilDestroyed } from '../../../_services/until-destroy.service';

@Component({
  selector: 'app-add-employee',
  standalone: false,
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css'],
})
export class AddEmployeeComponent implements OnInit {
  private destroy$ = untilDestroyed();
  employee = {} as AddEmployeeDTO;
  previewUrl: string | ArrayBuffer | null = null; // For previewing the selected image
  selectedImage: File | null = null; // To store the selected image file
  @Output() employeeAdded = new EventEmitter<AddEmployeeDTO>();
  @Output() closeModal = new EventEmitter<void>();
  //pagination fields
  departments: any = [];
  paginationResponse!: PagingResponse;
  pageIndex: number = 1;
  pageSize: number = appConstants.pageSize;
  searchString: string = '';
  totalCount!: number;
  totalPages: number = 0;
  isLoading: boolean = false; // Loading indicator for fetching data
  hasMore: boolean = true; // Whether there are more departments to load
  previousSelectedValue: any = null; // Track previous value to detect repeated selection of "Load More..."
  isLoadMoreVisible = true;
  selectedDepartmentId: any;

  constructor(private _departmentService: DepartmentService) {}

  ngOnInit() {
    this.loadDepartments();
  }

  // Load departments with pagination
  loadDepartments() {
    this.isLoading = true;
    const request = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      searchString: this.searchString,
    };

    // Make the API call to get departments
    this._departmentService
      .getAllPaginated(request)
      .pipe(this.destroy$())
      .subscribe({
        next: (response: PagingResponse) => {
          this.processDepartmentResponse(response);
        },
        error: (err) => {
          console.error('Error fetching departments:', err);
          this.isLoading = false;
        },
      });
  }

  // Append new departments to the list, or replace the list for the first page
  processDepartmentResponse(response: PagingResponse): void {
    this.departments =
      this.pageIndex === 1
        ? response.items
        : [...this.departments, ...response.items];

    // Update pagination info
    this.totalCount = response.totalCount;
    this.pageIndex = response.currentPage;
    this.totalPages = response.totalPages;

    // Check if there are more pages to load
    this.hasMore = this.pageIndex < this.totalPages;
    this.isLoadMoreVisible = this.hasMore;

    this.isLoading = false;
  }

  // Called when the dropdown is focused
  onDropdownFocus() {
    if (this.departments.length === 0) {
      this.loadDepartments();
    }
  }

  // Called when the department is selected or "Load More..." is clicked
  onDepartmentChange(event: any) {
    const selectedValue = event.target.value; // Get the selected value

    // If the user selects "Load More...", trigger loadDepartments
    if (selectedValue === '-1') {
      // Only load more if it's not already loading and there are more departments
      if (!this.isLoading && this.hasMore) {
        this.pageIndex++; // Increment page index to load the next set of departments
        this.loadDepartments();
      }

      // Store the current departmentId for future use
      const currentDepartmentId = this.employee.departmentId;

      // Use setTimeout to ensure the reset happens after the "Load More" action
      setTimeout(() => {
        // Reset selectedDepartmentId to trigger the dropdown reset
        this.selectedDepartmentId = null; // Reset ngModel to allow UI update

        // Don't affect the employee.departmentId as it needs to stay for DB saving
        this.employee.departmentId = currentDepartmentId;
      }, 0);
    } else {
      this.employee.departmentId = selectedValue; // Save the selected department for DB
    }
  }

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
