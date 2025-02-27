import { Component, EventEmitter, Output } from '@angular/core';
import { AddDepartmentDTO } from '../../../DTOs/department/AddDepartmentDTO';

@Component({
  selector: 'app-add-department',
  standalone: false,
  templateUrl: './add-department.component.html',
  styleUrl: './add-department.component.css',
})
export class AddDepartmentComponent {
  department = {} as AddDepartmentDTO;

  @Output() departmentAdded = new EventEmitter<AddDepartmentDTO>();
  @Output() closeModal = new EventEmitter<void>();

  constructor() {}

  // Close the modal
  close() {
    this.closeModal.emit();
  }

  onSubmit() {
    this.departmentAdded.emit(this.department);
    this.close(); // Close the dialog after submission
  }
}
