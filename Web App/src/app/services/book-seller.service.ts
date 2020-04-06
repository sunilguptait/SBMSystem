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
export class BookSellerService {

  constructor(private http: HttpService,
    private commonService: CommonService,
    private apiUrls: APIUrls,
  ) { }

  save(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.BookSeller.Create, data)
      .pipe(map(response => {
        return response;
      }));
  }

  getList(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.BookSeller.List, data)
      .pipe(map(response => {
        return response;
      }));
  }

  getBookSellerDropdown(): Observable<any> {
    return this.http.get(this.apiUrls.Urls.BookSeller.GetBookSellerDropdown)
      .pipe(map(response => {
        return response;
      }));
  }

  saveBookSellerSchoolMapping(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.BookSeller.CreateBookSellerSchoolMapping, data)
      .pipe(map(response => {
        return response;
      }));
  }

  getBookSellerSchoolMappingList(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.BookSeller.GetBookSellerSchoolMappingList, data)
      .pipe(map(response => {
        return response;
      }));
  }

  deleteBookSellerSchool(mappingId): Observable<any> {
    return this.http.get(this.apiUrls.Urls.BookSeller.DeleteBookSellerSchool+"?mappingId="+mappingId)
      .pipe(map(response => {
        return response;
      }));
  }
}
