import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../../_services/employeeAPI.service';
import { EmployeeDTO, EmployeeFileDTO } from '../../../DTOs/employeeDTO';
import { ActivatedRoute } from '@angular/router';
import { envConstants } from '../../../_constants/environmnet-constants';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { untilDestroyed } from '../../../_services/until-destroy.service';
import { HelperFunctionsService } from '../../../_helpers/helperFunctions.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee-card',
  standalone: false,
  templateUrl: './employee-card.component.html',
  styleUrls: ['./employee-card.component.css'],
})
export class EmployeeCardComponent implements OnInit {
  employeeDTO!: EmployeeDTO;
  employeeId: number | null = null;
  isEditMode = false;
  employeeForm!: FormGroup;
  selectedFile: File | null = null;
  previewUrl: string | null = null;
  private destroy$ = untilDestroyed();

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
        this.employeeDTO = res;
        this.formatEmployeeData();
        this.updateFormValues();
      },
      error: (err) => {
        console.error('Error fetching employee data:', err);
      },
    });
  }

  formatEmployeeData(): void {
    this.employeeDTO.startDate = this.helperFunctionsService.formatDate(
      this.employeeDTO.startDate
    );
    this.employeeDTO.endDate = this.helperFunctionsService.formatDate(
      this.employeeDTO.endDate
    );
    this.employeeDTO.dateOfBirth = this.helperFunctionsService.formatDate(
      this.employeeDTO.dateOfBirth
    );

    this.employeeDTO?.employeeFiles?.forEach((item: EmployeeFileDTO) => {
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
      name: this.employeeDTO.name,
      jobTitle: this.employeeDTO.jobTitle,
      email: this.employeeDTO.email,
      phoneNumber: this.employeeDTO.phoneNumber,
      startDate: this.employeeDTO.startDate,
      endDate: this.employeeDTO.endDate ? this.employeeDTO.endDate : '',
      dateOfBirth: this.employeeDTO.dateOfBirth,
      salary: this.employeeDTO.salary,
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

      // If no new file is selected, keep the old image path
      // if (!this.selectedFile) {
      //   formData.picture = this.employeeDTO?.employeeFiles?.[0]?.filePath || ''; // Set to an empty string or retain the existing path
      // }

      // Update the employeeDTO with form values
      this.employeeDTO = {
        ...formData,
        id: this.employeeId,
        employeeFiles: this.employeeDTO?.employeeFiles,
      };
      // Handle file upload if a new image is selected
      if (this.selectedFile) {
        // Logic for file upload (you can call your API here)
        console.log('File to upload:', this.selectedFile);
      }

      this._employeeService
        .postForm(this.employeeDTO)
        .pipe(this.destroy$())
        .subscribe({
          next: () => {
            this.toastr.success('Employee Record Updated');
          },
          error: (err) => {
            console.error('Error saving employee data:', err);
          },
        });

      this.isEditMode = false; // Exit edit mode after saving
    }
  }
}
