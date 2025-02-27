export interface AddDepartmentDTO {
  id?: number | null;
  name: string;
  location: string;
  employeesNumber: number;
  managerId?: number | null;
}
