import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../_services/employeeAPI.service';
import { Employee } from '../../models/employee.model';
import { PagingRequest } from '../../models/pagination-models/pagination-request.model';
import { PagingResponse } from '../../models/pagination-models/paging-response.model';
import { appConstants } from '../../_constants/app-constants';

@Component({
  selector: 'app-employee-dashboard',
  imports: [],
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
      },
      error: (err) => {
        console.error('Error fetching employees:', err);
      },
    });
  }
}
