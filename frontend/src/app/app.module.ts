import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './Layouts/header/header.component';
import { FooterComponent } from './Layouts/footer/footer.component'
import { AuthModule } from './auth/auth.module';
import { LayoutComponent } from './Layouts/layout/layout.component';
import { HttpClientModule } from '@angular/common/http';
import { StoreApiService } from './store-api.service';
import { ProductModule } from './Features-Module/product/product.module';
import { AdminPortalModule } from './admin-portal/admin-portal.module';
import { OrderModule } from './Features-Module/order/order.module';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { EditProfileComponent } from './User/edit-profile/edit-profile.component';
import { ViewProfileComponent } from './User/view-profile/view-profile.component';
import { AccountComponent } from './User/account/account.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { SearchbarProductlistComponent } from './searchbar-productlist/searchbar-productlist.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    LayoutComponent,
    AboutUsComponent,
    ContactUsComponent,
    EditProfileComponent,
    ViewProfileComponent,
    AccountComponent,
    SearchbarProductlistComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    AuthModule,
    ProductModule,
    OrderModule,
    AdminPortalModule ,
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatIconModule,
    MatToolbarModule
   ],
  providers: [StoreApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
