export interface GetEmployeeDTO {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  dateOfBirth: string;
  hireDate: string;
  jobTitle: string;
  departmentId: number;
  employeeFiles?: GetEmployeeFileDTO[];
  profilePicture?: File | null;
}

export interface GetEmployeeFileDTO {
  id: number;
  filePath: any;
  employeeId: number;
  employeeFileTypeId: number;
}
