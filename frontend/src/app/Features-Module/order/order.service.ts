import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders ,HttpParams} from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { Router } from '@angular/router';
//import { CartItem } from '../../models/product.model';
import { catchError, tap, map } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';
import { BehaviourServiceService } from 'src/app/behaviour-service.service';


@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private apiUrl = 'https://localhost:44321/Order';
  
  Token:any;

  private cartKey = 'cartItems';


  //private cart: CartItem[] = [];
  private cart: any[] = []
  //private cart: any;
  
  constructor(private http: HttpClient,private router:Router,private _bs:BehaviourServiceService) { 

    let token:any=localStorage.getItem('authToken')
    this.Token = JSON.parse(token)
    this.loadCartFromLocalStorage();
  }

  // Create authorization header with token
  private createAuthorizationHeader(): HttpHeaders {
    let headers = new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${this.Token}`);
    return headers;
  }
  
  private loadCartFromLocalStorage() {
    const storedCart = localStorage.getItem(this.cartKey);
    if (storedCart) {
      this.cart = JSON.parse(storedCart);
      //this.cartSubject.next(this.cart); // Notify subscribers
      console.log('Loaded cart from local storage:', this.cart); 
    }
  }

  private saveCartToLocalStorage() {
    localStorage.setItem(this.cartKey, JSON.stringify(this.cart));
    console.log('Cart saved to local storage:', this.cart); 
  }

  addToCart(cartItem: any): Observable<string[]> {
    
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.Token}`,
      'Content-Type': 'application/json'
    });
    this.cart.push(cartItem);
    this.saveCartToLocalStorage();
    return this.http.post<string[]>(`${this.apiUrl}/AddToCart`, cartItem, {headers}).pipe(
    tap(response => {
      if (response) {
        console.log('addtoCart',response);
        
      }
    }),
    catchError(error => {
      
      console.error('Failed to addtoCart', error);
      return throwError(() => new Error('Failed to addtoCart. Please check your credentials and try again.'));
    })
   );
  }

  UpdateCartItem(item: any):Observable<string[]>{
     const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.Token}`,
      'Content-Type': 'application/json'
    });

     // Construct the request body with all necessary fields
    const body = {
      userId: item.userId,
      productId: item.productId,
      quantity: item.quantity,
      size: item.size 
    };

    return this.http.post<string[]>(`${this.apiUrl}/UpdateCartItems`, body, { headers }).pipe(
    tap(response => {
      if (response) {
        console.log('UpdateCartItemApi',response);
        // Update the local cart
        const index = this.cart.findIndex(cartItem => cartItem.productId === item.productId);
        if (index > -1) {
          this.cart[index] = item; // Update item
        }
        this.saveCartToLocalStorage(); // Save updated cart
      }
    }),
    catchError(error => {
      
      console.error('Failed to addtoCart', error);
      return throwError(() => new Error('Failed to addtoCart. Please check your credentials and try again.'));
    })
   );
  }

  removeCartItem(item: any): Observable<string[]> {
     // Update local cart by filtering out the item to be removed
    this.cart = this.cart.filter(cartItem => cartItem.cartItemId !== item.cartItemId); 
  
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.Token}`
    });
  
     // Update local cart
     this.cart = this.cart.filter(cartItem => cartItem.cartItemId !== item.cartItemId);
     this.saveCartToLocalStorage(); // Save updated cart

    // Remove item from backend
    return this.http.delete<string[]>(`${this.apiUrl}/RemoveFromCart/${item.userId}/${item.productId}`, { headers }).pipe(
      tap(response => {
        console.log('Item removed from cart:', response);
      }),
      catchError(error => {
        console.error('Failed to remove item from cart', error);
        return throwError(() => new Error('Failed to remove item from cart.'));
      })
    );
  }

  getCartItems(): any[] {
    return this.cart;
  }

  getCartItemsFromBackend(userId: number): Observable<any> {

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.Token}`
    });
  
    return this.http.get<any>(`${this.apiUrl}/GetCartItems/${userId}`, { headers }).pipe(
      tap(response => {
        localStorage.setItem('CartItemsByUserId', JSON.stringify(this.cart));
        // Update the local cart with the response from the backend
        this.cart = response.cartItemsList; // Update the local cart
        console.log(this.cart)
        console.log(this.cart.length)
        //this._bs.updateCart(this.cart.length)
        this.saveCartToLocalStorage(); // Save to local storage
        console.log('Cart items fetched from backend:', this.cart);
      }),
      catchError(error => {
        console.error('Failed to fetch cart items', error);
        return throwError(() => new Error('Failed to fetch cart items.'));
      })
    );
  }

  UpdateCartCount(userId: number):  Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.Token}`
    });
    return this.http.get<any>(`${this.apiUrl}/UpdateCartCount/${userId}`, { headers }).pipe(
      tap(response => {
        
      }),
      catchError(error => {
        console.error('Failed to fetch cart Count', error);
        return throwError(() => new Error('Failed to fetch cart Count.'));
      })
    );
  }
 
  
  clearCart() {
    this.cart = [];
    this.saveCartToLocalStorage(); // Clear local storage
    //this.cartSubject.next(this.cart); // Notify subscribers
  }


  buyNow(item: any) {
    // Here, you can implement your logic for checkout or processing the order.
    // For now, we just log the item and clear the cart.
    console.log('Proceeding to checkout with item:', item);
    this.clearCart(); // Clear the cart after the purchase
    // Redirect to a checkout page or show a confirmation
  }
  
  confirmOrder(orderdetails:any): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.Token}`
    });
    return this.http.post<any>(`${this.apiUrl}/CreateOrder`, orderdetails, {headers}).pipe(
      tap(response => {
        if(response.status == 'Success'){
          console.log('order create Succuessfully')
        }
      }),
      catchError(error => {
        console.error('Failed to to Create Order', error);
        return throwError(() => new Error('Failed to Create Order.'));
      })
    );
  }

  // createOrder(order: Order): Observable<Order> {
  //   return this.http.post<Order>(`${this.apiUrl}/create`, order);
  // }

  // getOrdersByUserId(userId: number): Observable<Order[]> {
  //   return this.http.get<Order[]>(`${this.apiUrl}/user/${userId}`);
  // }

}
