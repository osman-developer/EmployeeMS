import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component'; // Correct relative path
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
@NgModule({
  declarations: [NavBarComponent],
  imports: [CommonModule, FormsModule, RouterModule, ToastrModule],
  providers: [DatePipe], // Add DatePipe here
  exports: [
    NavBarComponent, // Export it to be used outside
    ToastrModule,
  ],
})
export class SharedModule {}
