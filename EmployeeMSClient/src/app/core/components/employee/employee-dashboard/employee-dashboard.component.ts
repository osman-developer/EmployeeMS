import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../../_services/employeeAPI.service';
import { Employee } from '../../../models/employee.model';
import { PagingRequest } from '../../../models/pagination-models/pagination-request.model';
import { PagingResponse } from '../../../models/pagination-models/paging-response.model';
import { appConstants } from '../../../_constants/app-constants';

@Component({
  selector: 'app-employee-dashboard',
  standalone: false,
  templateUrl: './employee-dashboard.component.html',
  styleUrl: './employee-dashboard.component.css',
})
export class EmployeeDashboardComponent implements OnInit {
  employees: Employee[] = [];
  paginationResponse!: PagingResponse;
  //for pagination
  pageIndex: number = 1;
  totalRecords: number = 0;
  pageSize: number = appConstants.pageSize;
  totalCount!: number;
  constructor(private _employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.getEmployeesPaginated();
  }

  getEmployeesPaginated() {
    var request: PagingRequest = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      searchString: '',
    };
    this._employeeService.getAllPaginated(request).subscribe({
      next: (res) => {
        console.log(res);
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
      this.pageIndex = event;  // Update the page index to match the new page
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
