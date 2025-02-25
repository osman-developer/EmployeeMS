import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../../_services/employeeAPI.service';
import { PagingRequest } from '../../../models/pagination-models/pagination-request.model';
import { PagingResponse } from '../../../models/pagination-models/paging-response.model';
import { appConstants } from '../../../_constants/app-constants';
import { MatDialog } from '@angular/material/dialog';
import { AddEmployeeComponent } from '../add-employee/add-employee.component';
import { GetEmployeeDTO } from '../../../DTOs/GetEmployeeDTO';
import { untilDestroyed } from '../../../_services/until-destroy.service';
import { ToastrService } from 'ngx-toastr';
import { AddEmployeeDTO } from '../../../DTOs/AddEmployeeDTO';
@Component({
  selector: 'app-employee-dashboard',
  standalone: false,
  templateUrl: './employee-dashboard.component.html',
  styleUrl: './employee-dashboard.component.css',
})
export class EmployeeDashboardComponent implements OnInit {
  private destroy$ = untilDestroyed();
  employees: GetEmployeeDTO[] = [];
  showModal = false;
  //for pagination
  pageIndex: number = 1;
  pageSize: number = appConstants.pageSize;
  totalCount!: number;
  paginationResponse!: PagingResponse;

  constructor(
    private _employeeService: EmployeeService,
    public dialog: MatDialog,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getEmployeesPaginated();
  }

  openAddEmployeeModal() {
    const dialogRef = this.dialog.open(AddEmployeeComponent, {
      width: '600px', // Adjust the size as needed
    });

    // Handle employee added event when the dialog is closed
    dialogRef.componentInstance.employeeAdded.subscribe(
      (employee: AddEmployeeDTO) => {
        this.saveEmployee(employee);
      }
    );

    dialogRef.componentInstance.closeModal.subscribe(() => {
      dialogRef.close(); // Close the dialog when requested
    });
  }

  // Handle the event when an employee is added
  saveEmployee(employee: AddEmployeeDTO) {
    this._employeeService
      .saveEmployee(employee)
      .pipe(this.destroy$())
      .subscribe({
        next: () => {
          this.toastr.success('Employee Record Added.');
          this.getEmployeesPaginated();
        },
        error: (err) => {
          console.error('Error saving employee data:', err);
        },
      });
  }

  getEmployeesPaginated() {
    var request: PagingRequest = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      searchString: '',
    };
    this._employeeService.getAllPaginated(request).subscribe({
      next: (res) => {
        this.paginationResponse = res;
        this.employees = res.items;
        (this.paginationResponse.currentPage = res.currentPage),
          (this.paginationResponse.pageSize = res.pageSize),
          (this.totalCount = res.totalCount);
      },
      error: (err) => {
        console.error('Error fetching employees:', err);
      },
    });
  }
  onPageChanged(event: any) {
    if (this.paginationResponse.currentPage !== event) {
      this.paginationResponse.currentPage = event;
      this.pageIndex = event; // Update the page index to match the new page
      this.getEmployeesPaginated();
    }
  }
  onSearch() {
    // this.shopParams.search = this.searchTerm?.nativeElement.value;
    // this.shopParams.pageNumber = 1;
    // this.getProducts();
  }
  onReset() {
    // this.searchTerm = undefined;
    // this.shopParams = new ShopParams();
    // this.getProducts();
  }
}
