import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component'; // Correct relative path
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { PagerComponent } from './pager/pager.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { ConfirmationDialogComponent } from './dialogs/confirmation-dialog/confirmation-dialog.component';
@NgModule({
  declarations: [
    NavBarComponent,
    ConfirmationDialogComponent,
    PagerComponent,
    PagingHeaderComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    ToastrModule,
    PaginationModule.forRoot(),
  ],
  providers: [DatePipe], // Add DatePipe here
  exports: [
    NavBarComponent, // Export it to be used outside
    ToastrModule,
    PagerComponent,
    PagingHeaderComponent,
    PaginationModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ConfirmationDialogComponent,
  ],
})
export class SharedModule {}
