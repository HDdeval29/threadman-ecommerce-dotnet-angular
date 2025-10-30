import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css']
})
export class AdminLoginComponent {
  loginForm: FormGroup;
  loginError: string | null = null;

  constructor(private fb: FormBuilder, private router: Router, private adminService : AdminService) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      role: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const { username, password, role } = this.loginForm.value;
      this.adminService.adminlogin({ username, password, role }).subscribe(
        response => {
          // Handle successful login
          console.log('Login successful', response);
          this.router.navigate(['/admin-portal/dashboard']); 
        },
        error => {
          // Handle login error
          this.loginError = 'Invalid username, password, or role.';
        }
      );
    }
  }
}
