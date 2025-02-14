import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class BaseApiService {
  constructor(
    @Inject(String) protected apiUrl: string,
    protected http: HttpClient
  ) {}

  public get(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/all`);
  }

  public getById(id: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/by-id`, id);
  }

  public getWithFilters(body: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/all`, body);
  }

  public getAllPaginated(body: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/all-paginated`, body);
  }

  public post(body: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/save`, body);
  }

  public put(body: any): Observable<any> {
    return this.http.put(`${this.apiUrl}`, body);
  }

  public patch(body: any): Observable<any> {
    return this.http.patch(`${this.apiUrl}`, body);
  }

  public delete(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }

  public deleteRecord(body: any): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
    });
    return this.http.post(`${this.apiUrl}/delete`, body, { headers });
  }
}
