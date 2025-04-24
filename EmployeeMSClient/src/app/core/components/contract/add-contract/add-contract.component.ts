import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AddContractDTO } from '../../../DTOs/contract/AddContractDTO';
import { untilDestroyed } from '../../../_services/until-destroy.service';
import { EmployeeService } from '../../../_services/employeeAPI.service';
import { appConstants } from '../../../_constants/app-constants';
import { PagingResponse } from '../../../models/pagination-models/paging-response.model';
import { EmployeeContractTypeService } from '../../../_services/employeeContractTypeAPI.service';
import { GetContractTypeDTO } from '../../../DTOs/contractType/GetContractTypeDTO';

@Component({
  selector: 'app-add-contract',
  standalone: false,
  templateUrl: './add-contract.component.html',
  styleUrl: './add-contract.component.css',
})
export class AddContractComponent implements OnInit {
  contract = {} as AddContractDTO;

  @Output() contractAdded = new EventEmitter<AddContractDTO>();
  @Output() closeModal = new EventEmitter<void>();

  private destroy$ = untilDestroyed();
  employees: any = [];
  contractTypes: GetContractTypeDTO[] = [];
  isLoading: boolean = false; // Loading indicator for fetching data
  hasMore: boolean = true; // Whether there are more employees to load
  previousSelectedValue: any = null; // Track previous value to detect repeated selection of "Load More..."
  isLoadMoreVisible = true;
  selectedEmployeeId: any;
  totalPages: number = 0;
  totalCount!: number;
  pageIndex: number = 1;
  pageSize: number = appConstants.pageSize;
  searchString: string = '';
  constructor(
    private _employeeService: EmployeeService,
    private _employeeContractTypeService: EmployeeContractTypeService
  ) {}

  ngOnInit(): void {
    this.loadContractTypes();
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

  // Load employees with pagination
  loadEmployees() {
    this.isLoading = true;
    const request = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      searchString: this.searchString,
    };

    // Make the API call to get employees
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

  // Load contract types
  loadContractTypes() {
    // Make the API call to get employees
    this._employeeContractTypeService
      .get()
      .pipe(this.destroy$())
      .subscribe({
        next: (response: GetContractTypeDTO[]) => {
          this.contractTypes = response;
          console.log('respo', response);
        },
        error: (err) => {
          console.error('Error fetching contract types:', err);
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
    // If the user selects "Load More...", trigger loademployees
    if (selectedValue === '-1') {
      // Only load more if it's not already loading and there are more employees
      if (!this.isLoading && this.hasMore) {
        this.pageIndex++; // Increment page index to load the next set of employees
        this.loadEmployees();
      }

      // Store the current contractId for future use
      const currentEmployeeId = this.contract.employeeId;

      // Use setTimeout to ensure the reset happens after the "Load More" action
      setTimeout(() => {
        // Reset selectedEmployeeId to trigger the dropdown reset
        this.selectedEmployeeId = null; // Reset ngModel to allow UI update

        // Don't affect the contract.employeeId as it needs to stay for DB saving
        this.contract.employeeId = currentEmployeeId;
      }, 0);
    } else {
      this.contract.employeeId = selectedValue; // Save the selected employee for DB
    }
  }

  // Close the modal
  close() {
    this.closeModal.emit();
  }

  onSubmit() {
    this.contractAdded.emit(this.contract);
    this.close(); // Close the dialog after submission
  }
}
