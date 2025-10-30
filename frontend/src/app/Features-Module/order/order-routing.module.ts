import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ProductListComponent } from '../product/product-list/product-list.component';
import { OrderCartComponent } from './order-cart/order-cart.component';
import { OrderListComponent } from './order-list/order-list.component';
import { BuyNowComponent } from './buy-now/buy-now.component';
import { OrderConfirmationComponent } from './order-confirmation/order-confirmation.component';
import { OrderDetailComponent } from './order-detail/order-detail.component';


const routes: Routes = [
  { 
    path: '', component: ProductListComponent 
  }, 
  { 
    path: 'cart', component: OrderCartComponent
  }, 
  { 
    path: 'buy-now', component: BuyNowComponent
  },
  { 
    path: 'order-confirmation', component: OrderConfirmationComponent
  },
  { 
    path: 'order-details', component: OrderDetailComponent
  }
  
];

@NgModule({
  // declarations: [
  //   OrderCartComponent,
  //   OrderListComponent
  // ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule],
})
export class OrderRoutingModule { }
