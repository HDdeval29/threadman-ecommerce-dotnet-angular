import { Component, OnInit } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from '../../../models/product.model';
import { MasterDataService } from '../../../master-data.service';
import { OrderService } from '../../order/order.service';
//import { CartItem } from '../../../models/product.model';
import { Router } from '@angular/router';
@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})

export class ProductListComponent implements OnInit {

  products: Product[] = [];

  ProductFilters: Product[] = [];
  
  categories1: string[] = ['Men', 'Women', 'Kids'];    // Define available categories
  selectedCategory1: string = 'All';                   // Default to 'All' categories
  isLoading = true;
  errorMessage: string | null = null;
  selectedProduct: Product | null = null; // For modal

  selectedSize1: string = ''; // For size selection
  quantity: number = 1;

   // Sidebar filter variables
  categories:any;
  brands: any= [];
  sizes: any = [];
  fitTypes: any = [];
  colors: any = [];

  searchText: any = '';
  selectedCategory: any = '';
  selectedBrand: any = '';
  selectedSize: any = '';
  selectedFitType: any = '';
  selectedColor: any = '';
  
  constructor(private productService: ProductService,private masterDataService: MasterDataService, private orderService: OrderService,private router:Router) { }

  ngOnInit(): void {
    this.loadFilterOptions();
    this.loadProducts(); //Load all products initially
  }

  loadFilterOptions(): void {
    this.masterDataService.getCategories().subscribe(data => {
      console.log('Categories Response:', data);
      this.categories = data;
    });

    this.masterDataService.getBrands().subscribe(data => this.brands = data);
    this.masterDataService.getSizes().subscribe(data => this.sizes = data);
    this.masterDataService.getFitTypes().subscribe(data => this.fitTypes = data);
    this.masterDataService.getColors().subscribe(data => this.colors = data);
  }

  loadProducts(filters?: any): void {
     this.isLoading = true;
    const endpoint = 'filterProduct-endpoint';
    const loadProducts$ = filters
      ? this.productService.getProductbyFilter(endpoint,filters)
      : this.productService.getAllProduct();

    loadProducts$.subscribe({
      next: (data: Product[]) => {
        this.products = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load products';
        this.isLoading = false;
        console.error('Error loading products', err);
      }
    });
  }


  applyFilters(): void {
    const filters = {

      searchText: this.searchText,
      category: this.selectedCategory,
      brand: this.selectedBrand,
      size: this.selectedSize,
      fitType: this.selectedFitType,
      color: this.selectedColor

      // searchText: this.searchText,
      // category: this.selectedCategory?.categoryName || '',
      // brand: this.selectedBrand?.brandName || '',
      // size: this.selectedSize?.sizeName || '',
      // fitType: this.selectedFitType?.fitTypeName || '',
      // color: this.selectedColor?.colorName || ''
    };

    console.log('Applying filters:', filters);
    this.loadProducts(filters); // Load products based on filters
  }

  clearFilters(): void {
    this.searchText = '';
    this.selectedCategory = '';
    this.selectedBrand = '';
    this.selectedSize = '';
    this.selectedFitType = '';
    this.selectedColor = '';

    this.loadProducts(); // Reload all products with no filters
  }

  selectCategory(category: string): void {
    this.selectedCategory = category;
  }

  selectBrand(brand: string): void {
    this.selectedBrand = brand;
  }

  selectSize(size: string): void {
    this.selectedSize = size;
  }

  selectFitType(fitType: string): void {
    this.selectedFitType = fitType;
  }

  selectColor(color: string): void {
    this.selectedColor = color;
  }

  
  // loadProducts(): void {
  //   this.isLoading = true;
  //   const loadProducts$ = this.selectedCategory === 'All'
  //     ? this.productService.getAllProduct()
  //     : this.productService.getProductsByCategory(this.selectedCategory as 'Men' | 'Women' | 'Kids');

  //   loadProducts$.subscribe({
  //     next: (data: Product[]) => {
  //       this.products = data;
  //       this.isLoading = false;
  //     },
  //     error: (err) => {
  //       this.errorMessage = 'Failed to load products';
  //       this.isLoading = false;
  //       console.error('Error loading products', err);
  //     }
  //   });
  // }
  

  
  onCategoryChange(event: Event): void {
    const target = event.target as HTMLSelectElement | null;
    if (target) {
      this.selectedCategory = target.value;
      this.loadProducts(); // Reload products based on selected category
    }
  }

   openModal(product: Product): void {
     this.selectedProduct = product;
     this.selectedSize1 = ''; // Reset selected size
     this.quantity = 1;
   }


  showProductDetails(productId: number): void {
     this.productService.getProductById(productId).subscribe(product => {
       this.selectedProduct = product;
     });
  }

  closeModal(): void {
    this.selectedProduct = null;
  }

  addToWishlist(product: Product): void {
    const userId = 101; // Replace with actual logic to get the logged-in user's ID


    this.productService.addToWishlist(userId, product.productId).subscribe({
      next: () => {
        alert('Product added to wishlist');
        this.closeModal();
      },
      error: (err) => {
        console.error('Error adding product to wishlist', err);
      }
    });
  }

  addToCart(product: Product): void {
    // Validate size selection
    if (!this.selectedSize1) {
      alert('Please select a size.');
      return;
    }
     var user_details:any = sessionStorage.getItem('UserDetail')
    var User = JSON.parse(user_details)
    // Create the cart item object
    const cartItem: any = {
      cartItemId: 0, // This will be set by the backend
      userId: User.userId, // Replace with the actual user ID
      productId: product.productId, // Assuming product has an ID
      quantity: this.quantity,
      size: this.selectedSize,
      productName: product.name, // Assuming Product has a name
      productPrice: product.price, // Assuming Product has a price
      productImageUrl: product.imageUrl // Assuming Product has an image URL
    };

    // Call the service to add the item to the cart
    this.orderService.addToCart(cartItem).subscribe(() => {
      this.orderService.UpdateCartCount(User.userId);
      alert('Added to cart!');
      // Optionally refresh cart items or handle further logic here
    }, error => {
      console.error('Error adding to cart', error);
      alert('Failed to add to cart.');
    });
  }

  buyNow(product: Product): void {
    //  this.productService.buyNow(product.id, this.quantity).subscribe(() => {
    //    alert('Product purchased');
    //    this.closeModal();
    //  });
    this.router.navigate(['/order/buy-now']);
  }
}
