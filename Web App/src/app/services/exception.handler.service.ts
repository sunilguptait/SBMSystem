
import { Injectable, ErrorHandler, Injector } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonService } from '../services/common.service';

@Injectable()
export class GlobalErrorHandlerService implements ErrorHandler {
    constructor(private injector: Injector) { }
    handleError(error: any) {
        let router = this.injector.get(Router);
        let commonService = this.injector.get(CommonService);
        console.log('URL: ' + router.url);
        console.log(error);
        if (error instanceof HttpErrorResponse) {
            //Backend returns unsuccessful response codes such as 404, 500 etc.				  
            // console.error('Backend returned status code: ', error.status);
            // console.error('Response body:', error.message);
            //alert('Response body:'+error.message)
            commonService.showErrorMessage((error.message || error.error));

        } else {
            //A client-side or network error occurred.	          
            //console.error('An error occurred:', (error.message || error.error));
            //alert('An error occurred:'+ (error.message||error.error));
            commonService.showErrorMessage((error.message || error.error));
        }
        setTimeout(() => {
            commonService.hideSpinner();
            if ((<HTMLElement>document.querySelector('.spinner')))
                (<HTMLElement>document.querySelector('.spinner')).classList.add("hidden");
        }, 500);
    }
} 