import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class PaymentService {
  private api = 'https://localhost:44321/payment';  // backend API

  constructor(private http: HttpClient) {}

  createOrder(amount: number) {
    return this.http.post<{orderId:string, amount:number, currency:string}>(`${this.api}/create-order`, { amount });
  }

  verifyPayment(payload: any) {
    return this.http.post(`${this.api}/verify`, payload);
  }
}
