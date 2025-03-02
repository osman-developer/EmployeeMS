export interface AddContractDTO {
  id?: number;
  employeeId: number;
  startDate: string;
  endDate: string;
  contractTypeId: number;
  salary: number;
  position: string;
  contractStatusId: number;
  signingDate: string;
  contractTerms: string;
}
