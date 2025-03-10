import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../../_services/employeeAPI.service';
import {
  GetEmployeeDTO,
  GetEmployeeFileDTO,
} from '../../../DTOs/employee/GetEmployeeDTO';
import { ActivatedRoute } from '@angular/router';
import { envConstants } from '../../../_constants/environmnet-constants';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { untilDestroyed } from '../../../_services/until-destroy.service';
import { HelperFunctionsService } from '../../../_helpers/helperFunctions.service';
import { ToastrService } from 'ngx-toastr';
import { AddEmployeeDTO } from '../../../DTOs/employee/AddEmployeeDTO';
import { EmployeeFileService } from '../../../_services/employeeFileAPI.service';
import { ConfirmationDialogService } from '../../../_helpers/confirmation-dialog.service';

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
    private _employeeFileService: EmployeeFileService,
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private helperFunctionsService: HelperFunctionsService,
    private toastr: ToastrService,
    private confirmationDialogService: ConfirmationDialogService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.employeeId = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    if (this.employeeId) {
      this.fetchEmployeeData(this.employeeId);
    }
  }
  get profilePicture() {
    // Find the profile picture file where employeeFileTypeId is 1
    return this.getEmployeeDTO.employeeFiles?.find(
      (file) => file.employeeFileTypeId === 1
    );
  }
  initForm(): void {
    this.employeeForm = new FormGroup({
      name: new FormControl('', Validators.required),
      jobTitle: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      phoneNumber: new FormControl('', [Validators.required]),
      dateOfBirth: new FormControl(null, Validators.required),
      hireDate: new FormControl(null, Validators.required),
      departmentId: new FormControl(null, [Validators.required]),
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
    this.getEmployeeDTO.dateOfBirth = this.helperFunctionsService.formatDate(
      this.getEmployeeDTO.dateOfBirth
    );
    this.getEmployeeDTO.hireDate = this.helperFunctionsService.formatDate(
      this.getEmployeeDTO.hireDate
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
      dateOfBirth: this.getEmployeeDTO.dateOfBirth,
      hireDate: this.getEmployeeDTO.hireDate,
      departmentId: this.getEmployeeDTO.departmentId,
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
      this.confirmationDialogService
        .openDialog(
          'Update Employee Record',
          'Are you sure you want to update this record? This action cannot be undone.'
        )
        .then((confirmed) => {
          if (confirmed) {
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
        });
    }
  }

  saveEmployee(employee: AddEmployeeDTO) {
    this._employeeService
      .saveEmployee(employee)
      .pipe(this.destroy$())
      .subscribe({
        next: () => {
          this.toastr.success('Employee Record Updated');
          this.previewUrl = null; // Clear preview image if any
          this.fetchEmployeeData(this.updateEmployee.id);
        },
        error: (err) => {
          console.error('Error saving employee data:', err);
        },
      });
  }

  removePicture(employeeFileId?: number) {
    // Remove the employee file object with employeeFileTypeId = 1
    this.confirmationDialogService
      .openDialog(
        'Remove Profile Picture',
        'Are you sure you want to remove the profile picture? This action cannot be undone.'
      )
      .then((confirmed) => {
        if (confirmed) {
          this.getEmployeeDTO.employeeFiles =
            this.getEmployeeDTO?.employeeFiles?.filter(
              (file) => file.employeeFileTypeId !== 1
            );
          this.previewUrl = null; // Clear preview image if any
          this.deletePicture(employeeFileId);
        }
      });
  }

  //handles the APi call to delete image from db and disk
  deletePicture(employeeFileId?: number) {
    this._employeeFileService
      .deleteRecord(employeeFileId)
      .pipe(this.destroy$())
      .subscribe({
        next: () => {
          this.toastr.success('Employee Picture Deleted');
        },
        error: (err) => {
          console.error('Error saving employee data:', err);
        },
      });
  }
}
