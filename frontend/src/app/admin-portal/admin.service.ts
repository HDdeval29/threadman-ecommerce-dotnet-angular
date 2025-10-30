import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders ,HttpParams} from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private apiUrl = 'https://localhost:44321/Admin';
  
  Token:any;

  constructor(private http: HttpClient,private router:Router) { 

    let token:any=localStorage.getItem('authToken')
    this.Token = JSON.parse(token)
  }

  // Create authorization header with token
  private createAuthorizationHeader(): HttpHeaders {
    let headers = new HttpHeaders();
      headers = headers.set('Authorization', `Bearer ${this.Token}`);
    return headers;
  }

  adminlogin(credentials: { username: string; password: string; role: string }): Observable<any> {
    const headers = this.createAuthorizationHeader();

    return this.http.post<any>(`${this.apiUrl}/GetAdmin`, credentials, { headers }).pipe(
      tap(response => {
        if (response) {
          console.log(response);
          if (response.result === 'Admin Login Successfully') {
            sessionStorage.setItem('AdminDetail', JSON.stringify(response));
          } else {
            alert(response.result);
          }
        }
      }),
      catchError(error => {
        console.error('Login failed', error);
        return throwError(() => new Error('Login failed. Please check your credentials and try again.'));
      })
    );
  }

  addProduct(endpoint: string, data:any):Observable<any>{
    const headers = this.createAuthorizationHeader();

    return this.http.post<any>(`${this.apiUrl}/InsertProduct`, data, { headers }).pipe(
      tap(response => {
        if (response) {
          console.log(response);
          if (response.result === 'Admin Login Successfully') {
            sessionStorage.setItem('AdminDetail', JSON.stringify(response));
          } else {
            alert(response.result);
          }
        }
      }),
      catchError(error => {
        console.error('Login failed', error);
        return throwError(() => new Error('Login failed. Please check your credentials and try again.'));
      })
    );
  }
}
