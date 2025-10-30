import { Component, OnInit  } from '@angular/core';
import { OrderService} from '../order.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-order-cart',
  templateUrl: './order-cart.component.html',
  styleUrls: ['./order-cart.component.css']
})

export class OrderCartComponent implements OnInit{
  userId: number; 

  cartItems: any[] = [];

  currentStep = 0;
  constructor(private orderService: OrderService,private router:Router) {
    var user_details:any = sessionStorage.getItem('UserDetail')
    var User = JSON.parse(user_details)
    this.userId = User.userId;
  }

  
  ngOnInit(): void {
    this.orderService.getCartItemsFromBackend(this.userId).subscribe({
      next: (response: any) => {
        if (response.status === 'success') {
          this.cartItems = response.cartItemsList; 
        }
      },
      error: (error) => {
        console.error('Error fetching cart items:', error);
      }
    });
    //this.orderService.UpdateCartCount(this.userId );
  }

  clearCart() {
    this.orderService.clearCart();
    this.cartItems = [];
  }


  removeItem(item: any) {
    item.userId = this.userId ;
    this.orderService.removeCartItem(item).subscribe({
      next: (response) => {
        console.log('Item removed from cart:', response);
        // Optionally update local cart state or UI here
        //this.cart = this.cart.filter(cartItem => cartItem.CartItemId !== item.cartItemId);
        this.cartItems = this.cartItems.filter(cartItem => cartItem !== item);
        this.orderService.UpdateCartCount(item.userId);
      },
      error: (error) => {
        console.error('Error while removing item from cart:', error);
        // Optionally display an error message to the user
      }
    });
  }

  // Method to increase the quantity of an item
  increaseQuantity(item: any): void {
    item.quantity++; // Increase local quantity
    item.userId = this.userId;
    this.orderService.UpdateCartItem(item).subscribe({
      next: (response) => {
        this.orderService.UpdateCartCount(item.userId);
        console.log('Cart item quantity increased:', response);
        // You might want to refresh your cart items or update the UI here
      },
      error: (error) => {
        console.error('Failed to increase cart item quantity', error);
      }
    });
  }

// Method to decrease the quantity of an item
decreaseQuantity(item: any): void {
   if (item.quantity > 1) {
    item.quantity--; // Decrease local quantity
    item.userId = this.userId;
    this.orderService.UpdateCartItem(item).subscribe({
      next: (response) => {
        this.orderService.UpdateCartCount(item.userId);
        console.log('Quantity decreased successfully:', response);
        // You might want to refresh your cart items or update the UI here
      },
      error: (error) => {
        console.error('Error while decreasing quantity:', error);
      }
    });
  } else {
    // If quantity is 1, you might want to remove the item instead
    this.orderService.removeCartItem(item).subscribe({
      next: (response) => {
        console.log('Item removed from cart:', response);
        // Update the UI to reflect item removal
      },
      error: (error) => {
        console.error('Error while removing item from cart:', error);
      }
    });
  }
}
  
proceedToCheckout() {
  this.currentStep = 1;
    // Logic to handle checkout process
    console.log('Proceeding to checkout with items:', this.cartItems);
    //this.orderService.checkout(this.cartItems);
    this.router.navigate(['/order/buy-now']);
  }

  getTotal() {
    return this.cartItems.reduce((total, item) => total + item.productPrice * item.quantity, 0);
  }
}
