export interface EmployeeDTO {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  dateOfBirth: string;
  jobTitle: string;
  salary: number;
  startDate: string;
  endDate: any;
  employeeFiles?: EmployeeFileDTO[];
}

export interface EmployeeFileDTO {
  filePath: any;
  employeeId: number;
  employeeFileTypeId: number;
}
