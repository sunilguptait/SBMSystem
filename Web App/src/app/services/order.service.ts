import { Injectable } from '@angular/core';
import { HttpService } from '.';
import { APIUrls } from '../common/api-urls';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpService,
    private apiUrls: APIUrls,
  ) { }

  searchStudents(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Order.SearchStudent, data)
      .pipe(map(response => {
        return response;
      }));
  }

  createOrder(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Order.CreateOrder, data)
      .pipe(map(response => {
        return response;
      }));
  }

  getOrders(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Order.List, data)
      .pipe(map(response => {
        return response;
      }));
  }

  getInvoices(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Order.Invoice, data, true)
      .pipe(map(response => {
        return response;
      }));
  }

  getOrder(orderId): Observable<any> {
    return this.http.get(this.apiUrls.Urls.Order.GetOrder + '?id=' + orderId)
      .pipe(map(response => {
        return response;
      }));
  }

  saveStudent(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Order.SaveStudent, data)
      .pipe(map(response => {
        return response;
      }));
  }

  //
  imporStudents(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Order.ImportStudents, data)
      .pipe(map(response => {
        return response;
      }));
  }
}
