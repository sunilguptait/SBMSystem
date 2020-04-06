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
export class SchoolService {

  constructor(private http: HttpService,
    private apiUrls: APIUrls,
  ) { }

  save(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.School.Create, data)
      .pipe(map(response => {
        return response;
      }));
  }

  getList(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.School.List, data)
      .pipe(map(response => {
        return response;
      }));
  }

  getSchoolDropdown(): Observable<any> {
    return this.http.get(this.apiUrls.Urls.School.GetDropdown)
      .pipe(map(response => {
        return response;
      }));
  }
  
  delete(id): Observable<any> {
    return this.http.get(this.apiUrls.Urls.School.Delete + "?id=" + id)
      .pipe(map(response => {
        return response;
      }));
  }

}
