import { Component,OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { StoreApiService } from './../store-api.service';
import { BehaviourServiceService } from 'src/app/behaviour-service.service';
import { Observable,of } from 'rxjs';

@Component({
  selector: 'app-searchbar-productlist',
  templateUrl: './searchbar-productlist.component.html',
  styleUrls: ['./searchbar-productlist.component.css']
})
export class SearchbarProductlistComponent implements OnInit {

  //searchResults: any[] = [];
  query: string | null = '';
  searchResults: Observable<any[]> = of([]);

  constructor(private route: ActivatedRoute, private storeApiService: StoreApiService,private _bs:BehaviourServiceService) {}

  ngOnInit(): void {
    debugger
    // this.route.queryParams.subscribe(params => {
    //   this.query = params['query'];
    //   if (this.query) {
    //     this.fetchSearchResults(this.query);
    //   }
    // });
    this.searchResults = this._bs.searchResults$;

    this._bs.searchTerm$.subscribe(term => {
      if (term) {
        console.log("term query", term);
        this.query = term;
      }
    });
  }
  // fetchSearchResults(searchTerm: string): void {
  //   this.storeApiService.getProductBySearchBar(searchTerm).subscribe(
  //     results => {
  //       this.searchResults = results;
  //     },
  //     error => {
  //       console.error('Error fetching search results:', error);
  //     }
  //   );
  // }
}
