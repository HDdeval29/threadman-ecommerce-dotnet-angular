import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OrderRoutingModule } from './order-routing.module';
import { OrderListComponent } from './order-list/order-list.component';
import { OrderDetailComponent } from './order-detail/order-detail.component';
import { OrderCartComponent } from './order-cart/order-cart.component';
import { BuyNowComponent } from './buy-now/buy-now.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatNativeDateModule} from '@angular/material/core';
import { OrderConfirmationComponent } from './order-confirmation/order-confirmation.component';

@NgModule({
  declarations: [
    OrderListComponent,
    OrderDetailComponent,
    OrderCartComponent,
    BuyNowComponent,
    OrderConfirmationComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    OrderRoutingModule,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
    MatFormFieldModule
   ],
  exports: [
    OrderCartComponent // Export if needed in other modules
  ]
})
export class OrderModule { }
