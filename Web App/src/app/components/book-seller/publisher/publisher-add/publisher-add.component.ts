import { Component, OnInit } from '@angular/core';
import { CommonService, SessionService } from '../../../../services';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonAPIsService } from '../../../../services/common-apis.service';
import { SessionKeys } from '../../../../common/session-keys';
import { ConstantPool } from '@angular/compiler';
import { ClassService } from '../../../../services/class.service';
import { PublisherService } from '../../../../services/publisher.service';
import { BroadCasterService } from '../../../../services/broad-caster.service';

@Component({
  selector: 'app-publisher-add',
  templateUrl: './publisher-add.component.html',
  styleUrls: ['./publisher-add.component.scss']
})
export class PublisherAddComponent implements OnInit {

  publisherForm: FormGroup;
  submitted = false;
  loading = false;
  hide = false;
  statesList: any = [];
  citiesList: any = [];
  publisherId: number = 0;
  publisherDetails: any = {};
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private sessionKeys: SessionKeys,
    private commonService: CommonService,
    private sessionService: SessionService,
    private activeRoute: ActivatedRoute,
    private publisherService: PublisherService,
    private commonAPIsService: CommonAPIsService,
    private broadcasterService: BroadCasterService,

  ) { }

  ngOnInit() {
    this.initializeForm();
    this.activeRoute.params.subscribe(params => {
      if (params['id']) {
        this.publisherId = params['id'];
        this.getpublisherDetails();
        this.broadcasterService.broadcast("PageTitle", "Edit Publisher");
      }
      else {
        this.broadcasterService.broadcast("PageTitle", "Add Publisher");
      }
    })
  }

  initializeForm() {
    this.publisherForm = this.formBuilder.group({
      Id: [0],
      Name: ['', Validators.required],
      Address: ['', Validators.required],
      MobileNo: ['', Validators.required],
    })
  }

  getpublisherDetails() {
    this.publisherDetails = this.sessionService.getSessionAsJSON(this.sessionKeys.Keys.Publisher.Details);
    if (this.publisherDetails) {
      for (var prop in this.publisherDetails) {
        if (Object.prototype.hasOwnProperty.call(this.publisherDetails, prop)) {
          const formControl = this.publisherForm.get(prop);
          if (formControl) {
            formControl.setValue(this.publisherDetails[prop]);
          }
        }
      }
    }
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.publisherForm.controls[controlName].hasError(errorName);
  }

  onSubmit(formData) {
    this.submitted = true;
    if (this.publisherForm.invalid) {
      return;
    }
    this.commonService.showSpinner();
    this.publisherService.save(formData)
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.commonService.showSuccessMessage(response.Data);
          this.router.navigate(["/book-seller/publisher"]);
        })
  }

  onCancel() {
    this.router.navigate(["/book-seller/publisher"]);
  }

  onBack() {
    this.router.navigate(["/book-seller/publisher"]);
  }

  ngOnDestory() {
    this.sessionService.deleteSession(this.sessionKeys.Keys.Publisher.Details)
  }
}
