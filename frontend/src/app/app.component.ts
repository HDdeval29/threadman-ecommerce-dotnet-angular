import { Component } from '@angular/core';
import { StoreApiService } from './store-api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'eStore';

  constructor(private storeApiService: StoreApiService) { }
  
}
