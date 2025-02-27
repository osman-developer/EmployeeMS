import { Component } from '@angular/core';
import { DepartmentService } from '../../../_services/departmentAPI.service';
import { ConfirmationDialogService } from '../../../_helpers/confirmation-dialog.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { untilDestroyed } from '../../../_services/until-destroy.service';
import { AddDepartmentDTO } from '../../../DTOs/department/AddDepartmentDTO';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GetDepartmentDTO } from '../../../DTOs/department/GetDepartmentDTO';

@Component({
  selector: 'app-department-card',
  standalone: false,
  templateUrl: './department-card.component.html',
  styleUrl: './department-card.component.css',
})
export class DepartmentCardComponent {
  private destroy$ = untilDestroyed();
  getDepartmentDTO!: GetDepartmentDTO;
  departmentId!: number;
  isEditMode = false;
  departmentForm!: FormGroup;
  updateDepartment!: AddDepartmentDTO;

  constructor(
    private _departmentService: DepartmentService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private confirmationDialogService: ConfirmationDialogService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.departmentId = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    if (this.departmentId) {
      this.fetchDepartmentData(this.departmentId);
    }
  }

  initForm(): void {
    this.departmentForm = new FormGroup({
      name: new FormControl('', Validators.required),
      location: new FormControl('', Validators.required),
      employeesNumber: new FormControl(null, [
        Validators.required,
        Validators.min(1),
      ]),
      managerId: new FormControl(null),
    });
  }

  fetchDepartmentData(departmentId: number): void {
    this._departmentService.getById(departmentId).subscribe({
      next: (res) => {
        this.getDepartmentDTO = res;
        this.updateFormValues();
      },
      error: (err) => {
        console.error('Error fetching department data:', err);
      },
    });
  }

  updateFormValues(): void {
    this.departmentForm.patchValue({
      name: this.getDepartmentDTO.name,
      location: this.getDepartmentDTO.location,
      employeesNumber: this.getDepartmentDTO.employeesNumber,
      managerId: this.getDepartmentDTO.managerId,
    });
  }

  saveChanges() {
    if (this.departmentForm.valid) {
      this.confirmationDialogService
        .openDialog(
          'Update Department Record',
          'Are you sure you want to update this record? This action cannot be undone.'
        )
        .then((confirmed) => {
          if (confirmed) {
            const formData = this.departmentForm.value;
            // Update the departmentDTO with form values
            this.updateDepartment = {
              ...formData,
              id: this.departmentId,
            };

            this.saveDepartment(this.updateDepartment);
            this.isEditMode = false; // Exit edit mode after saving
          }
        });
    }
  }

  saveDepartment(department: AddDepartmentDTO) {
    this._departmentService
      .post(department)
      .pipe(this.destroy$())
      .subscribe({
        next: () => {
          this.toastr.success('Department Record Updated');
          this.fetchDepartmentData(this.departmentId);
        },
        error: (err) => {
          console.error('Error saving department data:', err);
        },
      });
  }
}
