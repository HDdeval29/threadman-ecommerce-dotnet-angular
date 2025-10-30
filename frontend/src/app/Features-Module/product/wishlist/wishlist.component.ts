import { Component } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from '../../../models/product.model';
@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent {
  wishlist: Product[] = [];
  userId: number = 101; // Replace with logic to get the logged-in user's ID
  isLoading = true;
  errorMessage: string | null = null;
  
  loaderforpayment:boolean = false;
  LoaderVal = "";

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.loadWishlist();
  }

  loadWishlist(): void {
    //this.isLoading = true;
    this.loaderforpayment = true;
    this.LoaderVal = "Please Wait....";
    setTimeout(() => {
      this.productService.getWishlist(this.userId).subscribe({
        next: (data: Product[]) => {
          this.wishlist = data;
          //this.isLoading = false;
          this.loaderforpayment = false;
        },
        error: (err) => {
          this.errorMessage = 'Failed to load wishlist';
          this.isLoading = false;
          console.error('Error loading wishlist', err);
        }
      });
    }, 5000);
  }

  removeFromWishlist(productId: number): void {
    this.productService.removeFromWishlist(this.userId, productId).subscribe({
      next: () => {
        this.wishlist = this.wishlist.filter(product => product.id !== productId);
        alert('Product removed from wishlist');
      },
      error: (err) => {
        console.error('Error removing product from wishlist', err);
      }
    });
  }
}
