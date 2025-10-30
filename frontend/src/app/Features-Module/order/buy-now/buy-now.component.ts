import { Component } from '@angular/core';
import { OrderService} from '../order.service';
//import { CartItem } from '../../../models/product.model';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { Router } from '@angular/router';
import { PaymentService } from '../payment.service';
declare var Razorpay: any;

@Component({
  selector: 'app-buy-now',
  templateUrl: './buy-now.component.html',
  styleUrls: ['./buy-now.component.css']
})
export class BuyNowComponent {
  User:any;
  defaultAddress: any | null = null;
  userAddress: any | null = null;
  cartItems: any[] = [];

  //selectedPaymentMode: string = '';
  couponCode: string = '';
  discountApplied: boolean = false;
  discountPercentage: number = 10; // Example discount
  discountedTotal: number = 0;
  selectedPaymentMode: string = '';
  cartTotal: number = 100; // Example total
  currency: string = 'INR';

  // Card payment details
  cardNumber: string = '';
  expiryDate: string = '';
  cvv: string = '';

  // UPI payment details
  upiId: string = '';


  // Toggle for showing online payment options
  showOnlinePaymentOptions: boolean = false;

  // Toggle for showing pay on delivery options
  showPayOnDelivery: boolean = false;

  payableamount:any;

  constructor(private orderService: OrderService,private router:Router, private ps: PaymentService) {
    this.cartItems = this.orderService.getCartItems(); // Get cart items from service
    var user_details:any = sessionStorage.getItem('UserDetail')
    this.User = JSON.parse(user_details)
  }

  ngOnInit(): void {

    this.loadUserProfile();
  }

  onDateChange(event: MatDatepickerInputEvent<Date>) {
    const selectedDate = event.value;
    if (selectedDate) {
      const month = selectedDate.getMonth() + 1; // Months are zero-based
      const year = selectedDate.getFullYear() % 100; // Get last two digits of year
      this.expiryDate = `${month < 10 ? '0' + month : month}/${year < 10 ? '0' + year : year}`;
    }
  }

  // If you're using dateClass, define it here (optional)
  dateClass(date: Date) {
    // Example: Mark today's date
    const today = new Date();
    return today.toDateString() === date.toDateString() ? 'mat-datepicker-today' : '';
  }

  loadUserProfile() {

    if (this.User && this.User.userProfileWithAddresses) {
      // Directly assign the user profile
      this.userAddress = this.User.userProfileWithAddresses;

      // Check if addresses exist and are an array
      if (this.userAddress.addresses && Array.isArray(this.userAddress.addresses)) {
        // Find the default address
        this.defaultAddress = this.userAddress?.addresses?.find((addr : any) => addr.isDefault) || null;
      } else {
        console.error('Addresses are not in the expected format.');
      }
    } else {
      console.error('User profile not found');
    }

  }

  editAddress() {
    // Logic to navigate to the edit address page or open a modal
  }

  toggleOnlinePaymentOptions() {
    this.showOnlinePaymentOptions = !this.showOnlinePaymentOptions;
  }

  togglePayOnDelivery() {
    this.showPayOnDelivery = !this.showPayOnDelivery;
  }

  selectPaymentMode(mode: string) {
    this.selectedPaymentMode = mode;

    // Close the pay on delivery section if another payment method is selected
    if (mode !== 'pay-on-delivery') {
      this.showPayOnDelivery = false;
    }
  }

  getTotal() {
    this.payableamount = this.cartItems.reduce((total, item) => total + item.productPrice * item.quantity, 0);
    return this.cartItems.reduce((total, item) => total + item.productPrice * item.quantity, 0);
  }

  applyCoupon() {
    if (this.couponCode === 'SAVE10') {
      this.discountedTotal = this.getTotal() * (1 - this.discountPercentage / 100);
      this.discountApplied = true;
    } else {
      this.discountApplied = false;
    }
  }

  cancelPurchase() {
    console.log('Purchase canceled.');
  }


  confirmPurchase() {
    // let paymentDetails: any;
    // if (this.selectedPaymentMode === 'credit-card') {
    //   paymentDetails = {
    //     amount: this.cartTotal,
    //     currency: this.currency,
    //     paymentMethod: this.selectedPaymentMode,
    //     cardNumber: this.cardNumber,
    //     expiryDate: this.expiryDate,
    //     cvv: this.cvv,
    //   };
    // } else if (this.selectedPaymentMode === 'upi') {
    //   paymentDetails = {
    //     amount: this.cartTotal,
    //     currency: this.currency,
    //     paymentMethod: this.selectedPaymentMode,
    //     upiId: this.upiId,
    //   };
    // } else if (this.selectedPaymentMode === 'phone-pe' || this.selectedPaymentMode === 'google-pay') {
    //   paymentDetails = {
    //     amount: this.cartTotal,
    //     currency: this.currency,
    //     paymentMethod: this.selectedPaymentMode,
    //   };
    // } else if (this.selectedPaymentMode === 'pay-on-delivery') {
    //   paymentDetails = {
    //     amount: this.cartTotal,
    //     currency: this.currency,
    //     paymentMethod: this.selectedPaymentMode,
    //   };
    // }
    var add = this.userAddress.addresses.find((addr : any) => addr.isDefault)


     // Prepare the order details
    const orderDetails = {
      userId: this.User.userId,  // Replace with actual user ID from authentication
      totalAmount: this.getTotal(),
      shippingAddress: add.addressLine1 +'-'+ add.city +'-'+  add.state +'-'+  add.zipCode +'-'+  add.country
    };

    this.orderService.confirmOrder(orderDetails).subscribe(response => {
      if(response.status == 'Success'){
        localStorage.setItem('orderId', response.orderId);
      localStorage.setItem('deliveryAddress', orderDetails.shippingAddress);
      this.router.navigate(['/order/order-confirmation']);
      }
      else{
        alert('Order submission failed');
      }

    }, error => {
      console.error('Order creation failed', error);
      // Handle error (e.g., show a notification)
    });
  }

  payNow() {
    debugger
    this.ps.createOrder(this.payableamount).subscribe(order => {
      const options = {
        key: 'rzp_test_RPjT5fDesHKouX',   // apna TEST key id
        amount: order.amount,
        currency: order.currency,
        name: 'My Test Shop',
        description: 'Order Payment',
        order_id: order.orderId,
        handler: (response: any) => {
          this.ps.verifyPayment(response).subscribe(res => {
            alert('✅ Payment Verified!');
          }, err => {
            alert('❌ Verification Failed');
          });
        },
        prefill: { name: 'Test User', email: 'test@example.com' },
        theme: { color: '#3399cc' }
      };
      const rzp = new Razorpay(options);
      rzp.open();
    });
  }
}
