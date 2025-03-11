import { Injectable } from '@angular/core';
import { BaseApiService } from './baseAPI.service';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { envConstants } from '../_constants/environmnet-constants';
import { catchError, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ContractService extends BaseApiService {
  constructor(protected override http: HttpClient) {
    super(`${envConstants.endpoint}/${envConstants.contract}`, http);
  }

  generateReport(body: any): Observable<HttpResponse<Blob>> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http
      .post(`${this.apiUrl}/generate-report`, body, {
        headers,
        responseType: 'blob',
        observe: 'response', // Add 'observe' to get the full response (headers + body)
      })
      .pipe(catchError(this.handleError));
  }
}
