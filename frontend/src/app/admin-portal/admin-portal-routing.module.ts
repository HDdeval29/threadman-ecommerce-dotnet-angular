import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';

const routes: Routes = [
  { path: 'admin-portal/login', component: AdminLoginComponent },
  {
    path: 'admin-portal/dashboard',
    component: AdminDashboardComponent,
    // children: [
    //   { path: '', component: DashboardOverviewComponent }, // Default child route
    //   { path: 'settings', component: DashboardSettingsComponent }
    // ]
  },
  { path: '', redirectTo: '/admin-portal/login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminPortalRoutingModule { }
