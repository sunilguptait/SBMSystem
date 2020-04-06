import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, retryWhen } from 'rxjs/operators';

import { Observable } from 'rxjs';
import { CommonService } from './common.service';
import { HttpService } from './http.service';
import { SessionService } from '.';
import { APIUrls } from '../common/api-urls';

@Injectable({
  providedIn: 'root'
})
export class CommonAPIsService {

  constructor(private http: HttpService,
    private commonService: CommonService,
    private apiUrls: APIUrls,
  ) { }


  getStates(): Observable<any> {
    return this.http.get(this.apiUrls.Urls.Common.GetStates)
      .pipe(map(response => {
        return response;
      }));
  }

  getCities(stateId: number): Observable<any> {
    return this.http.get(this.apiUrls.Urls.Common.GetCities + "?stateId=" + stateId)
      .pipe(map(response => {
        return response;
      }));
  }

  getBookTypes(): Observable<any> {
    return this.http.get(this.apiUrls.Urls.Common.GetBookTypes)
      .pipe(map(response => {
        return response;
      }));
  }
}
