import { Component, OnInit } from '@angular/core';
import { CommonService, SessionService } from '../../../../services';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonAPIsService } from '../../../../services/common-apis.service';
import { SessionKeys } from '../../../../common/session-keys';
import { ConstantPool } from '@angular/compiler';
import { SchoolService } from '../../../../services/school.service';
import { BroadCasterService } from '../../../../services/broad-caster.service';

@Component({
  selector: 'app-school-add',
  templateUrl: './school-add.component.html',
  styleUrls: ['./school-add.component.scss']
})
export class SchoolAddComponent implements OnInit {

  schoolForm: FormGroup;
  submitted = false;
  loading = false;
  hide = false;
  statesList: any = [];
  citiesList: any = [];
  bookSellerId: number = 0;
  schoolDetails:any={};
  pageTitle="Add new school";
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private sessionKeys: SessionKeys,
    private commonService: CommonService,
    private sessionService: SessionService,
    private activeRoute: ActivatedRoute,
    private schoolService: SchoolService,
    private commonAPIsService: CommonAPIsService,
    private broadcasterService: BroadCasterService,

  ) { }

  ngOnInit() {
    this.initializeForm();
    this.activeRoute.params.subscribe(params => {
      if (params['id']) {
        this.bookSellerId = params['id'];
        this.getschoolDetails();
        this.broadcasterService.broadcast("PageTitle", "Edit School");
      }
      else
      {
        this.broadcasterService.broadcast("PageTitle", "Add School");
      }
    })
  }

  initializeForm() {
    this.schoolForm = this.formBuilder.group({
      Id: [0],
      Name: ['', Validators.required],
      Address: ['', Validators.required],
      MobileNo: ['', Validators.required],
    })
  }

  getschoolDetails() {
    this.schoolDetails = this.sessionService.getSessionAsJSON(this.sessionKeys.Keys.School.Details);
    if (this.schoolDetails) {
      for (var prop in this.schoolDetails) {
        if (Object.prototype.hasOwnProperty.call(this.schoolDetails, prop)) {
          const formControl = this.schoolForm.get(prop);
          if (formControl) {
            formControl.setValue(this.schoolDetails[prop]);
         }
        }
      }
    }
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.schoolForm.controls[controlName].hasError(errorName);
  }

  onSubmit(formData) {
    this.submitted = true;
    if (this.schoolForm.invalid) {
      return;
    }
    this.commonService.showSpinner();
    this.schoolService.save(formData)
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.commonService.showSuccessMessage(response.Data);
          this.router.navigate(["/admin/school"]);
        })
  }

  onCancel() {
    this.router.navigate(["/admin/school"]);
  }

  onBack() {
    this.router.navigate(["/admin/school"]);
  }

  ngOnDestory() {
    this.sessionService.deleteSession(this.sessionKeys.Keys.School.Details)
  }

}
