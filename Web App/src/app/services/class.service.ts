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
export class ClassService {

  constructor(private http: HttpService,
    private apiUrls: APIUrls,
  ) { }

  save(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Class.Create, data)
      .pipe(map(response => {
        return response;
      }));
  }

  getList(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Class.List, data)
      .pipe(map(response => {
        return response;
      }));
  }

  getClassDropdown(): Observable<any> {
    return this.http.get(this.apiUrls.Urls.Class.GetClassDropdown)
      .pipe(map(response => {
        return response;
      }));
  }
}
