import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component'
import { AdminLayoutComponent } from './shared/admin-layout/admin-layout.component';
import { BookSellerAddComponent, BookSellersListComponent } from './book-sellers';
import { SchoolAddComponent, SchoolListComponent } from './school';
import { ClassListComponent, ClassAddComponent } from './class';
import { BookSellerSchoolMappingAddComponent,BookSellerSchoolMappingListComponent } from './book-seller-class-mapping';
import { ImportStudentsComponent } from './import-students/import-students.component';

const routes: Routes = [
  {
    path: '', component: AdminLayoutComponent,
    children: [
      { path: '', component: AdminDashboardComponent },
      { path: 'dashboard', component: AdminDashboardComponent },
      { path: 'book-sellers', component: BookSellersListComponent },
      { path: 'add-new-book-seller', component: BookSellerAddComponent },
      { path: 'edit-book-seller/:id', component: BookSellerAddComponent },
      {
        path: 'book-seller-mapping',
        children: [
          { path: '', component: BookSellerSchoolMappingListComponent },
          { path: 'list', component: BookSellerSchoolMappingListComponent },
          { path: 'add', component: BookSellerSchoolMappingAddComponent },
        ]
      },
      {
        path: 'school',
        children: [
          { path: '', component: SchoolListComponent },
          { path: 'list', component: SchoolListComponent },
          { path: 'add', component: SchoolAddComponent },
          { path: 'edit/:id', component: SchoolAddComponent },
        ]
      },
      {
        path: 'class',
        children: [
          { path: '', component: ClassListComponent },
          { path: 'list', component: ClassListComponent },
          { path: 'add', component: ClassAddComponent },
          { path: 'edit/:id', component: ClassAddComponent },
        ]
      },
      { path: 'import-students', component: ImportStudentsComponent },
    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
