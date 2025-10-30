import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders ,HttpParams} from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { Product } from '../../models/product.model';
import { AuthService } from '../../auth/auth.service';
import { Router } from '@angular/router';
import { catchError, tap } from 'rxjs/operators';
import { Filter } from '../../models/filter.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  // Mock filter data
  private filters: Filter = {
    category: ['Electronics', 'Clothing', 'Home'],
    size: ['S', 'M', 'L', 'XL'],
    type: ['New', 'Sale'],
    brand: ['Brand A', 'Brand B'],
    fabric: ['Cotton', 'Wool', 'Polyester'],
  };


  private apiUrl = 'https://localhost:44321/Products';
  
  Token:any;

  constructor(private http: HttpClient,private authService: AuthService,private router:Router) { 

    let token:any=localStorage.getItem('authToken')
    this.Token = JSON.parse(token)
  }

  // Create authorization header with token
  private createAuthorizationHeader(): HttpHeaders {
    let headers = new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${this.Token}`);
    return headers;
  }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }


  getAllProduct():Observable<Product[]> {
    // const headers = this.createAuthorizationHeader();
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.Token}`,
      'Content-Type': 'application/json'
    });
    console.log(headers)
     return this.http.get<Product[]>('https://localhost:44321/Products/GetProduct', {headers}).pipe(
      tap(response => {
        if (response) {
          console.log(response);
        }
      }),
      catchError(error => {
        
        console.error('Failed to Get Product', error);
        return throwError(() => new Error('Failed to Get Product. Please check your credentials and try again.'));
      })
    );
  }



  getProductsByCategory(category: 'Men' | 'Women' | 'Kids'): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}?category=${category}`);
  }




  // Method to get filter data
  getFilters(): Observable<Filter> {
    return of(this.filters);
  }



  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/products/${id}`);
  }

  // addToWishlist(productId: number): Observable<void> {
  //   return this.http.post<void>(`${this.apiUrl}/wishlist`, { productId });
  // }

  addToCart(productId: number, quantity: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/cart`, { productId, quantity });
  }

  buyNow(productId: number, quantity: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/buy-now`, { productId, quantity });
  }





  getProductbyFilter(endpoint: string, filters:any):Observable<Product[]>{
     
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.Token}`,
      'Content-Type': 'application/json'
    });
  
    console.log(headers)
       return this.http.post<Product[]>('https://localhost:44321/Products/SearchProductbyFilter', filters, {headers}).pipe(
        tap(response => {
          if (response) {
            console.log('SearchProductbyFilter',response);
          }
        }),
        catchError(error => {
          
          console.error('Failed to Get Product', error);
          return throwError(() => new Error('Failed to Get Product. Please check your credentials and try again.'));
        })
      );
  }









  getWishlist(userId: number): Observable<Product[]> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.Token}`,
      'Content-Type': 'application/json'
    });
    // const params = new HttpParams().set('userId', userId.toString());
    return this.http.get<Product[]>(`${this.apiUrl}/GetWishlist/${userId}`,{headers});
  }

  addToWishlist(userId: number, productId: number): Observable<void> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.Token}`,
      'Content-Type': 'application/json'
    });
    return this.http.post<void>(`${this.apiUrl}/AddToWishlist`, { userId, productId }, {headers});
  }

  removeFromWishlist(userId: number, productId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/RemoveFromWishlist/${userId}/${productId}`);
  }

}
