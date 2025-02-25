export interface GetEmployeeDTO {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  dateOfBirth: string;
  jobTitle: string;
  salary: number;
  startDate: string;
  endDate: string | '';
  employeeFiles?: GetEmployeeFileDTO[];
  profilePicture?: File | null;
}

export interface GetEmployeeFileDTO {
  id: number;
  filePath: any;
  employeeId: number;
  employeeFileTypeId: number;
}
