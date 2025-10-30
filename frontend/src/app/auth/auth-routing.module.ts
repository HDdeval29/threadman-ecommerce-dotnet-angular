import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerLoginComponent } from './customer-login/customer-login.component';
import { CustomerSignupComponent } from './customer-signup/customer-signup.component';

const routes: Routes = [
  {
    path:'',component:CustomerLoginComponent
  },
  {
    path:'Userlogin',component:CustomerLoginComponent
  },
  {
    path:'UserSignup',component:CustomerSignupComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
