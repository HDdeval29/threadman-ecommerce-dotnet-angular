import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StoreApiService } from 'src/app/store-api.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-customer-signup',
  templateUrl: './customer-signup.component.html',
  styleUrls: ['./customer-signup.component.css']
})
export class CustomerSignupComponent {

  signupForm: FormGroup;

  constructor (private fb: FormBuilder, private storeApiService: StoreApiService, private router: Router) {
    this.signupForm = this.fb.group({
      userName: ['', Validators.required],
      fullName: ['', Validators.required],
      mobileNo: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]], 
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.signupForm.valid) {
      // Send the form data to your backend or service
      let SignUpdata = { ...this.signupForm.value }

      this.storeApiService.userSignup('Signup-endpoint', SignUpdata).subscribe({
        next: (response) => {
          //alert("Registration successfully Please Login")
          Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: 'Registration successful! Please log in.',
            showConfirmButton: false,
            timer: 2000,
            toast: true, // Ensure it's a toast notification
            customClass: {
              container: 'custom-toast-container' // Apply custom CSS class
            }

          }).then(() => {
            // Redirect to login after the toast has been shown
            this.router.navigateByUrl('/auth/Userlogin');
          });
          
          // setTimeout(() => {
          //   this.router.navigateByUrl('/auth/Userlogin'); // Redirect to login on successful signup
          // }, 2000); // Delay for demonstration; adjust as needed
        },
        error: err => {console.error('Signup failed', err);
        // Optionally show an error toast notification here
        Swal.fire({
          position: 'top-end',
          icon: 'error',
          title: 'Signup failed! Please try again.',
          showConfirmButton: false,
          timer: 2000,
          toast: true, // Ensure it's a toast notification
          customClass: {
              container: 'custom-toast-container' // Apply custom CSS class
          }
        });
      }
      });
    }
  }
}
