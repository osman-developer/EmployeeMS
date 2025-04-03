import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../../_services/employeeAPI.service';
import {
  GetEmployeeDTO,
  GetEmployeeFileDTO,
} from '../../../DTOs/employee/GetEmployeeDTO';
import { ActivatedRoute } from '@angular/router';
import { envConstants } from '../../../_constants/environmnet-constants';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { untilDestroyed } from '../../../_services/until-destroy.service';
import { HelperFunctionsService } from '../../../_helpers/helperFunctions.service';
import { ToastrService } from 'ngx-toastr';
import { AddEmployeeDTO } from '../../../DTOs/employee/AddEmployeeDTO';
import { EmployeeFileService } from '../../../_services/employeeFileAPI.service';
import { ConfirmationDialogService } from '../../../_helpers/confirmation-dialog.service';
import { PagingResponse } from '../../../models/pagination-models/paging-response.model';
import { DepartmentService } from '../../../_services/departmentAPI.service';
import { appConstants } from '../../../_constants/app-constants';

@Component({
  selector: 'app-employee-card',
  standalone: false,
  templateUrl: './employee-card.component.html',
  styleUrls: ['./employee-card.component.css'],
})
export class EmployeeCardComponent implements OnInit {
  private destroy$ = untilDestroyed();
  getEmployeeDTO!: GetEmployeeDTO;
  employeeId: number | null = null;
  isEditMode = false;
  employeeForm!: FormGroup;
  selectedFile: File | null = null;
  previewUrl: string | null = null;
  updateEmployee!: AddEmployeeDTO;
  departments: any = [];
  isLoading: boolean = false; // Loading indicator for fetching data
  hasMore: boolean = true; // Whether there are more departments to load
  previousSelectedValue: any = null; // Track previous value to detect repeated selection of "Load More..."
  isLoadMoreVisible = true;
  selectedDepartmentId: any;
  totalPages: number = 0;
  totalCount!: number;
  pageIndex: number = 1;
  pageSize: number = appConstants.pageSize;
  searchString: string = '';

  constructor(
    private _employeeService: EmployeeService,
    private _employeeFileService: EmployeeFileService,
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private helperFunctionsService: HelperFunctionsService,
    private toastr: ToastrService,
    private confirmationDialogService: ConfirmationDialogService,
    private _departmentService: DepartmentService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadDepartments();
    this.employeeId = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    if (this.employeeId) {
      this.fetchEmployeeData(this.employeeId);
    }
  }
  get profilePicture() {
    // Find the profile picture file where employeeFileTypeId is 1
    return this.getEmployeeDTO.employeeFiles?.find(
      (file) => file.employeeFileTypeId === 1
    );
  }

  initForm(): void {
    this.employeeForm = new FormGroup({
      name: new FormControl('', Validators.required),
      jobTitle: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      phoneNumber: new FormControl('', [Validators.required]),
      dateOfBirth: new FormControl(null, Validators.required),
      hireDate: new FormControl(null, Validators.required),
      selectedDepartmentId: new FormControl(null, [Validators.required]),
    });
  }

  fetchEmployeeData(employeeId: number): void {
    this._employeeService.getById(employeeId).subscribe({
      next: (res) => {
        this.getEmployeeDTO = res;
        this.updateEmployee = res;
        this.formatEmployeeData();
        this.updateFormValues();
      },
      error: (err) => {
        console.error('Error fetching employee data:', err);
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
      const currentDepartmentId = this.updateEmployee.departmentId;

      // Use setTimeout to ensure the reset happens after the "Load More" action
      setTimeout(() => {
        // Reset selectedDepartmentId to trigger the dropdown reset
        this.selectedDepartmentId = null; // Reset ngModel to allow UI update

        // Don't affect the employee.departmentId as it needs to stay for DB saving
        this.updateEmployee.departmentId = currentDepartmentId;
      }, 0);
    } else {
      this.updateEmployee.departmentId = selectedValue; // Save the selected department for DB
    }
  }

  formatEmployeeData(): void {
    this.getEmployeeDTO.dateOfBirth = this.helperFunctionsService.formatDate(
      this.getEmployeeDTO.dateOfBirth
    );
    this.getEmployeeDTO.hireDate = this.helperFunctionsService.formatDate(
      this.getEmployeeDTO.hireDate
    );

    this.getEmployeeDTO?.employeeFiles?.forEach((item: GetEmployeeFileDTO) => {
      if (item.employeeFileTypeId === 1) {
        const photoUrl = `${
          envConstants.endpointForPhotos
        }/Photos/${item.filePath.split('/').pop()}`;
        item.filePath = this.sanitizer.bypassSecurityTrustUrl(
          photoUrl
        ) as string;
      }
    });
  }

  updateFormValues(): void {
    this.employeeForm.patchValue({
      name: this.getEmployeeDTO.name,
      jobTitle: this.getEmployeeDTO.jobTitle,
      email: this.getEmployeeDTO.email,
      phoneNumber: this.getEmployeeDTO.phoneNumber,
      dateOfBirth: this.getEmployeeDTO.dateOfBirth,
      hireDate: this.getEmployeeDTO.hireDate,
      selectedDepartmentId: this.getEmployeeDTO.departmentId,
    });
    this.selectedDepartmentId = this.getEmployeeDTO.departmentId;
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input?.files?.length) {
      this.selectedFile = input.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        this.previewUrl = reader.result as string;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }

  saveChanges() {
    if (this.employeeForm.valid) {
      this.confirmationDialogService
        .openDialog(
          'Update Employee Record',
          'Are you sure you want to update this record? This action cannot be undone.'
        )
        .then((confirmed) => {
          if (confirmed) {
            const formData = this.employeeForm.value;
            // Update the employeeDTO with form values
            this.updateEmployee = {
              ...formData,
              id: this.employeeId,
              departmentId: this.selectedDepartmentId,
            };

            // Handle file upload if a new image is selected
            if (this.selectedFile) {
              this.updateEmployee.employeeFiles = [
                {
                  id: this.getEmployeeDTO?.employeeFiles?.[0]?.id || 0,
                  file: this.selectedFile,
                  employeeFileTypeId: 1,
                },
              ];
            }

            this.saveEmployee(this.updateEmployee);
            this.isEditMode = false; // Exit edit mode after saving
          }
        });
    }
  }

  saveEmployee(employee: AddEmployeeDTO) {
    this._employeeService
      .saveEmployee(employee)
      .pipe(this.destroy$())
      .subscribe({
        next: () => {
          this.toastr.success('Employee Record Updated');
          this.previewUrl = null; // Clear preview image if any
          this.fetchEmployeeData(this.updateEmployee.id);
        },
        error: (err) => {
          console.error('Error saving employee data:', err);
        },
      });
  }

  removePicture(employeeFileId?: number) {
    // Remove the employee file object with employeeFileTypeId = 1
    this.confirmationDialogService
      .openDialog(
        'Remove Profile Picture',
        'Are you sure you want to remove the profile picture? This action cannot be undone.'
      )
      .then((confirmed) => {
        if (confirmed) {
          this.getEmployeeDTO.employeeFiles =
            this.getEmployeeDTO?.employeeFiles?.filter(
              (file) => file.employeeFileTypeId !== 1
            );
          this.previewUrl = null; // Clear preview image if any
          this.deletePicture(employeeFileId);
        }
      });
  }

  //handles the APi call to delete image from db and disk
  deletePicture(employeeFileId?: number) {
    this._employeeFileService
      .deleteRecord(employeeFileId)
      .pipe(this.destroy$())
      .subscribe({
        next: () => {
          this.toastr.success('Employee Picture Deleted');
        },
        error: (err) => {
          console.error('Error saving employee data:', err);
        },
      });
  }
}
