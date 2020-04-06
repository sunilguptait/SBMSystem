import { Component, OnInit } from '@angular/core';
import { CommonService, SessionService } from '../../../../services';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SessionKeys } from '../../../../common/session-keys';
import { ClassService } from '../../../../services/class.service';
import { BookService } from '../../../../services/book.service';
import { BroadCasterService } from '../../../../services/broad-caster.service';


@Component({
  selector: 'app-class-book-mapping-add',
  templateUrl: './class-book-mapping-add.component.html',
  styleUrls: ['./class-book-mapping-add.component.scss']
})
export class ClassBookMappingAddComponent implements OnInit {

  mappingForm: FormGroup;
  submitted = false;
  loading = false;
  hide = false;
  booksList: any = [];
  classList: any = [];
  mappingId: number = 0;
  mappingDetails: any = {};
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private sessionKeys: SessionKeys,
    private commonService: CommonService,
    private sessionService: SessionService,
    private activeRoute: ActivatedRoute,
    private bookService: BookService,
    private classService: ClassService,
    private broadcasterService: BroadCasterService,
  ) { }

  ngOnInit() {
    this.initializeForm();
    this.getBooksList();
    this.getClasssList();
    this.broadcasterService.broadcast("PageTitle", "Add Class Book Mapping");
  }

  initializeForm() {
    this.mappingForm = this.formBuilder.group({
      BCM_Id: [0],
      BCM_BookId: ['', Validators.required],
      BCM_ClassId: ['', Validators.required],
      BCM_DefaultQty: ['1', Validators.required],
    })
  }

  getBooksList() {
    this.bookService.getBookDropdown()
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.booksList = response.Data;
        })
  }

  getClasssList() {
    this.classService.getClassDropdown()
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.classList = response.Data;
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
    this.bookService.createBookClassMapping(formData)
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.commonService.showSuccessMessage(response.Data);
          this.router.navigate(["/book-seller/class-book-mapping"]);
        })
  }

  onCancel() {
    this.router.navigate(["/book-seller/class-book-mapping"]);
  }

  onBack() {
    this.router.navigate(["/book-seller/class-book-mapping"]);
  }
}