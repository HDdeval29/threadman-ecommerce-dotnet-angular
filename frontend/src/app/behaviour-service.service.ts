import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BehaviourServiceService {
  public LoggedIn = new BehaviorSubject<boolean>(false);
  public CartItem = new BehaviorSubject<number>(0)

  private searchTermSubject = new BehaviorSubject<string>('');
  private searchResultsSubject = new BehaviorSubject<any[]>([]);

  searchTerm$ = this.searchTermSubject.asObservable();
  searchResults$ = this.searchResultsSubject.asObservable();

  constructor() { }

  login(user:any): void {
     this.LoggedIn.next(true);
  }

  GetLocalUser(){
    var user_details:any = sessionStorage.getItem('UserDetail')
    var User = JSON.parse(user_details)
     return User
  }

  returnLogin(){
   return this.LoggedIn.asObservable()
  }

  logout(): void {
    sessionStorage.removeItem('UserDetail')
    this.LoggedIn.next(false);
  }

  updateSearchTerm(term: string) {
    this.searchTermSubject.next(term);
  }

  updateSearchResults(results: any[]) {
    this.searchResultsSubject.next(results);
  }

}
