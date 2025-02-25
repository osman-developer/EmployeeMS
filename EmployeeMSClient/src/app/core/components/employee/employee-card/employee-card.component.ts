import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../../_services/employeeAPI.service';
import {
  GetEmployeeDTO,
  GetEmployeeFileDTO,
} from '../../../DTOs/GetEmployeeDTO';
import { ActivatedRoute } from '@angular/router';
import { envConstants } from '../../../_constants/environmnet-constants';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { untilDestroyed } from '../../../_services/until-destroy.service';
import { HelperFunctionsService } from '../../../_helpers/helperFunctions.service';
import { ToastrService } from 'ngx-toastr';
import {
  AddEmployeeDTO,
  AddEmployeeFileDTO,
} from '../../../DTOs/AddEmployeeDTO';

@Component({
  selector: 'app-employee-card',
  standalone: false,
  templateUrl: './employee-card.component.html',
  styleUrls: ['./employee-card.component.css'],
})
export class EmployeeCardComponent implements OnInit {
  private destroy$ = untilDestroyed();
  getEmployeeDTO!: GetEmployeeDTO;
  employeeId: number | null = null;
  isEditMode = false;
  employeeForm!: FormGroup;
  selectedFile: File | null = null;
  previewUrl: string | null = null;
  updateEmployee!: AddEmployeeDTO;

  constructor(
    private _employeeService: EmployeeService,
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private helperFunctionsService: HelperFunctionsService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.employeeId = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    if (this.employeeId) {
      this.fetchEmployeeData(this.employeeId);
    }
  }

  initForm(): void {
    this.employeeForm = new FormGroup({
      name: new FormControl('', Validators.required),
      jobTitle: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      phoneNumber: new FormControl('', [Validators.required]),
      startDate: new FormControl(null, Validators.required),
      dateOfBirth: new FormControl(null, Validators.required),
      salary: new FormControl(null, [Validators.required, Validators.min(0)]),
      endDate: new FormControl(''),
    });
  }
  fetchEmployeeData(employeeId: number): void {
    this._employeeService.getById(employeeId).subscribe({
      next: (res) => {
        this.getEmployeeDTO = res;
        this.formatEmployeeData();
        this.updateFormValues();
      },
      error: (err) => {
        console.error('Error fetching employee data:', err);
      },
    });
  }

  formatEmployeeData(): void {
    this.getEmployeeDTO.startDate = this.helperFunctionsService.formatDate(
      this.getEmployeeDTO.startDate
    );
    this.getEmployeeDTO.endDate = this.helperFunctionsService.formatDate(
      this.getEmployeeDTO.endDate
    );
    this.getEmployeeDTO.dateOfBirth = this.helperFunctionsService.formatDate(
      this.getEmployeeDTO.dateOfBirth
    );

    this.getEmployeeDTO?.employeeFiles?.forEach((item: GetEmployeeFileDTO) => {
      if (item.employeeFileTypeId === 1) {
        const photoUrl = `${
          envConstants.endpointForPhotos
        }/Photos/${item.filePath.split('/').pop()}`;
        item.filePath = this.sanitizer.bypassSecurityTrustUrl(
          photoUrl
        ) as string;
      }
    });
  }

  updateFormValues(): void {
    this.employeeForm.patchValue({
      name: this.getEmployeeDTO.name,
      jobTitle: this.getEmployeeDTO.jobTitle,
      email: this.getEmployeeDTO.email,
      phoneNumber: this.getEmployeeDTO.phoneNumber,
      startDate: this.getEmployeeDTO.startDate,
      endDate: this.getEmployeeDTO.endDate ? this.getEmployeeDTO.endDate : '',
      dateOfBirth: this.getEmployeeDTO.dateOfBirth,
      salary: this.getEmployeeDTO.salary,
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input?.files?.length) {
      this.selectedFile = input.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        this.previewUrl = reader.result as string;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }

  saveChanges() {
    if (this.employeeForm.valid) {
      const formData = this.employeeForm.value;
      // Update the employeeDTO with form values
      this.updateEmployee = {
        ...formData,
        id: this.employeeId,
      };

      // Handle file upload if a new image is selected
      if (this.selectedFile) {
        this.updateEmployee.employeeFiles = [
          {
            id: this.getEmployeeDTO?.employeeFiles?.[0]?.id || 0,
            file: this.selectedFile,
            employeeFileTypeId: 1,
          },
        ];
      }

      this.saveEmployee(this.updateEmployee);
      this.isEditMode = false; // Exit edit mode after saving
    }
  }

  saveEmployee(employee: AddEmployeeDTO) {
    this._employeeService
      .saveEmployee(employee)
      .pipe(this.destroy$())
      .subscribe({
        next: () => {
          this.toastr.success('Employee Record Updated');
          this.fetchEmployeeData(this.updateEmployee.id);
        },
        error: (err) => {
          console.error('Error saving employee data:', err);
        },
      });
  }
}
