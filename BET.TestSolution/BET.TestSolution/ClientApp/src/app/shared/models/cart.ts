export class Cart {
  id?: number;
  totals: number;
  items?: CartItem[];
  userId: number;
}

export class CartItem {
  id: number;
  cartId: number;
  image: string;
  name: string;
  productId: number;
  quantity: number;
  totals: number;
  price: number;
}
