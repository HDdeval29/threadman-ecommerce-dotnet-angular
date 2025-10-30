import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ProductService } from '../product.service';
import { Product } from '../../../models/product.model';
import { Filter } from '../../../models/filter.model';
import { HttpClient } from '@angular/common/http';
import { MasterDataService } from '../../../master-data.service';
//import { Category } from '../../../models/Master-data.model';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {

 // @Output() filtersChanged = new EventEmitter<any>();

  // searchText: string = '';
  // selectedCategory: string = '';
  // selectedBrand: string = '';
  // selectedSize: string = '';
  // selectedFitType: string = '';
  // selectedColor: string = '';

  //categories: Category[] = []; // Use the Category interface
  //categories:string[] = [];
  //brands: string[] = [];
  //sizes: string[] = [];
  //fitTypes: string[] = [];
  //colors: string[] = [];
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

  constructor(private http: HttpClient,private masterDataService: MasterDataService, private productService: ProductService) {}

  ngOnInit() {
    this.loadFilterOptions();
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
  
  applyFilters(): void {
    const filters = this.getSelectedFilters();
    const endpoint = 'filterProduct-endpoint';
    console.log('Applying filters:', filters);
    this.productService.getProductbyFilter(endpoint, filters).subscribe(
      response => {
        // Handle the response with filtered products
        console.log('Filtered products:', response);
        // You might want to store this in a service or another component
      }, error => {
        console.error('Error filtering products:', error);
      });
  }
  // applyFilters(): void {
  //   const filters = this.getSelectedFilters();
  //   console.log('Applying filters:', filters);
  //   this.filtersChanged.emit(filters); // Emit the filters to parent component
  // }
  getSelectedFilters() {
    return {
      // searchText: this.searchText,
      // category: this.selectedCategory,
      // brand: this.selectedBrand,
      // size: this.selectedSize,
      // fitType: this.selectedFitType,
      // color: this.selectedColor

      searchText: this.searchText,
      category: this.selectedCategory?.categoryName || '',
      brand: this.selectedBrand?.brandName || '',
      size: this.selectedSize?.sizeName || '',
      fitType: this.selectedFitType?.fitTypeName || '',
      color: this.selectedColor?.colorName || ''
    };
  }
}
