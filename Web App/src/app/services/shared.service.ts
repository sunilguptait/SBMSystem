import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { CommonService } from './common.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private http: HttpService, private commonService: CommonService) { }

  shipmentQuote(data): Observable<any> {
    return this.http.post('/api/Shared/ShipmentQuote', data)
      .pipe(map(response => {
        return response;
      }));
  }
}
