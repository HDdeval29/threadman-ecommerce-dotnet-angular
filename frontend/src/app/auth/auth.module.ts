import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CustomerLoginComponent } from './customer-login/customer-login.component';
import { AuthService } from '../auth/auth.service';
import { CustomerSignupComponent } from './customer-signup/customer-signup.component';



@NgModule({
  declarations: [
    CustomerLoginComponent,
    CustomerSignupComponent,
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
   ],
   providers: [AuthService],
})
export class AuthModule { 

}
