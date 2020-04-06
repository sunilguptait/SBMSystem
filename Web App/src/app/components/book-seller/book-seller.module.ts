import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookSellerFooterComponent } from './shared/book-seller-footer/book-seller-footer.component';
import { BookSellerHeaderComponent } from './shared/book-seller-header/book-seller-header.component';
import { BookSellerSidebarComponent } from './shared/book-seller-sidebar/book-seller-sidebar.component';
import { BookSellerLayoutComponent } from './shared/book-seller-layout/book-seller-layout.component';
import { BookSellerDashboardComponent } from './book-seller-dashboard/book-seller-dashboard.component';
import { BookSellerRoutingModule } from './book-seller-routing.module';
import { SharedModule } from '../../shared.module';
import { LayoutModule } from 'angular-admin-lte';    //Loading layout module
import { BoxModule } from 'angular-admin-lte';
import { PublisherAddComponent,PublisherListComponent } from './publisher';
import { BookAddComponent,BookListComponent } from './book';
import { ClassBookMappingAddComponent,ClassBookMappingListComponent } from './class-book-mapping';
import { CreateOrderComponent } from './order/create-order/create-order.component';
import { OrderListComponent } from './order/order-list/order-list.component';
import { AddStudentComponent } from './order/add-student/add-student.component';
import { ModalModule } from 'ngx-bootstrap';


@NgModule({
  declarations: [
    BookSellerFooterComponent, 
    BookSellerHeaderComponent, 
    BookSellerSidebarComponent, 
    BookSellerLayoutComponent, 
    BookSellerDashboardComponent, 
    PublisherAddComponent,
    PublisherListComponent,
    BookAddComponent,
    BookListComponent,
    ClassBookMappingAddComponent,
    ClassBookMappingListComponent,
    CreateOrderComponent,
    OrderListComponent,
    AddStudentComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    BookSellerRoutingModule,
    LayoutModule,
    BoxModule
  ],
  entryComponents:[AddStudentComponent]
})
export class BookSellerModule { }
