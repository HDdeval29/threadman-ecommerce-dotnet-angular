import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { OrderListComponent } from '../order/order-list/order-list.component';
import { OrderDetailComponent } from '../order/order-detail/order-detail.component';
import { WishlistComponent } from './wishlist/wishlist.component';

const routes: Routes = [
  {
    path:'',component:ProductListComponent
  },
  {
    path:'product-list',component:ProductListComponent
  },
  {
    path:'product-detail',component:ProductDetailComponent
  },
  // {
  //   path:'order-list',component:OrderListComponent
  // },
  // {
  //   path:'order-detail',component:OrderDetailComponent
  // },
  { 
    path: 'product-wishlist', component: WishlistComponent 

  },
];

@NgModule({
  // declarations: [],
  // imports: [
  //   CommonModule
  // ]
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }
