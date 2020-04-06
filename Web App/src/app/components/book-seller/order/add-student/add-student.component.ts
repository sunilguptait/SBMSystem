import { Component, OnInit, Input, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { SessionKeys } from 'src/app/common/session-keys';
import { CommonService, SessionService } from 'src/app/services';
import { SchoolService } from 'src/app/services/school.service';
import { Subscription, Subject } from 'rxjs';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.scss']
})
export class AddStudentComponent implements OnInit {
  onClose: EventEmitter<any> = new EventEmitter();
  onSuccess: EventEmitter<any> = new EventEmitter();
  studentForm: FormGroup;
  submitted = false;
  classList: any = [];

  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private sessionKeys: SessionKeys,
    private commonService: CommonService,
    private sessionService: SessionService,
    private activeRoute: ActivatedRoute,
    private orderService: OrderService, ) { }

  ngOnInit() {
    this.initializeForm();
  }

  initializeForm() {
    this.studentForm = this.formBuilder.group({
      ClassId: ['', Validators.required],
      Name: ['', Validators.required],
      ParentName: ['', Validators.required],
      MobileNo: ['', Validators.required],
    })
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.studentForm.controls[controlName].hasError(errorName);
  }

  onSubmit(formData) {
    this.submitted = true;
    if (this.studentForm.invalid) {
      return;
    }
    this.commonService.showSpinner();
    var selectedSchool = this.commonService.getSelectedSchool();
    if (!selectedSchool) {
      this.commonService.showErrorMessage("School not selected.");
      return;
    }
    formData.SchoolId=selectedSchool.Id;
    this.orderService.saveStudent(formData)
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return;
          }
          this.commonService.showSuccessMessage("New student added successfully.");
          if (this.onSuccess) {
            this.onSuccess.emit(response.Data);
          }
        })
  }

  onCancel() {
    if (this.onClose) {
      this.onClose.emit();
    }
  }
}
