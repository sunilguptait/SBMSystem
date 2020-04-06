import { Component, OnInit } from '@angular/core';
import { CommonService, SessionService } from '../../../../services';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonAPIsService } from '../../../../services/common-apis.service';
import { SessionKeys } from '../../../../common/session-keys';
import { ConstantPool } from '@angular/compiler';
import { BookSellerService } from '../../../../services/book-seller.service';
import { SchoolService } from '../../../../services/school.service';
import { BroadCasterService } from '../../../../services/broad-caster.service';


@Component({
  selector: 'app-book-seller-school-mapping-add',
  templateUrl: './book-seller-school-mapping-add.component.html',
  styleUrls: ['./book-seller-school-mapping-add.component.scss']
})
export class BookSellerSchoolMappingAddComponent implements OnInit {

  mappingForm: FormGroup;
  submitted = false;
  loading = false;
  hide = false;
  bookSellersList: any = [];
  schoolsList: any = [];
  mappingId: number = 0;
  mappingDetails:any={};
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private sessionKeys: SessionKeys,
    private commonService: CommonService,
    private sessionService: SessionService,
    private activeRoute: ActivatedRoute,
    private bookSellerService: BookSellerService,
    private schoolService: SchoolService,
    private broadcasterService: BroadCasterService,

  ) { }

  ngOnInit() {
    this.initializeForm();
    this.getBookSellersList();
    this.getSchoolsList();
    this.broadcasterService.broadcast("PageTitle", "Add Seller School Mapping");
  }

  initializeForm() {
    this.mappingForm = this.formBuilder.group({
      SSM_Id: [0],
      SSM_BSMId: ['', Validators.required],
      SSM_SMId: ['', Validators.required],
    })
  }

  getBookSellersList() {
    this.bookSellerService.getBookSellerDropdown()
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.bookSellersList = response.Data;
        })
  }

  getSchoolsList() {
    this.schoolService.getSchoolDropdown()
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.schoolsList = response.Data;
        })
  }



  public hasError = (controlName: string, errorName: string) => {
    return this.mappingForm.controls[controlName].hasError(errorName);
  }

  onSubmit(formData) {
    this.submitted = true;
    if (this.mappingForm.invalid) {
      return;
    }
    this.commonService.showSpinner();
    this.bookSellerService.saveBookSellerSchoolMapping(formData)
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.commonService.showSuccessMessage(response.Data);
          this.router.navigate(["/admin/book-seller-mapping"]);
        })
  }

  onCancel() {
    this.router.navigate(["/admin/book-seller-mapping"]);
  }

  onBack() {
    this.router.navigate(["/admin/book-seller-mapping"]);
  }
}