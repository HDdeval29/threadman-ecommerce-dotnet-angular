import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private authUrl = 'https://localhost:44321/Authentication/token'; 

  constructor(private http: HttpClient) {
    this.getToken()
  }

  getToken(){
    
    let data = { "userName": "Hddeval@29", "password": "Deval@29" }

    this.http.post('https://localhost:44321/Authentication/token',data).subscribe((res:any)=>{
    console.log(res)
    localStorage.setItem('authToken',JSON.stringify(res.token))
   },catchError(this.handleError))
  }
 
  // // Method to retrieve the token from local storage
  // getStoredToken(): string | null {
  //   return localStorage.getItem('authToken');
  // }

  // Method to clear the token
  clearToken(): void {
    localStorage.removeItem('authToken');
  }

  // Error handling method
  private handleError(error: any): Observable<never> {
    console.error('Authentication error:', error);
    return throwError('Authentication failed; please try again later.');
  }
}
