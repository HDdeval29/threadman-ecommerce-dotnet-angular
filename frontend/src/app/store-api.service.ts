import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders  } from '@angular/common/http';
import { Observable ,throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { AuthService } from './auth/auth.service';
import { Router } from '@angular/router';
import { BehaviourServiceService } from './behaviour-service.service';

@Injectable({
  providedIn: 'root'
})

export class StoreApiService {
  
  Token:any;

  constructor(private http: HttpClient, private authService: AuthService,private router:Router, private _bs:BehaviourServiceService) {
    let token:any=localStorage.getItem('authToken')
    this.Token = JSON.parse(token)
    }

  // Create authorization header with token
  private createAuthorizationHeader(): HttpHeaders {
    let headers = new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${this.Token}`);
    return headers;
  }

  
  userlogin(endpoint: string,credentials: { userName: string; password: string }):Observable<any>{

    const headers = this.createAuthorizationHeader();

    return this.http.post<any>('https://localhost:44321/User/GetUser', credentials, {headers}).pipe(
      tap(response => {
        if (response) {
          console.log(response);
          if(response.result == 'Login Successfully')
          {
               sessionStorage.setItem('UserDetail', JSON.stringify(response))
               this._bs.login(true);   
          }
          else
          {
            alert(response.result)
            this._bs.login(false); 
          }
        }
      }),
      catchError(error => {
        
        console.error('Customer login failed', error);
        return throwError(() => new Error('Customer login failed. Please check your credentials and try again.'));
      })
    );
  }

  userSignup(endpoint: string,credentials: { userName: string; password: string }):Observable<any>{
    const headers = this.createAuthorizationHeader();
    return this.http.post<any>('https://localhost:44321/User/RegisterUser', credentials, {headers}).pipe(
     tap(response => {
      if (response){

      }
     }),
     catchError(error => {
      console.error('Customer login failed', error);
        return throwError(() => new Error('Customer Signup failed. Please check your credentials and try again.'));
     })
    );
  }

  // Method to check if the user is authenticated
  userIsAuthenticated() {
    var user_details:any = sessionStorage.getItem('UserDetail')
    var User = JSON.parse(user_details)
    return User
   }


   getSuggestions(search: string): Observable<any[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<any[]>(`https://localhost:44321/Products/GetSuggestionsForSearch?search=${encodeURIComponent(search)}`, { headers });
  }

  getProductBySearchBar(searchText: string): Observable<string[]> {
    const headers = new HttpHeaders({
        'Authorization': `Bearer ${this.Token}`,
        'Content-Type': 'application/json'
    });

    const filters = {
        SearchText: searchText
    };

    return this.http.post<string[]>('https://localhost:44321/Products/GetSearchBarProduct', filters, { headers }).pipe(
        tap(response => {
            if (response) {
                console.log('Products fetched:', response);
            }
        }),
        catchError(error => {
            console.error('Failed to fetch products:', error);
            return throwError(() => new Error('Failed to fetch products.'));
        })
    );
}

}

