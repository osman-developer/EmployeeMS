import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component'; // Correct relative path
import { RouterModule } from '@angular/router';
// import {ToastrModule} from 'ngx-toastr';

@NgModule({
  declarations: [
    NavBarComponent,
    // ToastrModule.forRoot({
    //   positionClass:'toast-bottom-right',
    //   preventDuplicates:true
    // })
  ],
  imports: [CommonModule, RouterModule],
  providers: [DatePipe], // Add DatePipe here
  exports: [
    NavBarComponent, // Export it to be used outside
    // other components...
  ],
})
export class SharedModule {}
