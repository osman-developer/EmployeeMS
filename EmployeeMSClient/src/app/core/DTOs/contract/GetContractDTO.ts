import { GetEmployeeDTO } from '../employee/GetEmployeeDTO';

export interface GetContractDTO {
  id: number;
  employeeId: number;
  startDate: string;
  endDate: any;
  contractTypeId: number;
  contractType: any;
  salary: number;
  position: string;
  contractStatusId: number;
  contractStatus: any;
  signingDate: string;
  contractTerms: string;
  employee: GetEmployeeDTO;
}
