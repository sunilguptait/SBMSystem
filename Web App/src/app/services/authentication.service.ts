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
export class AuthenticationService {

  constructor(private http: HttpService,
    private commonService: CommonService,
    private apiUrls: APIUrls,
  ) { }

  login(data): Observable<any> {
    return this.http.post(this.apiUrls.Urls.Login, data)
      .pipe(map(response => {
        if (!response || !response.Success) {
          return response;
        }
        if (response.Data && response.Data.Schools && response.Data.Schools.length > 0) {
          response.Data.Schools[0].isSelected = true;
        }
        this.commonService.updateCurrentUser(response.Data);
        return response;
      }));
  }

  getNewToken(): Observable<any> {
    this.commonService.swapToken();
    return this.http.post('account/getNewToken', { UserName: this.commonService.getPropValueFromCurrentUser('UserName') })
      .pipe(map(response => {
        if (!response || !response.Success) {
          return response;
        }
        this.commonService.updateToken(response.Data);
        return response;
      }));
  }

  checkIsUserNameExists(userName): Observable<any> {
    return this.http.get('account/IsUserNameExists?userName=' + userName)
      .pipe(map(response => {
        return response;
      }));
  }

  validateUserOnRegistration(loginName,loginType): Observable<any> {
    return this.http.post('account/ValidateUserOnRegistration',{LoginName:loginName,LoginType:loginType})
      .pipe(map(response => {
        return response;
      }));
  }

  IsLoggedIn() {
    if (localStorage.getItem('currentUser')) {
      return true;
    }
    return false;
  }
  logout() {
    localStorage.removeItem("currentUser");
  }
}
