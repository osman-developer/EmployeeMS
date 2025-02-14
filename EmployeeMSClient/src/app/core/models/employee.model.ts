export interface Employee {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  dateOfBirth: string;
  jobTitle: string;
  salary: number;
  startDate: string;
  endDate: any;
  employeeFiles: EmployeeFile[];
}

export interface EmployeeFile {
  filePath: string;
  employeeId: number;
  employeeFileTypeId: number;
}
