import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviourServiceService } from 'src/app/behaviour-service.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {
  activeSection: string = 'Personal-info'; 
  
  User:any;

  selectedAddress: any = {}; 

  selectedOrder: any;

  constructor(private router:Router, private _bs:BehaviourServiceService)
  {
    var user_details:any = sessionStorage.getItem('UserDetail')
    var user = JSON.parse(user_details)
  }

  ngOnInit(){
    this.User = this._bs.GetLocalUser()

    this._bs.returnLogin().subscribe((res: any) => {
      this.User = this._bs.GetLocalUser()
    });
  }

  selectSection(section: string) {
    this.activeSection = section;
  }

  getSectionContent(section : any) {
    
    switch (this.activeSection) {
      case 'Personal-info':
        return this.User;
      case 'orders':
        return this.User.userOrders;
      case 'manage-address':
        return this.User.userProfileWithAddresses;
      case 'payment':
        return this.User;
      default:
        return [];
    }

  }
  editPersonalInfo() {
    
    console.log("Editing personal information");
  }

  openModal(address: any) {
    this.selectedAddress = { ...address }; 
    $('#updateAddressModal').modal('show'); 
  }

  saveAddress() {
    // Logic to save the updated address
    console.log('Updated Address:', this.selectedAddress);
    
    // Update the address in User.userProfileWithAddresses.addresses array if needed
    // Example: this.User.userProfileWithAddresses.addresses[index] = this.selectedAddress;

    $('#updateAddressModal').modal('hide'); // Hide the modal using jQuery
  }


  selectOrder(order: any) {
    this.selectedOrder = order; // Set the selected order
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
