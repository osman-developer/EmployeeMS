import { Component } from '@angular/core';
import { untilDestroyed } from '../../../_services/until-destroy.service';
import { GetContractDTO } from '../../../DTOs/contract/GetContractDTO';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AddContractDTO } from '../../../DTOs/contract/AddContractDTO';
import { ContractService } from '../../../_services/contractAPI.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationDialogService } from '../../../_helpers/confirmation-dialog.service';

@Component({
  selector: 'app-contract-card',
  standalone: false,
  templateUrl: './contract-card.component.html',
  styleUrl: './contract-card.component.css',
})
export class ContractCardComponent {
  private destroy$ = untilDestroyed();
  getContractDTO!: GetContractDTO;
  contractId!: number;
  isEditMode = false;
  contractForm!: FormGroup;
  updateContract!: AddContractDTO;

  constructor(
    private _contractService: ContractService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private confirmationDialogService: ConfirmationDialogService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.contractId = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    if (this.contractId) {
      this.fetchContractData(this.contractId);
    }
  }

  initForm(): void {
    this.contractForm = new FormGroup({
      id: new FormControl(null),
      employeeId: new FormControl(null, Validators.required),
      name: new FormControl('', Validators.required),
      startDate: new FormControl('', Validators.required),
      endDate: new FormControl(null),
      contractTypeId: new FormControl(null, Validators.required),
      contractType: new FormControl(''),
      salary: new FormControl(null, [Validators.required, Validators.min(0)]),
      position: new FormControl('', Validators.required),
      contractStatusId: new FormControl(null, Validators.required),
      contractStatus: new FormControl(''),
      signingDate: new FormControl('', Validators.required),
      contractTerms: new FormControl('', Validators.required),
      employee: new FormGroup('', Validators.required),
    });
  }

  fetchContractData(contractId: number): void {
    this._contractService.getById(contractId).subscribe({
      next: (res) => {
        this.getContractDTO = res;
        this.updateFormValues();
      },
      error: (err) => {
        console.error('Error fetching contract data:', err);
      },
    });
  }

  updateFormValues(): void {
    this.contractForm.patchValue({
      id: this.getContractDTO.id,
      employeeId: this.getContractDTO.employeeId,
      name: this.getContractDTO.name,
      startDate: this.getContractDTO.startDate,
      endDate: this.getContractDTO.endDate,
      contractTypeId: this.getContractDTO.contractTypeId,
      contractType: this.getContractDTO.contractType,
      salary: this.getContractDTO.salary,
      position: this.getContractDTO.position,
      contractStatusId: this.getContractDTO.contractStatusId,
      contractStatus: this.getContractDTO.contractStatus,
      signingDate: this.getContractDTO.signingDate,
      contractTerms: this.getContractDTO.contractTerms,
      employee: this.getContractDTO.employee.name,
    });
  }

  prepareUpdatedContract(contract: any) {
    const updatedContract: AddContractDTO = {
      id: contract.id,
      name: contract.name,
      employeeId: contract.employeeId, // Keep only necessary properties
      startDate: contract.startDate,
      endDate: contract.endDate, // If null, set an empty string
      contractTypeId: contract.contractTypeId,
      salary: contract.salary,
      position: contract.position,
      contractStatusId: contract.contractStatusId, // Map contractStatus to ID
      signingDate: contract.signingDate,
      contractTerms: contract.contractTerms,
    };

    return updatedContract;
  }

  saveChanges() {
    if (this.contractForm.valid) {
      this.confirmationDialogService
        .openDialog(
          'Update Contract Record',
          'Are you sure you want to update this record? This action cannot be undone.'
        )
        .then((confirmed) => {
          if (confirmed) {
            const formData = this.contractForm.value;
            this.updateContract = this.prepareUpdatedContract(formData);

            this.saveContract(this.updateContract);
            this.isEditMode = false; // Exit edit mode after saving
          }
        });
    }
  }

  saveContract(contract: AddContractDTO) {
    this._contractService
      .post(contract)
      .pipe(this.destroy$())
      .subscribe({
        next: () => {
          this.toastr.success('Contract Record Updated');
          this.fetchContractData(this.contractId);
        },
        error: (err) => {
          console.error('Error saving contract data:', err);
        },
      });
  }

  generatePDF() {}
}
