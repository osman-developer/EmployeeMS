import { Component } from '@angular/core';
import { EmployeeService } from '../../../_services/employeeAPI.service';
import { EmployeeDTO, EmployeeFileDTO } from '../../../DTOs/employeeDTO';
import { ActivatedRoute } from '@angular/router';
import { envConstants } from '../../../_constants/environmnet-constants';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-employee-card',
  standalone: false,
  templateUrl: './employee-card.component.html',
  styleUrl: './employee-card.component.css',
})
export class EmployeeCardComponent {
  constructor(
    private _employeeService: EmployeeService,
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer
  ) {}

  employeeDTO!: EmployeeDTO;
  employeeId: number | null = null;
  isEditMode: boolean = false;

  ngOnInit(): void {
    this.employeeId = parseInt(this.route.snapshot.paramMap.get('id')!);

    if (this.employeeId) {
      this.getEmployeeById(this.employeeId);
    }
  }

  saveChanges() {
    // Implement your logic to save the changes (e.g., call a service to update the employee info)
    console.log('Employee information saved', this.employeeDTO);
    this.isEditMode = false; // Switch back to view mode after saving
  }
  getEmployeeById(employeeId: number) {
    this._employeeService.getById(employeeId).subscribe({
      next: (res) => {
        this.employeeDTO = res;
        this.employeeDTO?.employeeFiles?.forEach((item: EmployeeFileDTO) => {
          if (item.employeeFileTypeId === 1) {
            let photoUrl =
              envConstants.endpointForPhotos +
              '/Photos/' +
              item.filePath.split('/').pop();
            console.log('photoUrl', photoUrl); // Check the output of the URL

            // Sanitize the URL before using it
            let sanitizedUrl: SafeUrl =
              this.sanitizer.bypassSecurityTrustUrl(photoUrl);
            item.filePath = sanitizedUrl; // Store the sanitized URL in the filePath
          }
        });
      },
      error: (err) => {
        console.error('Error fetching employee:', err);
      },
    });
  }
}
