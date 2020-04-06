import { Injectable } from '@angular/core'
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http'
import { Observable, throwError, BehaviorSubject } from 'rxjs'
import { catchError, switchMap, filter, take } from 'rxjs/operators';

import { AuthenticationService } from '../services/authentication.service';
import { Router } from '@angular/router';
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    private isRefreshing = false;
    private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    constructor(private authenticationService: AuthenticationService, private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): any {
        return next.handle(request).pipe(catchError(error => {
            if (error.status === 401) {
                return this.handle401Error(request, next);
            } else {
                return throwError(error);
            }
        }));
    }

    private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
        if (!this.isRefreshing) {
            this.isRefreshing = true;
            this.refreshTokenSubject.next(null);
            return this.authenticationService.getNewToken().pipe(
                switchMap((response: any) => {
                    this.isRefreshing = false;
                    this.refreshTokenSubject.next(response);
                    return this.handleToken(response, request, next);
                }));

        } else {
            return this.refreshTokenSubject.pipe(
                filter(response => response != null),
                take(1),
                switchMap(response => {
                    return this.handleToken(response, request, next);
                }));
        }
    }

    private handleToken(response: any, request: HttpRequest<any>, next: HttpHandler) {
        if (response && response.AccessToken) {
            return next.handle(this.addToken(request, response.AccessToken));
        }
        else {
            this.authenticationService.logout();
            this.router.navigate(["login"]);
            return throwError(response.Message);
        }
    }
    
    private addToken(request: HttpRequest<any>, token: string) {
        return request.clone({
            setHeaders: {
                'Authorization': `Bearer ${token}`
            }
        });
    }
}