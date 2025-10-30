import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders  } from '@angular/common/http';
import { AuthService } from './auth/auth.service';
import { Router } from '@angular/router';
import { Observable ,throwError } from 'rxjs';
//import { Category } from './models/Master-data.model';

@Injectable({
  providedIn: 'root'
})
export class MasterDataService {

  Token:any;

  constructor(private http: HttpClient, private authService: AuthService,private router:Router) {
    let token:any=localStorage.getItem('authToken')
    this.Token = JSON.parse(token)
    }

  // Create authorization header with token
  private createAuthorizationHeader(): HttpHeaders {
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${this.Token}`);
    return headers;
  }

  private apiUrl = 'https://localhost:44321/Master';

  // const headers = new HttpHeaders({
  //   'Authorization': `Bearer ${this.Token}`,
  //   'Content-Type': 'application/json'
  // });

  getCategories(): Observable<string[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<string[]>(`${this.apiUrl}/GetCategoryList`,{headers});
  }

  getBrands(): Observable<string[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<string[]>(`${this.apiUrl}/GetBrandsList`,{headers});
  }

  getSizes(): Observable<string[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<string[]>(`${this.apiUrl}/GetSizesList`,{headers});
  }

  getFitTypes(): Observable<string[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<string[]>(`${this.apiUrl}/GetFitTypesList`,{headers});
  }

  getColors(): Observable<string[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<string[]>(`${this.apiUrl}/GetColoursList`,{headers});
  }
}
