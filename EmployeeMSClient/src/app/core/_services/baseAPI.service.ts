import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class BaseApiService {
  constructor(
    @Inject(String) protected apiUrl: string,
    protected http: HttpClient
  ) {}

  // Centralized error handler to avoid repeating the same logic
  private handleError(error: any): Observable<never> {
    console.error('Error occurred:', error);
    return throwError(() => new Error(error));
  }

  // GET request to fetch all records
  public get(): Observable<any> {
    return this.http
      .get<any>(`${this.apiUrl}/all`)
      .pipe(catchError(this.handleError));
  }

  // GET request to fetch a record by ID
  public getById(id: any): Observable<any> {
    return this.http
      .post(`${this.apiUrl}/by-id`, id)
      .pipe(catchError(this.handleError));
  }

  // POST request with filters
  public getWithFilters(body: any): Observable<any> {
    return this.http
      .post<any>(`${this.apiUrl}/all`, body)
      .pipe(catchError(this.handleError));
  }

  // POST request for paginated data
  public getAllPaginated(body: any): Observable<any> {
    return this.http
      .post<any>(`${this.apiUrl}/all-paginated`, body)
      .pipe(catchError(this.handleError));
  }

  // POST request to save a new record
  public post(body: any): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http
      .post(`${this.apiUrl}/save`, body, { headers })
      .pipe(catchError(this.handleError));
  }

  // POST request for FormData (file upload or multipart form data)
  public postForm(body: any): Observable<any> {
    const formData = new FormData();
    Object.keys(body).forEach((key) => formData.append(key, body[key]));
    return this.http
      .post(`${this.apiUrl}/save`, formData)
      .pipe(catchError(this.handleError));
  }

  // PUT request to update an existing record
  public put(body: any): Observable<any> {
    return this.http
      .put(`${this.apiUrl}`, body)
      .pipe(catchError(this.handleError));
  }

  // PATCH request to partially update a record
  public patch(body: any): Observable<any> {
    return this.http
      .patch(`${this.apiUrl}`, body)
      .pipe(catchError(this.handleError));
  }

  // DELETE request to remove a record by ID
  public delete(id: string): Observable<any> {
    return this.http
      .delete<any>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  // POST request to delete a record with a body (custom delete logic)
  public deleteRecord(body: any): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
    });
    return this.http
      .post(`${this.apiUrl}/delete`, body, { headers })
      .pipe(catchError(this.handleError));
  }
}
