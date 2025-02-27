export interface AddDepartmentDTO {
  id?: number;
  name: string;
  location: string;
  employeesNumber: number;
  managerId?: number | null;
}
