import { HttpClient } from '@angular/common/http';
import { Component, OnInit,ViewChild  } from '@angular/core';
import {FormControl, FormsModule, ReactiveFormsModule} from '@angular/forms';
import { StoreApiService } from '../../store-api.service';
import { Router } from '@angular/router';
import { BehaviourServiceService } from 'src/app/behaviour-service.service';
import { OrderService } from '../../Features-Module/order/order.service';
import { Observable, of } from 'rxjs';
import {map, startWith,switchMap } from 'rxjs/operators';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggedIn = false;
  User:any;
  cartCount: number = 0;
  cartItems: any[] = [];

  myControl = new FormControl();
  filteredOptions: Observable<any[]> = of([]);

  filteredProducts: string[] = [];

  constructor(private storeApiService: StoreApiService, private router:Router, private _bs:BehaviourServiceService,private orderService: OrderService) {
    this.User = storeApiService.userIsAuthenticated();
   }

  ngOnInit() {
    debugger
     this.User = this._bs.GetLocalUser()

    this._bs.returnLogin().subscribe((res: any) => {
      this.User = this._bs.GetLocalUser()
    });

    this.filteredOptions = this.myControl.valueChanges.pipe(
      startWith(''),
      switchMap(value => {
        console.log('Value changed:', value); // Check what value is being emitted
        return this.filter(value);
      })
    );

    this.myControl.valueChanges.subscribe((searchValue) => {
      this.performSearch(searchValue);
      this._bs.updateSearchTerm(searchValue);
    });

    this.orderService.UpdateCartCount(this.User.userId).subscribe({
      next: (cartCount: any) => {
            if (cartCount != '') {
              this.cartCount = cartCount;
            }
          },
          error: (error) => {
            console.error('Error fetching cart items:', error);
          }
    });
  }

  filter(value: string): Observable<any[]> {
    console.log('Searching for:', value);
    if (value.length < 2) {
      return of([]); // Return an empty observable for short input
    }
    return this.storeApiService.getSuggestions(value);
  }

//   performSearch(searchTerms: any): void {
//      if (searchTerms.length >= 3) {
//       this.storeApiService.getProductBySearchBar(searchTerms).subscribe(productNames => {
//         console.log('Filtered Product Names:', productNames);
//         this.router.navigate(['/search-results'], { queryParams: { query: searchTerms } })
//       }, error => {
//         console.error('Error fetching product names:', error);
//       });
//     } else {
//       // this.searchbarComponent.updateProductNames([]);
//     }

// }

private performSearch(searchTerms: string): void {
  if (searchTerms.length >= 3) {
    this.storeApiService.getProductBySearchBar(searchTerms).subscribe(
      productNames => {
        console.log('Filtered Product Names:', productNames);
        this._bs.updateSearchResults(productNames);
        this.router.navigate(['/search-results']);
      },
      error => {
        console.error('Error fetching product names:', error);
        this._bs.updateSearchResults([]);
      }
    );
  } else {
    this._bs.updateSearchResults([]);
  }
}

  logout(): void {
    this._bs.logout();
    this.router.navigateByUrl('/')
  }

  goToCart() {
    this.router.navigate(['/order/cart']);
  }
}
