import { Injectable } from '@angular/core';

import { NotifierService } from 'angular-notifier';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
import { Subject, Observable } from 'rxjs';
import { DatePipe } from '@angular/common';
import { CommonMethods } from '../common/common-methods';
import { DomSanitizer } from '@angular/platform-browser';
import { HttpService } from '.';
// import { TranslateService } from '@ngx-translate/core';


@Injectable({
  providedIn: 'root'
})
export class CommonService {
  private subject = new Subject<any>();
  private culture: any;
  private termsDetails: any;
  constructor(
    private datePipe: DatePipe,
    private notifier: NotifierService,
    private spinnerService: Ng4LoadingSpinnerService,
    private sanitizer: DomSanitizer,
    // private translate: TranslateService
  ) {

  }

  getCulture() {
    return this.culture ? this.culture.name : '';
  }

  setCulture(culture) {
    this.culture = culture;
  }

  validateAPIResponse(response) {
    this.hideSpinner();
    if (!response) {
      return false;
    }
    else if (response.Success == false) {
      if (!response.ErrorMessage) {
        response.ErrorMessage = "Something went worng . Try again."
      }
      this.showErrorMessage(response.ErrorMessage)
      return false;
    }
    else if (response.Success == true) {
      return true;
    }
    return false;
  }
  showSuccessMessage(message: string) {
    this.notifier.notify("success", message);
  }
  showErrorMessage(message: string) {
    this.notifier.notify("error", message);
  }
  showWarningMessage(message: string) {
    this.notifier.notify("warning", message);
  }
  showSpinner() {
    this.spinnerService.show();
  }
  hideSpinner() {
    this.spinnerService.hide();
  }
  getCurrentUser() {
    var user = localStorage.getItem('currentUser');
    return JSON.parse(user);
  }
  getCurrentUserId() {
    var user = this.getCurrentUser();
    if (user)
      return user.id;

    return 0;
  }
  getPropValueFromCurrentUser(property) {
    var user = this.getCurrentUser();
    if (user)
      return user[property];

    return null;
  }
  updateCurrentUser(currentUser: any) {
    localStorage.setItem('currentUser', JSON.stringify(currentUser));
  }
  updateToken(data: any) {
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    currentUser.AccessToken = data.AccessToken;
    currentUser.RefreshToken = data.RefreshToken;
    localStorage.setItem('currentUser', JSON.stringify(currentUser));
  }
  swapToken() {
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let aToken = currentUser.AccessToken;
    let rToken = currentUser.RefreshToken;
    currentUser.AccessToken = rToken;
    currentUser.RefreshToken = aToken;
    localStorage.setItem('currentUser', JSON.stringify(currentUser));
  }
  getUserUpdatedDetails(): Observable<any> {
    return this.subject.asObservable();
  }
  transformDateFormat(date, format = "") {
    if (!format) {
      format = "dd/MM/yyyy";
    }
    if (isNaN(Date.parse(date))) {
      return date;
    }
    return this.datePipe.transform(date, format);
  }
  transformDateFormatFromDDMMYYYY(date, format = "") {
    if (!format) {
      format = "dd/MM/yyyy";
    }
    // console.log(date);
    var dateParts = date.split("/");
    var newDate = new Date(`${dateParts[1]}/${dateParts[0]}/${dateParts[2]}`);
    return this.datePipe.transform(newDate, format);
  }
  getHTMLString(html) {
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }
  addDayInDate(date = new Date(), days = 0) {
    return new Date(date.setDate(date.getDate() + days))
  }
  openPopupWindow(url) {
    var popUp = window.open(url, '_blank');
    if (popUp == null || typeof (popUp) == 'undefined') {
      this.showErrorMessage(CommonMethods.getLocalizedString("CMN_PopupBlocker_Warning"));
    }
  }

  downloadBlobFile(blobResponse, fileType: "PDF" | "Excel", fileName: string = "") {
    if (blobResponse.type == 'application/json') {
      let fr = new FileReader();
      let $this = this;
      fr.onload = function (e: any) {
        if (e.target.readyState === 2) {
          console.log(e.target.result);
          // if (e.target.result.indexOf('login') > -1) {
          //   // $this.authenticationService.logout();
          //   // location.reload(true);
          // }
        }
      };
      fr.readAsText(blobResponse);
    }
    else {
      let blob = new Blob([blobResponse], { type: "application/pdf" });
      let fileURL = window.URL.createObjectURL(blob);
      if (fileType == "PDF") {
        window.open(fileURL);
      }
      else {
        const a: HTMLAnchorElement = document.createElement('a') as HTMLAnchorElement;
        a.href = fileURL;
        a.download = fileName;
        document.body.appendChild(a);
        a.click();
      }
    }
  }

  getSelectedSchool(): any {
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let schools = currentUser.Schools ? currentUser.Schools.filter(m => m.isSelected == true) : [];
    return schools.length > 0 ? schools[0] : { Id: 0, Name: '' };
  }
  getBookSellerSchools() {
    let currentUser = this.getCurrentUser();
    return currentUser.Schools;
  }
}
