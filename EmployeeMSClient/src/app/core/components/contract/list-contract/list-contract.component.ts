import { Component, Input } from '@angular/core';
import { PagingResponse } from '../../../models/pagination-models/paging-response.model';

@Component({
  selector: 'app-list-contract',
  standalone: false,
  templateUrl: './list-contract.component.html',
  styleUrl: './list-contract.component.css',
})
export class ListContractComponent {
  @Input() paginationResponse!: PagingResponse;
}
