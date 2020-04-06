import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { Observable, forkJoin, observable } from 'rxjs';
import { startWith, debounceTime, switchMap, map, tap, finalize } from 'rxjs/operators';
import { OrderService } from 'src/app/services/order.service';
import { BookService } from 'src/app/services/book.service';
import { CommonService } from 'src/app/services';
import { ClassService } from 'src/app/services/class.service';
import { ActivatedRoute } from '@angular/router';
import { BroadCasterService } from 'src/app/services/broad-caster.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { AddStudentComponent } from '../add-student/add-student.component';

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.scss']
})
export class CreateOrderComponent implements OnInit {
  studentControl = new FormControl();
  classControl = new FormControl();
  filteredStudents: Observable<any[]>;
  selectedStudent: any;
  books: any[];
  orderStep: number = -1;
  orderInfo: any;
  isLoadingStudents: boolean = false;
  classList: any[];
  orderForm: FormGroup;
  orderId: number;
  studentId: number;
  orderDetails: any;
  pageTitle: string;
  modalRef: BsModalRef;
  constructor(
    private formBuilder: FormBuilder,
    private orderService: OrderService,
    private bookService: BookService,
    private commonService: CommonService,
    private classService: ClassService,
    private activeRoute: ActivatedRoute,
    private broadcasterService: BroadCasterService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {
    this.activeRoute.params.subscribe(params => {

      if (params['id'] && params['studentId']) {
        this.orderId = params['id']
        this.studentId = params['studentId'];
        this.getOrderDetails();
        this.broadcasterService.broadcast("PageTitle", "Order Details");
      }
      else {
        this.broadcasterService.broadcast("PageTitle", "Create Order");
        this.orderStep = 0;
        this.initializeForm();
        this.getClasses();
        this.initilizeStudentSeacrh();
      }

    })


  }

  getOrderDetails() {
    this.commonService.showSpinner();
    forkJoin(
      [
        this.orderService.getOrder(this.orderId),
        this.orderService.searchStudents({ StudentId: this.studentId })
      ])
      .subscribe(response => {
        if (!this.commonService.validateAPIResponse(response[0])) {
          return;
        }
        if (!this.commonService.validateAPIResponse(response[1])) {
          return;
        }
        this.orderDetails = response[0].Data;
        this.books = response[0].Data.Books;
        this.selectedStudent = response[1].Data[0];
        this.orderStep = 4;
      })
  }

  initilizeStudentSeacrh() {
    this.filteredStudents = this.studentControl.valueChanges.pipe(
      startWith(''),
      debounceTime(400),
      switchMap(value =>
        this.orderService.searchStudents({ StudentName: typeof (value) == 'object' ? value.Name : value, ClassId: this.classControl.value })
          .pipe(
            map(response => {
              return response.Data;
            }),
          )
      )
    )
  }

  initializeForm() {
    this.orderForm = this.formBuilder.group({
      PaymentMode: ['4'],
      PaymentRemark: ['']
    })
  }

  getClasses() {
    this.classService.getClassDropdown().subscribe(response => {
      if (!this.commonService.validateAPIResponse(response)) {
        return;
      }
      this.classList = response.Data;
    })
    this.classControl.setValue('0');
  }

  getStudentName(student) {
    return student ? student.Name : '';
  }

  onStudentSelected(event) {
    this.orderStep = 1;
    debugger;
    this.selectedStudent = event.option.value;
    this.getBooks();
  }

  getBooks() {
    this.commonService.showSpinner();
    this.bookService.getClassBooksForStudent({ ClassId: this.selectedStudent.ClassId, StudentId: this.selectedStudent.Id })
      .subscribe(response => {
        if (!this.commonService.validateAPIResponse(response)) {
          return;
        }
        if (response.Data) {
          response.Data.forEach(element => {
            element.Quantity = element.DefaultQuantity;
          });
        }
        this.books = response.Data;
      });
  }

  changeBookQty(book, buttonType) {
    if (buttonType == 1) {
      book.Quantity = book.Quantity - 1;
    }
    else if (buttonType == 2) {
      book.Quantity = book.Quantity + 1;
    }
  }

  getTotalBooks() {
    let total: number = this.books.filter(m => m.IsDeleted != true).reduce((a: number, b) => a + b.Quantity, 0);
    return total;
  }

  getSelectedBooks() {
    return this.books.filter(m => m.IsDeleted != true);
  }

  getTotalAmount() {
    let total: number = this.books.filter(m => m.IsDeleted != true).reduce((a: number, b) => a + (b.Quantity * b.Book_Price), 0);
    return total;
  }

  removeAddBook(book) {
    book.IsDeleted = book.IsDeleted ? !book.IsDeleted : true;
  }

  placeOrder(formData) {
    this.books.forEach(m => {
      m.TotalAmount = m.Quantity * m.Book_Price;
    })
    let request = {
      OrderDate: new Date(),
      StudentEnrollmentId: this.selectedStudent.EnrollmentId,
      OrderStatus: 1,
      OrderPaymentMode: formData.PaymentMode,
      PaymentStatus: 0,
      TotalOrderAmount: this.getTotalAmount(),
      Books: this.books.filter(m => m.IsDeleted != true),
      OrderPaymentRemark: formData.PaymentRemark
    }
    this.commonService.showSpinner();
    this.orderService.createOrder(request)
      .subscribe(
        response => {
          if (!this.commonService.validateAPIResponse(response)) {
            return;
          }
          this.commonService.showSuccessMessage('Order created successfully with order code : ' + response.Data.OrderCode);
          this.orderInfo = response.Data;
          this.books = null;
          this.orderStep = 3;
        })
  }

  changeStep(step) {
    this.orderStep = step;
  }

  createNewOrder() {
    this.orderStep = 0;
    this.selectedStudent = null;
    this.studentControl.setValue('');
  }

  classChange() {
    this.selectedStudent = null;
    this.orderStep = 0;
    this.studentControl.setValue('');
  }

  addStudent() {
    debugger
    this.modalRef = this.modalService.show(AddStudentComponent);
    this.modalRef.content.classList = this.classList;
    this.modalRef.content.onClose.subscribe(m => {
      this.modalRef.hide();
    })
    this.modalRef.content.onSuccess.subscribe(m => {
      this.modalRef.hide();
      this.orderStep = 1;
      this.selectedStudent = m;
      this.studentControl.setValue(m.Name);
      this.getBooks();

    })
  }
}
