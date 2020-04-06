import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BookSellerDashboardComponent } from './book-seller-dashboard/book-seller-dashboard.component';
import { BookSellerLayoutComponent } from './shared/book-seller-layout/book-seller-layout.component';
import { PublisherAddComponent,PublisherListComponent } from './publisher';
import { BookAddComponent,BookListComponent } from './book';
import { ClassBookMappingAddComponent,ClassBookMappingListComponent } from './class-book-mapping';
import { CreateOrderComponent } from './order/create-order/create-order.component';
import { OrderListComponent } from './order/order-list/order-list.component';

const routes: Routes = [
  {
    path: '', component: BookSellerLayoutComponent,
    children: [
      { path: '', component: BookSellerDashboardComponent },
      { path: 'dashboard', component: BookSellerDashboardComponent },
      {
        path: 'book',
        children: [
          { path: '', component: BookListComponent },
          { path: 'list', component: BookListComponent },
          { path: 'add', component: BookAddComponent },
          { path: 'edit/:id', component: BookAddComponent },
        ]
      },
      {
        path: 'class-book-mapping',
        children: [
          { path: '', component: ClassBookMappingListComponent },
          { path: 'list', component: ClassBookMappingListComponent },
          { path: 'add', component: ClassBookMappingAddComponent },
        ]
      },
      {
        path: 'publisher',
        children: [
          { path: '', component: PublisherListComponent },
          { path: 'list', component: PublisherListComponent },
          { path: 'add', component: PublisherAddComponent },
          { path: 'edit/:id', component: PublisherAddComponent },
        ]
      },
      {
        path: 'order',
        children: [
          { path: 'create', component: CreateOrderComponent },
          { path: 'list', component: OrderListComponent },
          { path: 'details/:id/:studentId', component: CreateOrderComponent },
        ]
      },
    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookSellerRoutingModule { }
