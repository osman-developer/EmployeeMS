import { Injectable } from '@angular/core';
import { BaseApiService } from './baseAPI.service';
import { HttpClient } from '@angular/common/http';
import { envConstants } from '../_constants/environmnet-constants';

@Injectable({
  providedIn: 'root',
})
export class ContractService extends BaseApiService {
  constructor(protected override http: HttpClient) {
    super(`${envConstants.endpoint}/${envConstants.contract}`, http);
  }
}
