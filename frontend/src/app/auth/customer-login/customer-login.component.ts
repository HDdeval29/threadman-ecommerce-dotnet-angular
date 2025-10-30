import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StoreApiService } from '../../store-api.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-customer-login',
  templateUrl: './customer-login.component.html',
  styleUrls: ['./customer-login.component.css']
})


export class CustomerLoginComponent {
  errorMessage: string | null = null;
  loginForm:FormGroup;

loaderforpayment:boolean = false;
LoaderVal = "";


  constructor(private _fb:FormBuilder, private http:HttpClient,private storeApiService: StoreApiService,private router: Router){
    this.loginForm=this._fb.group({
      userName:['',Validators.required],
      password:['',Validators.required]
    })

  }
  get V(){
   return this.loginForm.controls
  }
  submmitted:boolean=false;

  onSubmit(): void {
     this.submmitted = true;
    this.loaderforpayment = true;
    this.LoaderVal = "Please Wait....";
    
    // Stop if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    // Access form value
    console.log('Form Submitted', this.loginForm.value);

      let data={ ...this.loginForm.value }
      
      setTimeout(() => {
          this.storeApiService.userlogin('login-endpoint', data).subscribe({
            next: (response) => {
             ///this.loaderforpayment = false;
             this.loaderforpayment = false;
              console.log('Login successful', response);
              // Redirect or perform other actions after login
              this.router.navigate(['/home']); 
            },
            error: (error) => {
              // Handle error
              this.errorMessage = error.message;
              console.error('An error occurred:', error.message);
            }
          });

       }, 10000);
  }

  onlogin(){

  }
}
