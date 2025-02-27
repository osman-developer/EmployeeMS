import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DepartmentService } from '../../../_services/departmentAPI.service';
import { GetDepartmentDTO } from '../../../DTOs/department/GetDepartmentDTO';
import { untilDestroyed } from '../../../_services/until-destroy.service';
import { appConstants } from '../../../_constants/app-constants';
import { PagingResponse } from '../../../models/pagination-models/paging-response.model';
import { PagingRequest } from '../../../models/pagination-models/pagination-request.model';
import { MatDialog } from '@angular/material/dialog';
import { AddDepartmentComponent } from '../add-department/add-department.component';
import { AddDepartmentDTO } from '../../../DTOs/department/AddDepartmentDTO';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-department-dashboard',
  standalone: false,
  templateUrl: './department-dashboard.component.html',
  styleUrl: './department-dashboard.component.css',
})
export class DepartmentDashboardComponent implements OnInit {
  private destroy$ = untilDestroyed();
  showModal = false;
  //for pagination
  pageIndex: number = 1;
  pageSize: number = appConstants.pageSize;
  totalCount!: number;
  paginationResponse!: PagingResponse;
  searchString: string = '';

  constructor(
    private _departmentService: DepartmentService,
    public dialog: MatDialog,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getDepartmentsPaginated();
  }

  getDepartmentsPaginated() {
    var request: PagingRequest = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      searchString: this.searchString,
    };
    this._departmentService
      .getAllPaginated(request)
      .pipe(this.destroy$())
      .subscribe({
        next: (res) => {
          this.paginationResponse = res;
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
      this.getDepartmentsPaginated();
    }
  }

  openAddDepartmentModal() {
    const dialogRef = this.dialog.open(AddDepartmentComponent, {
      width: '600px', // Adjust the size as needed
    });

    // Handle department added event when the dialog is closed
    dialogRef.componentInstance.departmentAdded.subscribe(
      (department: AddDepartmentDTO) => {
        this.saveDepartment(department);
      }
    );

    dialogRef.componentInstance.closeModal.subscribe(() => {
      dialogRef.close(); // Close the dialog when requested
    });
  }

  // Handle the event when an department is added
  saveDepartment(department: AddDepartmentDTO) {
    this._departmentService
      .post(department)
      .pipe(this.destroy$())
      .subscribe({
        next: () => {
          this.toastr.success('Department Record Added.');
          this.getDepartmentsPaginated();
        },
        error: (err) => {
          console.error('Error saving department data:', err);
        },
      });
  }

  onSearch() {
    this.getDepartmentsPaginated();
  }
  onReset() {
    this.searchString = '';
    this.getDepartmentsPaginated();
  }
}
