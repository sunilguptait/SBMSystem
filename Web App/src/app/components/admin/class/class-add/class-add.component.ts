import { Component, OnInit } from '@angular/core';
import { CommonService, SessionService } from '../../../../services';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonAPIsService } from '../../../../services/common-apis.service';
import { SessionKeys } from '../../../../common/session-keys';
import { ConstantPool } from '@angular/compiler';
import { ClassService } from '../../../../services/class.service';
import { BroadCasterService } from '../../../../services/broad-caster.service';

@Component({
  selector: 'app-class-add',
  templateUrl: './class-add.component.html',
  styleUrls: ['./class-add.component.scss']
})
export class ClassAddComponent implements OnInit {
  classForm: FormGroup;
  submitted = false;
  loading = false;
  hide = false;
  statesList: any = [];
  citiesList: any = [];
  classId: number = 0;
  classDetails: any = {};
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private sessionKeys: SessionKeys,
    private commonService: CommonService,
    private sessionService: SessionService,
    private activeRoute: ActivatedRoute,
    private classService: ClassService,
    private broadcasterService: BroadCasterService,

  ) { }

  ngOnInit() {
    this.initializeForm();
    this.activeRoute.params.subscribe(params => {
      if (params['id']) {
        this.classId = params['id'];
        this.getClassDetails();
        this.broadcasterService.broadcast("PageTitle", "Edit Class");
      }
      else {
        this.broadcasterService.broadcast("PageTitle", "Add Class");
      }
    })
  }

  initializeForm() {
    this.classForm = this.formBuilder.group({
      Id: [0],
      Name: ['', Validators.required],
      ShortName: ['', Validators.required],
    })
  }

  getClassDetails() {
    this.classDetails = this.sessionService.getSessionAsJSON(this.sessionKeys.Keys.Class.Details);
    if (this.classDetails) {
      for (var prop in this.classDetails) {
        if (Object.prototype.hasOwnProperty.call(this.classDetails, prop)) {
          const formControl = this.classForm.get(prop);
          if (formControl) {
            formControl.setValue(this.classDetails[prop]);
          }
        }
      }
    }
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.classForm.controls[controlName].hasError(errorName);
  }

  onSubmit(formData) {
    this.submitted = true;
    if (this.classForm.invalid) {
      return;
    }
    this.commonService.showSpinner();
    this.classService.save(formData)
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.commonService.showSuccessMessage(response.Data);
          this.router.navigate(["/admin/class"]);
        })
  }

  onCancel() {
    this.router.navigate(["/admin/class"]);
  }

  onBack() {
    this.router.navigate(["/admin/class"]);
  }

  ngOnDestory() {
    this.sessionService.deleteSession(this.sessionKeys.Keys.Class.Details)
  }
}
