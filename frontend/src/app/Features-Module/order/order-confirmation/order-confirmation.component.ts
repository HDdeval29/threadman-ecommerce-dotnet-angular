import { Component, OnInit  } from '@angular/core';
import { OrderService } from '../order.service';
@Component({
  selector: 'app-order-confirmation',
  templateUrl: './order-confirmation.component.html',
  styleUrls: ['./order-confirmation.component.css']
})
export class OrderConfirmationComponent implements OnInit {

  orderConfirmed: boolean = false;
  deliveryAddress: any;
  orderDetails: any;
  thankYouMessage: string = '';
  User:any;
  userAdd : string = '';
  errorMessage: string = '';
  userAddress: any | null = null; 
  OrderId:string = '';
  loaderforpayment = false;
  LoaderVal = "";
  loading:boolean = false;
  constructor(private orderService: OrderService) {

    const userDetailsString: string | null = sessionStorage.getItem('UserDetail');
    if (userDetailsString) {
      try {
        this.User = JSON.parse(userDetailsString);
  
        if (this.User && this.User.userProfileWithAddresses) {
          const userAddress = this.User.userProfileWithAddresses;
  
          if (Array.isArray(userAddress.addresses)) {
            this.userAdd = userAddress.addresses.find((addr: any) => addr.isDefault) || null;
          } else {
            console.error('Addresses are not in the expected format.');
          }
        } else {
          console.error('User profile not found.');
        }
      } catch (error) {
        console.error('Error parsing user details:', error);
        this.errorMessage = 'Invalid user details format.';
      }
    } else {
      console.error('No user details found in session storage.');
      this.errorMessage = 'User details not found.';
    }
  }

  ngOnInit(): void {
    this.confirmOrder();
  }

  confirmOrder() {
     this.loaderforpayment = true;
    this.LoaderVal = "Please Wait....";
    //this.loading = true;
    const orderId = localStorage.getItem('orderId');

    setTimeout(() => {
      if (orderId) {
        //this.loading = false;

        this.OrderId = orderId;
        this.loaderforpayment = false;
        this.orderConfirmed = true;
        this.deliveryAddress = this.userAdd;
        //this.orderDetails = response.orderDetails;
        this.thankYouMessage = "Thank you for your purchase!";
        this.orderService.clearCart();
      } else {
        this.errorMessage = 'No order ID found in local storage';
      }
    }, 10000);
    
  }

  getEstimatedDeliveryDate() {
    const today = new Date();
    const deliveryDate = new Date(today);
    deliveryDate.setDate(today.getDate() + 5); 
    return deliveryDate.toLocaleDateString(); 
  }
}
