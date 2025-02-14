import { Component } from '@angular/core';
import { EmployeeService } from '../../_services/employeeAPI.service';
import { EmployeeDTO } from '../../DTOs/employeeDTO';

@Component({
  selector: 'app-employee-card',
  imports: [],
  templateUrl: './employee-card.component.html',
  styleUrl: './employee-card.component.css',
})
export class EmployeeCardComponent {
  constructor(private _employeeService: EmployeeService) {}

  employeeDTO!: EmployeeDTO;

  ngOnInit(): void {
    this.getEmployeeById();
  }
  getEmployeeById() {
    this._employeeService.getById(41).subscribe({
      next: (res) => {
        this.employeeDTO = res;
        console.log(res);
        // If you have any other code related to employees or basketItemDTOs, you can continue here
        // Example for handling images (if necessary):
        // this.basket?.basketItemDTOs.forEach((basketItem: any) => {
        //     if (basketItem.productImagePath) {
        //         basketItem.productImagePath = envConstants.endpointForPhotos + "/Photos/" + basketItem.productImagePath.split("/").pop();
        //         basketItem.productImagePath = this.sanitizer.bypassSecurityTrustUrl(basketItem.productImagePath);
        //     }
        // });
      },
      error: (err) => {
        console.error('Error fetching employee:', err);
      },
    });
  }
}
