import { Component , OnInit} from '@angular/core';
import { NgChartsModule } from 'ng2-charts';
import { ChartOptions, ChartType, ChartData } from 'chart.js';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MasterDataService } from '../../master-data.service';
import { AdminService } from '../admin.service';
import * as $ from 'jquery';
import 'bootstrap';
@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit{

  productForm: FormGroup;
  // categories = any;
  // subcategories = [];
  // brands = any= [];
  // sizes = [];
  // colors = [];
  // fitTypes = [];

  categories:any; 
  subcategories = [
    { id: '1', name: 'Clothing' }, 
    { id: '2', name: 'Accessories' }
  ];
  brands: any= [];
  sizes: any = [];
  fitTypes: any = [];
  colors: any = [];

  selectedCategory: any = '';

  constructor(private fb: FormBuilder, private masterDataService: MasterDataService, private adminService:AdminService) {
    this.productForm = this.fb.group({
      name: [''],
      description: [''],
      price: [''],
      category: [''],
      subcategory: [''],
      imageurl: [''],
      brandId: [''],
      sizeId: [''],
      colorId: [''],
      fitTypeId: ['']
    });
  }

  ngOnInit(): void {
    this.loadDropdowns();
  }

  // this.masterDataService.getBrands().subscribe(data => this.brands = data);

  loadDropdowns(): void {
    this.masterDataService.getCategories().subscribe(data => {
      console.log('loadDropdowns Categories Response:', data);
      this.categories = data;
    });
    //this.masterDataService.getSubcategories().subscribe(data => this.subcategories = data);
    this.masterDataService.getBrands().subscribe(data => this.brands = data);
    this.masterDataService.getSizes().subscribe(data => this.sizes = data);
    this.masterDataService.getColors().subscribe(data => this.colors = data);
    this.masterDataService.getFitTypes().subscribe(data => this.fitTypes = data);
  }
  selectCategory(category: string): void {
    this.selectedCategory = category;
  }
  selectSubCategory(subcategory: string): void {
    this.selectedCategory = subcategory;
  }
  submmitted:boolean=false;

  onSubmit(): void {
     
    // Stop if form is invalid
    if (this.productForm.invalid) {
      return;
    }
    else if (this.productForm.valid) {
      console.log(this.productForm.controls)
      let data = { ...this.productForm.value }
      
      this.adminService.addProduct('Product-endpoint',data).subscribe(response => {
        // Handle success response
        alert('Product added successfully!');
        this.productForm.reset();
        // Optionally close the modal here
        $('#addProductModal').modal('hide');
      }, error => {
        // Handle error response
        alert('An error occurred while adding the product.');
      });
    }
  }

  
  //Chart data
  public barChartOptions: any = {
    responsive: true,
    plugins: {
      legend: {
        display: true
      }
    },
    scales: {
      x: {
        beginAtZero: true
      }
    }
  };
  public barChartLabels: string[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
  public barChartData: any = {
    labels: this.barChartLabels,
    datasets: [
      { data: [12, 19, 3, 5, 2, 3, 7], label: 'Sales',
        backgroundColor: 'rgb(192, 240, 216)', 
        borderColor: 'rgb(30, 80, 65)', 
        borderWidth: 1
      }
    ]
  };
  public barChartType: 'bar' = 'bar'; // Ensure this matches your chart type
  public barChartLegend: boolean = true;
  public barChartPlugins = []; // Add plugins if needed
}
