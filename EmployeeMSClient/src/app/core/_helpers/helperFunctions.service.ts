import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class HelperFunctionsService {
  constructor() {}

  formatDate(date: any) {
    if (date) {
      const formattedDate = new Date(date).toISOString().split('T')[0];
      return formattedDate;
    }
    return '';
  }
}
