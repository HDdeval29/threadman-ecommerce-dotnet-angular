import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { AdminPortalRoutingModule } from './admin-portal-routing.module';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdminLoginComponent } from './admin-login/admin-login.component';

import { NgChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [
    AdminDashboardComponent,
    AdminLoginComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    AdminPortalRoutingModule,
    NgChartsModule
  ]
})
export class AdminPortalModule { }
