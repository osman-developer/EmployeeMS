export interface AddEmployeeDTO {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  dateOfBirth: string;
  hireDate: string;
  jobTitle: string;
  departmentId: number;
  employeeFiles: AddEmployeeFileDTO[];
}

export interface AddEmployeeFileDTO {
  id?: number | null;
  file: File;
  employeeFileTypeId: number;
}
