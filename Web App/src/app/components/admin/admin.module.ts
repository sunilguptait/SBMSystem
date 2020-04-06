import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminFooterComponent } from './shared/admin-footer/admin-footer.component';
import { AdminHeaderComponent } from './shared/admin-header/admin-header.component';
import { AdminSidebarComponent } from './shared/admin-sidebar/admin-sidebar.component';
import { AdminLayoutComponent } from './shared/admin-layout/admin-layout.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdminRoutingModule } from './admin-routing.module';
import { SharedModule } from '../../shared.module';
import { LayoutModule } from 'angular-admin-lte';    //Loading layout module
import { BoxModule } from 'angular-admin-lte';
import { BookSellerAddComponent,BookSellersListComponent } from './book-sellers';
import { SchoolAddComponent,SchoolListComponent } from './school';
import { ClassAddComponent,ClassListComponent } from './class';
import { BookSellerSchoolMappingAddComponent,BookSellerSchoolMappingListComponent } from './book-seller-class-mapping';
import { ImportStudentsComponent } from './import-students/import-students.component';


@NgModule({
  declarations: [
    AdminFooterComponent, 
    AdminHeaderComponent, 
    AdminSidebarComponent, 
    AdminLayoutComponent, 
    AdminDashboardComponent, 
    BookSellerAddComponent, 
    BookSellersListComponent, 
    SchoolAddComponent,
    SchoolListComponent,
    ClassAddComponent,
    ClassListComponent,
    BookSellerSchoolMappingListComponent,
    BookSellerSchoolMappingAddComponent,
    ImportStudentsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    AdminRoutingModule,
    LayoutModule,
    BoxModule
  ]
})
export class AdminModule { }
