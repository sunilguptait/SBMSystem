import { Component, OnInit } from '@angular/core';
import { CommonService, SessionService } from '../../../../services';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonAPIsService } from '../../../../services/common-apis.service';
import { SessionKeys } from '../../../../common/session-keys';
import { ConstantPool } from '@angular/compiler';
import { BookService } from '../../../../services/book.service';
import { PublisherService } from '../../../../services/publisher.service';
import { BroadCasterService } from '../../../../services/broad-caster.service';

@Component({
  selector: 'app-book-add',
  templateUrl: './book-add.component.html',
  styleUrls: ['./book-add.component.scss']
})
export class BookAddComponent implements OnInit {

  bookForm: FormGroup;
  submitted = false;
  loading = false;
  hide = false;
  publisherList: any = [];
  bookTypeList: any = [];
  bookId: number = 0;
  bookDetails: any = {};
  pageTitle = "Add Book";
  bookImageURL: any;
  bookImage: any;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private sessionKeys: SessionKeys,
    private commonService: CommonService,
    private sessionService: SessionService,
    private activeRoute: ActivatedRoute,
    private bookService: BookService,
    private commonAPIsService: CommonAPIsService,
    private publisherService: PublisherService,
    private broadcasterService: BroadCasterService,

  ) { }

  ngOnInit() {
    this.getBookTypes();
    this.getPublishers();
    this.initializeForm();
    this.activeRoute.params.subscribe(params => {
      if (params['id']) {
        this.pageTitle = "Edit Book";
        this.bookId = params['id'];
        this.getbookDetails();
        this.broadcasterService.broadcast("PageTitle", "Edit Book");
      }
      else {
        this.broadcasterService.broadcast("PageTitle", "Add Book");
      }
    })
  }

  initializeForm() {
    this.bookForm = this.formBuilder.group({
      Book_Id: [0],
      Book_Name: ['', Validators.required],
      Book_ShortName: ['', Validators.required],
      Book_PublisherId: ['', Validators.required],
      Book_Price: ['', Validators.required],
      Book_TypeId: ['', Validators.required],
    })
  }

  getbookDetails() {
    this.bookDetails = this.sessionService.getSessionAsJSON(this.sessionKeys.Keys.Book.Details);
    if (this.bookDetails) {
      this.bookImageURL=this.bookDetails.Book_Image;
      for (var prop in this.bookDetails) {
        if (Object.prototype.hasOwnProperty.call(this.bookDetails, prop)) {
          const formControl = this.bookForm.get(prop);
          if (formControl) {
            formControl.setValue(this.bookDetails[prop]);
          }
        }
      }
    }
  }

  getBookTypes() {
    this.commonAPIsService.getBookTypes()
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.bookTypeList = response.Data;
        })
  }

  getPublishers() {
    this.publisherService.getListForDropdown()
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.publisherList = response.Data;
        })
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.bookForm.controls[controlName].hasError(errorName);
  }

  onSubmit(formData) {
    this.submitted = true;
    if (this.bookForm.invalid) {
      return;
    }
    this.commonService.showSpinner();
    formData.Book_ImageFile=this.bookImage;
    this.bookService.save(formData)
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return; // show error message and return in case of any error from API
          }
          this.commonService.showSuccessMessage(response.Data);
          this.router.navigate(["/book-seller/book"]);
        })
  }

  onImageChange(fileInputEvent: any) {
    var files = fileInputEvent.target.files;
    
    if (files.length === 0)
      return;

    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.commonService.showErrorMessage("Only images are supported.");
      return;
    }
    this.bookImage = files[0];
    var reader = new FileReader();
    reader.readAsDataURL(files[0]);
    reader.onload = (_event) => {
      this.bookImageURL = reader.result;
      this.bookImage=reader.result;
    }
  }

  onCancel() {
    this.router.navigate(["/book-seller/book"]);
  }

  onBack() {
    this.router.navigate(["/book-seller/book"]);
  }
}
