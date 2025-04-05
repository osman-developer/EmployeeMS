import { Component } from '@angular/core';
import { DepartmentService } from '../../../_services/departmentAPI.service';
import { ConfirmationDialogService } from '../../../_helpers/confirmation-dialog.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { untilDestroyed } from '../../../_services/until-destroy.service';
import { AddDepartmentDTO } from '../../../DTOs/department/AddDepartmentDTO';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GetDepartmentDTO } from '../../../DTOs/department/GetDepartmentDTO';
import { appConstants } from '../../../_constants/app-constants';
import { EmployeeService } from '../../../_services/employeeAPI.service';
import { PagingResponse } from '../../../models/pagination-models/paging-response.model';

@Component({
  selector: 'app-department-card',
  standalone: false,
  templateUrl: './department-card.component.html',
  styleUrl: './department-card.component.css',
})
export class DepartmentCardComponent {
  private destroy$ = untilDestroyed();
  getDepartmentDTO!: GetDepartmentDTO;
  departmentId!: number;
  isEditMode = false;
  departmentForm!: FormGroup;
  updateDepartment!: AddDepartmentDTO;
  employees: any = [];
  isLoading: boolean = false; // Loading indicator for fetching data
  hasMore: boolean = true; // Whether there are more emplotess to load
  previousSelectedValue: any = null; // Track previous value to detect repeated selection of "Load More..."
  isLoadMoreVisible = true;
  selectedEmployeeId: any;
  totalPages: number = 0;
  totalCount!: number;
  pageIndex: number = 1;
  pageSize: number = appConstants.pageSize;
  searchString: string = '';

  constructor(
    private _departmentService: DepartmentService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private _employeeService: EmployeeService,
    private confirmationDialogService: ConfirmationDialogService
  ) {}

  ngOnInit(): void {
    this.loadEmployees();
    this.initForm();
    this.departmentId = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    if (this.departmentId) {
      this.fetchDepartmentData(this.departmentId);
    }
  }

  initForm(): void {
    this.departmentForm = new FormGroup({
      name: new FormControl('', Validators.required),
      location: new FormControl('', Validators.required),
      employeesNumber: new FormControl(null, [
        Validators.required,
        Validators.min(1),
      ]),
      selectedEmployeeId: new FormControl(null),
    });
  }

  // Append new employees to the list, or replace the list for the first page
  processEmployeeResponse(response: PagingResponse): void {
    this.employees =
      this.pageIndex === 1
        ? response.items
        : [...this.employees, ...response.items];

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
  loadEmployees() {
    this.isLoading = true;
    const request = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      searchString: this.searchString,
    };

    // Make the API call to get departments
    this._employeeService
      .getAllPaginated(request)
      .pipe(this.destroy$())
      .subscribe({
        next: (response: PagingResponse) => {
          this.processEmployeeResponse(response);
        },
        error: (err) => {
          console.error('Error fetching employees:', err);
          this.isLoading = false;
        },
      });
  }

  // Called when the dropdown is focused
  onDropdownFocus() {
    if (this.employees.length === 0) {
      this.loadEmployees();
    }
  }

  // Called when the employee is selected or "Load More..." is clicked
  onEmployeeChange(event: any) {
    const selectedValue = event.target.value; // Get the selected value
    // If the user selects "Load More...", trigger loadEmployees
    if (selectedValue === '-1') {
      // Only load more if it's not already loading and there are more employees
      if (!this.isLoading && this.hasMore) {
        this.pageIndex++; // Increment page index to load the next set of employees
        this.loadEmployees();
      }

      // Store the current departmentId for future use
      const currentEmployeeId = this.updateDepartment.managerId;

      // Use setTimeout to ensure the reset happens after the "Load More" action
      setTimeout(() => {
        // Reset selectedDepartmentId to trigger the dropdown reset
        this.selectedEmployeeId = null; // Reset ngModel to allow UI update

        // Don't affect the employee.departmentId as it needs to stay for DB saving
        this.updateDepartment.managerId = currentEmployeeId;
      }, 0);
    } else {
      this.updateDepartment.managerId = selectedValue; // Save the selected department for DB
    }
  }

  fetchDepartmentData(departmentId: number): void {
    this._departmentService.getById(departmentId).subscribe({
      next: (res) => {
        this.getDepartmentDTO = res;
        this.updateFormValues();
      },
      error: (err) => {
        console.error('Error fetching department data:', err);
      },
    });
  }

  updateFormValues(): void {
    this.selectedEmployeeId = this.getDepartmentDTO?.managerId;
    this.departmentForm.patchValue({
      name: this.getDepartmentDTO.name,
      location: this.getDepartmentDTO.location,
      employeesNumber: this.getDepartmentDTO.employeesNumber,
      selectedEmployeeId: this.getDepartmentDTO.managerId,
    });
  }

  saveChanges() {
    if (this.departmentForm.valid) {
      this.confirmationDialogService
        .openDialog(
          'Update Department Record',
          'Are you sure you want to update this record? This action cannot be undone.'
        )
        .then((confirmed) => {
          if (confirmed) {
            const formData = this.departmentForm.value;
            // Update the departmentDTO with form values
            this.updateDepartment = {
              ...formData,
              id: this.departmentId,
              managerId: this.selectedEmployeeId,
            };

            this.saveDepartment(this.updateDepartment);
            this.isEditMode = false; // Exit edit mode after saving
          }
        });
    }
  }

  saveDepartment(department: AddDepartmentDTO) {
    this._departmentService
      .post(department)
      .pipe(this.destroy$())
      .subscribe({
        next: () => {
          this.toastr.success('Department Record Updated');
          this.fetchDepartmentData(this.departmentId);
        },
        error: (err) => {
          console.error('Error saving department data:', err);
        },
      });
  }
}
