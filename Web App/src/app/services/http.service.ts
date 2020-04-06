import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, flatMap, switchMap } from 'rxjs/operators';
import 'rxjs';
import { throwError as _throw } from 'rxjs';
import { ResponseContentType } from '@angular/http';


@Injectable({ providedIn: 'root' })
export class HttpService {
  httpParams: any;
  appSettings: any;
  constructor(private http: HttpClient) {
    this.httpParams = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
  }

  get(url: string, isBlob = false): Observable<any> {
    let options = {};
    if (isBlob) {
      options = {
        responseType: 'blob' as 'json'
      }
    }
    return this.getConfig()
      .pipe(flatMap(appSettings => {
        this.appSettings = appSettings;
        return this.http.get(appSettings.apiUrl + url, options)
          .pipe(switchMap((resp) => {
            this.updateAccessToken(resp);
            return of(resp)
          }));
      }))
  }

  post(url: string, jsonBody: any, isBlob = false, hasFile = false): Observable<any> {
    let reqBody = JSON.stringify(jsonBody);
    return this.getConfig()
      .pipe(flatMap(appSettings => {
        let params = { ...this.httpParams };
        params.observe = 'response';
        if (isBlob) {
          params.responseType = 'blob' as 'json'
          appSettings.apiUrl = appSettings.apiUrl.replace('api/', '')
        }
        if (hasFile) {
          params = new HttpHeaders({ "Content-Type": "multipart/form-data" })
        }
        this.appSettings = appSettings;
        return this.http.post(appSettings.apiUrl + url, reqBody, params)
          .pipe(switchMap((resp: any) => {
            this.updateAccessToken(resp);
            return of(resp.body)
          }));
      }))
  }

  postWithFile(url: string, data: FormData): Observable<any> {
    return this.getConfig()
      .pipe(flatMap(appSettings => {
        let params = { ...this.httpParams };
        params={
          headers: new HttpHeaders({ 'Content-Type': 'application/json' })
        };
        params.observe = 'response';

        //params.headers.append('Content-Type', 'multipart/form-data');
        this.appSettings = appSettings;
        return this.http.post(appSettings.apiUrl + url, data, params)
          .pipe(switchMap((resp: any) => {
            this.updateAccessToken(resp);
            return of(resp.body)
          }));
      }))
  }

  updateAccessToken(resp) {
    // const accessToken = resp.headers.get('x-newaccesstoken');
    // this.broadcaster.broadcast('NewAccessToken', accessToken);
  }

  getConfig(): Observable<any> {
    if (this.appSettings && this.appSettings.apiUrl) {
      return of(this.appSettings);
    }
    else {
      return this.http.get("assets/config.json", this.httpParams)//.catch(this.handleError);
    }

  }
}