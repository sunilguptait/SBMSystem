import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService, CommonService, SessionService } from '../../services'
import { UserType } from '../../common/common-enums';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  body: HTMLBodyElement = document.getElementsByTagName('body')[0];
  html= document.getElementsByTagName('html')[0];
  loginForm: FormGroup;
  submitted = false;
  loading = false;
  returnUrl: String;
  rememberMe = false;
  hide = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private commonService: CommonService,
    private sessionService: SessionService
  ) { }
  

  ngOnInit() {
    this.body.classList.add('bg-login');
    this.html.classList.add('bg-login');

    this.RedirectUserIfLoggedIn();
    this.loginForm = this.formBuilder.group({
      username: [localStorage.getItem('loginUserName'), Validators.required],
      password: [localStorage.getItem('loginPassword'), Validators.required]
    })
  }
  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }
  public hasError = (controlName: string, errorName: string) => {
    return this.loginForm.controls[controlName].hasError(errorName);
  }
  
  onSubmit(formData) {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    this.commonService.showSpinner();
    this.authenticationService.login(formData)
      .subscribe(
        response => {
          debugger;
          if (this.commonService.validateAPIResponse(response) == true) {
            this.ManageRememberMe()

            if (this.returnUrl) {
              this.router.navigate([this.returnUrl]);
            }
            else {
              if (response.Data.UserType == UserType.Admin) {
                this.router.navigate(["admin"]);
              }
              else if (response.Data.UserType == UserType.BookSeller) {
                this.router.navigate(["book-seller"]);
              }
              else {
                this.router.navigate(["book-seller"]);
              }
            }
          }
        }
      )
  }
  RedirectUserIfLoggedIn() {
    // if (this.authenticationService.IsLoggedIn()) {
    //   this.router.navigate(["/admin/dashboard"]);
    // }
    // else {
    //   this.sessionService.clearSession();
    // }
  }
  rememberMeChanged() {
    this.rememberMe = !this.rememberMe;
  }
  ManageRememberMe() {
    localStorage.removeItem('loginUserName');
    localStorage.removeItem('loginPassword');
    if (this.rememberMe) {
      localStorage.setItem('loginUserName', this.loginForm.controls['username'].value);
      localStorage.setItem('loginPassword', this.loginForm.controls['password'].value);
    }
  }
  ngOnDestroy() {
    // remove the the body classes
    this.body.classList.remove('bg-login');
  }


}
