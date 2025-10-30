import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductRoutingModule } from './product-routing.module';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductService } from '../product/product.service';
import { SidebarComponent } from './sidebar/sidebar.component';
import { FormsModule } from '@angular/forms';
import { WishlistComponent } from './wishlist/wishlist.component';

@NgModule({
  declarations: [
    ProductListComponent,
    ProductDetailComponent,
    SidebarComponent,
    WishlistComponent
  ],
  imports: [
    CommonModule,
    ProductRoutingModule,
    FormsModule 
  ],
  providers: [ProductService],
  exports: [
    WishlistComponent
    // other exports
  ]
})
export class ProductModule { }
