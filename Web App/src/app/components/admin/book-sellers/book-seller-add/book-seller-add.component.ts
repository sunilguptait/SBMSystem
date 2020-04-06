import { Component, OnInit } from '@angular/core';
import { CommonService, SessionService, AuthenticationService } from '../../../../services';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BookSellerService } from '../../../../services/book-seller.service';
import { CommonAPIsService } from '../../../../services/common-apis.service';
import { SessionKeys } from '../../../../common/session-keys';
import { ConstantPool } from '@angular/compiler';
import { BroadCasterService } from '../../../../services/broad-caster.service';

@Component({
  selector: 'app-book-seller-add',
  templateUrl: './book-seller-add.component.html',
  styleUrls: ['./book-seller-add.component.scss']
})
export class BookSellerAddComponent implements OnInit {
  bookSellerForm: FormGroup;
  submitted = false;
  loading = false;
  hide = false;
  statesList: any = [];
  citiesList: any = [];
  bookSellerId: number = 0;
  bookSellerDetails: any = {};
  pageTitle = "Add new seller";
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private sessionKeys: SessionKeys,
    private commonService: CommonService,
    private sessionService: SessionService,
    private activeRoute: ActivatedRoute,
    private bookSellerService: BookSellerService,
    private commonAPIsService: CommonAPIsService,
    private authenticationService: AuthenticationService,
    private broadcasterService: BroadCasterService,
  ) { }

  ngOnInit() {
    this.initializeForm();
    this.getStates();
    this.activeRoute.params.subscribe(params => {
      if (params['id']) {
        this.bookSellerId = params['id'];
        this.getBookSellerDetails();
        this.broadcasterService.broadcast("PageTitle", "Edit Book Seller");
      }
      else {
        this.broadcasterService.broadcast("PageTitle", "Add Book Seller");
      }
    })
  }

  initializeForm() {
    this.bookSellerForm = this.formBuilder.group({
      Id: [0],
      FirmName: ['', Validators.required],
      RegistrationNo: ['', Validators.required],
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required],
      MobileNo: ['', Validators.required],
      EmailId: ['', [Validators.required, Validators.email]],
      Address1: ['', Validators.required],
      Address2: [''],
      CityId: ['', Validators.required],
      StateId: ['', Validators.required],
      PostCode: ['', Validators.required],
    })
  }

  getBookSellerDetails() {
    this.bookSellerDetails = this.sessionService.getSessionAsJSON(this.sessionKeys.Keys.BookSeller.Details);
    if (this.bookSellerDetails) {
      for (var prop in this.bookSellerDetails) {
        if (Object.prototype.hasOwnProperty.call(this.bookSellerDetails, prop)) {
          const formControl = this.bookSellerForm.get(prop);
          if (formControl) {
            formControl.setValue(this.bookSellerDetails[prop]);
          }
        }
      }
      this.getCities();
    }
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.bookSellerForm.controls[controlName].hasError(errorName);
  }

  onSubmit(formData) {
    this.submitted = true;
    if (this.bookSellerForm.invalid) {
      return;
    }
    this.commonService.showSpinner();
    this.bookSellerService.save(formData)
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.commonService.showSuccessMessage(response.Data);
          this.router.navigate(["/admin/book-sellers"]);
        })
  }

  getStates() {
    this.commonAPIsService.getStates()
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.statesList = response.Data;
        })
  }

  getCities() {
    let stateId = this.bookSellerForm.controls['StateId'].value;
    this.commonAPIsService.getCities(stateId)
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.citiesList = response.Data;

        })
  }
  checkEmailIdExists() {
    if (this.bookSellerDetails.Id > 0) {
      return;
    }

    let emailAddress = this.bookSellerForm.controls['EmailId'].value;
    if (emailAddress) {
      this.validateUserOnRegistration(emailAddress, 2);
    }
  }
  checkMobileNumberExists() {
    if (this.bookSellerDetails.Id > 0) {
      return;
    }

    let mobileNo = this.bookSellerForm.controls['MobileNo'].value;
    if (mobileNo) {
      this.validateUserOnRegistration(mobileNo, 1);
    }
  }
  validateUserOnRegistration(loginName, loginType) {
    if (loginName) {
      this.authenticationService.validateUserOnRegistration(loginName, loginType)
        .subscribe(
          response => {
            if (!this.commonService.validateAPIResponse(response)) {
              return; // show error message and return in case of any error from API
            }
            if (response.Data.ReturnValue != 0) {
              if (loginType == 1) {
                this.bookSellerForm.controls['MobileNo'].setValue('');
                this.commonService.showErrorMessage("This mobile no already exists. Please use another.")
              }
              else {
                this.bookSellerForm.controls['EmailId'].setValue('');
                this.commonService.showErrorMessage("This email address already exists. Please use another.")
              }
            }

          })
    }
  }

  onCancel() {
    this.router.navigate(["/admin/book-sellers"]);
  }

  onBack() {
    this.router.navigate(["/admin/book-sellers"]);
  }
}
