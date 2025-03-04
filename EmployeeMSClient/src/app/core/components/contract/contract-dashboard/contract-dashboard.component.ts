import { Component, OnInit } from '@angular/core';
import { untilDestroyed } from '../../../_services/until-destroy.service';
import { appConstants } from '../../../_constants/app-constants';
import { PagingResponse } from '../../../models/pagination-models/paging-response.model';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { ContractService } from '../../../_services/contractAPI.service';
import { PagingRequest } from '../../../models/pagination-models/pagination-request.model';
import { AddContractComponent } from '../add-contract/add-contract.component';
import { AddContractDTO } from '../../../DTOs/contract/AddContractDTO';

@Component({
  selector: 'app-contract-dashboard',
  standalone: false,
  templateUrl: './contract-dashboard.component.html',
  styleUrl: './contract-dashboard.component.css',
})
export class ContractDashboardComponent implements OnInit {
  private destroy$ = untilDestroyed();
  showModal = false;
  //for pagination
  pageIndex: number = 1;
  pageSize: number = appConstants.pageSize;
  totalCount!: number;
  paginationResponse!: PagingResponse;
  searchString: string = '';
  toggle = true;

  constructor(
    private _contractService: ContractService,
    public dialog: MatDialog,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getContractsPaginated();
  }

  toggleChild() {
    this.toggle = !this.toggle;
  }

  getContractsPaginated() {
    var request: PagingRequest = {
      pageIndex: this.pageIndex,
      pageSize: this.pageSize,
      searchString: this.searchString,
    };
    this._contractService
      .getAllPaginated(request)
      .pipe(this.destroy$())
      .subscribe({
        next: (res) => {
          this.paginationResponse = res;
        },
        error: (err) => {
          console.error('Error fetching contracts:', err);
        },
      });
  }

  onPageChanged(event: any) {
    if (this.paginationResponse.currentPage !== event) {
      this.paginationResponse.currentPage = event;
      this.pageIndex = event; // Update the page index to match the new page
      this.getContractsPaginated();
    }
  }

  openAddContractModal() {
    const dialogRef = this.dialog.open(AddContractComponent, {
      width: '600px', // Adjust the size as needed
    });

    // Handle contract added event when the dialog is closed
    dialogRef.componentInstance.contractAdded.subscribe(
      (contract: AddContractDTO) => {
        this.saveContract(contract);
      }
    );

    dialogRef.componentInstance.closeModal.subscribe(() => {
      dialogRef.close(); // Close the dialog when requested
    });
  }

  // Handle the event when an contract is added
  saveContract(contract: AddContractDTO) {
    const message = contract.id
      ? 'Contract Record Updated.'
      : 'Contract Record Added.';
    this._contractService
      .post(contract)
      .pipe(this.destroy$())
      .subscribe({
        next: () => {
          this.toastr.success(message);
          this.getContractsPaginated();
        },
        error: (err) => {
          console.error('Error saving contract data:', err);
        },
      });
  }

  onSearch() {
    this.getContractsPaginated();
  }
  onReset() {
    this.searchString = '';
    this.getContractsPaginated();
  }
}
