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
export class BookService {

  constructor(private http: HttpService,
    private apiUrls: APIUrls,
  ) { }

  save(data: FormData): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Book.Create, data)
      .pipe(map(response => {
        return response;
      }));
  }

  getList(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Book.List, data)
      .pipe(map(response => {
        return response;
      }));
  }

  getBookDropdown(): Observable<any> {
    return this.http.get(this.apiUrls.Urls.Book.GetBookDropdown)
      .pipe(map(response => {
        return response;
      }));
  }

  createBookClassMapping(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Book.CreateBookClassMapping, data)
      .pipe(map(response => {
        return response;
      }));
  }

  getBookClassMappingList(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Book.GetBookClassMappingList, data)
      .pipe(map(response => {
        return response;
      }));
  }

  deleteBookClassMapping(mappingId): Observable<any> {
    return this.http.get(this.apiUrls.Urls.Book.DeleteBookClassMapping + "?mappingId=" + mappingId)
      .pipe(map(response => {
        return response;
      }));
  }

  getClassBooksForStudent(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Book.GetClassBooksForStudent, data)
      .pipe(map(response => {
        return response;
      }));
  }
}
