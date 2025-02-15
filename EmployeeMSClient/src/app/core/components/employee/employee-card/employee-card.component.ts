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

  ngOnInit(): void {
    this.employeeId = parseInt(this.route.snapshot.paramMap.get('id')!);

    if (this.employeeId) {
      this.getEmployeeById(this.employeeId);
    }
  }

  getEmployeeById(employeeId: number) {
    this._employeeService.getById(employeeId).subscribe({
      next: (res) => {
        this.employeeDTO = res;
        this.employeeDTO?.employeeFiles?.forEach((item: EmployeeFileDTO) => {
          if (item.employeeFileTypeId === 1) {
            let photoUrl = envConstants.endpointForPhotos + '/Photos/' + item.filePath.split('/').pop();
            // Sanitize the URL and ensure it's in the correct format for binding to [src]
            let sanitizedUrl: SafeUrl = this.sanitizer.bypassSecurityTrustUrl(photoUrl);
            item.filePath = sanitizedUrl.toString();
          }
        });
      },
      error: (err) => {
        console.error('Error fetching employee:', err);
      },
    });
  }
  
}
