import { Injectable } from '@angular/core';
import { BaseApiService } from './baseAPI.service';
import { HttpClient } from '@angular/common/http';
import { envConstants } from '../_constants/environmnet-constants';
import { Observable } from 'rxjs';
import { AddEmployeeDTO } from '../DTOs/employee/AddEmployeeDTO';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService extends BaseApiService {
  constructor(protected override http: HttpClient) {
    super(`${envConstants.endpoint}/${envConstants.employee}`, http);
  }

  saveEmployee(employee: AddEmployeeDTO): Observable<AddEmployeeDTO> {
    return this.postForm(employee, 'employeeFiles'); // Pass employeeFiles as fileKey
  }
}
