import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthModule } from './auth/auth.module';
import { HomeComponent } from './home/home.component';
import { LayoutComponent } from './Layouts/layout/layout.component';
import { ProductModule } from './Features-Module/product/product.module';
import { OrderModule } from './Features-Module/order/order.module';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { AccountComponent } from './User/account/account.component';
import { SearchbarProductlistComponent } from './searchbar-productlist/searchbar-productlist.component';

const routes: Routes = [
  {
    path:'',
    component:LayoutComponent,
    children:[
      {
        path:'',component:HomeComponent
      },
      {
        path:'home',component:HomeComponent
       },
       { 
         path: 'search-results', component: SearchbarProductlistComponent 
       },
       {
        path:'about-us',component:AboutUsComponent
       },
       {
        path:'contact-us',component:ContactUsComponent
       },
       {
        path:'user/account',component:AccountComponent
       },
       {
        path:'auth', loadChildren: () => AuthModule
       },
       {
        path:'product', loadChildren: () => ProductModule
       },
       {
        path:'order', loadChildren: () => OrderModule
       }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
