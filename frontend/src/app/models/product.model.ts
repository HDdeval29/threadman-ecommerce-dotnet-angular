export interface Product {
    id: number;
    productId : number;
    name: string;
    description: string;
    price: number;
    category:  string;    
    subCategory: string; 
    imageUrl: string; 
    Brand:string;
    // size: string; 
    size: string[]; // Ensure size is an array of strings
    color: string; 
    reviews: Review[]; 
    stockQuantity:string;
  }
  

  export interface Review {
    user: string;
    rating: number;
    comment: string;
  }
  
  export interface ProductFilters {
    id: number;
    name: string;
    category: string;
    size: string;
    type: string;
    brand: string;
    fabric: string;
    price: number; // Ensure consistency here
  }
 
// export interface CartItem {
//   cartItemId: number;  // Unique identifier for the cart item (set by the backend)
//   userId: number;      // ID of the user who owns the cart item
//   productId: number;   // ID of the product being added to the cart
//   quantity: number;     // Quantity of the product in the cart
//   size: string;         // Selected size for the product
//   productName: string;  // Name of the product
//   productPrice: number; // Price of the product
//   productImageUrl: string; // Image URL of the product
// }


  // src/app/models/order.model.ts
export interface Order {
  orderId: string;        // Custom OrderID format
  userId: number;
  orderDate: Date;
  totalAmount: number;
  paymentStatus: string;
  shippingAddress: string;
}
