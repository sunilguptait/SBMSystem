import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './services/auth.service';


const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  // {path:"user",loadChildren:'./user/user.module#UserModule',canActivate:[AuthGuard]  },
  { path: "admin", loadChildren: './components/admin/admin.module#AdminModule', canActivate: [AuthGuard] },
  { path: "book-seller", loadChildren: './components/book-seller/book-seller.module#BookSellerModule', canActivate: [AuthGuard] },
  { path: "**", component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
