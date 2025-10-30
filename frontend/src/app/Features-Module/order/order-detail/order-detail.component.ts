import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviourServiceService } from 'src/app/behaviour-service.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css']
})
export class OrderDetailComponent {
  User:any;
  selectedOrder: any;

  userOrders: any | null = null; 

  constructor(private router:Router, private _bs:BehaviourServiceService)
  {
    const user_details: string | null = sessionStorage.getItem('UserDetail');
    if (user_details) {
      this.User = JSON.parse(user_details);
    } else {
      console.error('No user details found in session storage');
    }
  }

  ngOnInit(){

   this.loadUserOrders();
   
  }


  loadUserOrders() {
    
    if (this.User && this.User.userOrders) {
      this.userOrders = this.User.userOrders; 
    } else {
      console.error('User Orders not found at Order-details Component');
    }
      
  }
  
  selectOrder(order: any) {
    this.selectedOrder = order; 
  }
  
  cancelOrder(order: any) {
    // Logic to cancel the order
    // if (confirm(`Are you sure you want to cancel Order ID: ${order.id}?`)) {
    //   // Find the order index and update the status
    //   const orderIndex = this.User.userOrders.findIndex(o => o.id === order.id);
    //   if (orderIndex !== -1) {
    //     this.User.userOrders[orderIndex].status = 'Canceled';
    //     // Additional logic for updating the backend can be added here
    //     alert(`Order ID: ${order.id} has been canceled.`);
    //   }
    // }
  }

  returnOrder(order: any) {
    // Logic to initiate the return process
    // if (confirm(`Are you sure you want to return Order ID: ${order.id}?`)) {
    //   // Find the order index and update the status
    //   const orderIndex = this.User.userOrders.findIndex(o => o.id === order.id);
    //   if (orderIndex !== -1) {
    //     this.User.userOrders[orderIndex].status = 'Returned';
    //     // Additional logic for updating the backend can be added here
    //     alert(`Order ID: ${order.id} has been marked for return.`);
    //   }
    // }
  }
}
