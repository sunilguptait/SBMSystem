import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SessionService } from '../services/session.service';

@Injectable({ providedIn: 'root' })
export class JwtInterceptor implements HttpInterceptor {
    constructor(private sessionService: SessionService) {

    }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add authorization header with jwt token if available
        let currentUser = this.sessionService.getLoggedInUser();
        if (currentUser  && currentUser.AccessToken) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.AccessToken}`,
                    //RefreshToken: currentUser.RefreshToken
                }
            });
        }
        return next.handle(request);
    }
}