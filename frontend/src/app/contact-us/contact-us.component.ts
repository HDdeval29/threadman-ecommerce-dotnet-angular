import { Component } from '@angular/core';

@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrls: ['./contact-us.component.css']
})
export class ContactUsComponent {

  name: string = '';
  email: string = '';
  message: string = '';

  onSubmit() {
    
    console.log('Form submitted:', { name: this.name, email: this.email, message: this.message });
    
    this.name = '';
    this.email = '';
    this.message = '';
  }
}
