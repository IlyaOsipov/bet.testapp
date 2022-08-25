import {OnDestroyMixin} from "@w11k/ngx-componentdestroyed";
import {BehaviorSubject} from "rxjs";
import {Cart, CartItem} from "../../../shared/models/cart";
import {UserStateService} from "../user/user-state.service";

export class CartStorage extends OnDestroyMixin{
  cartSubject = new BehaviorSubject<Cart | null>(null);

  constructor(private userStateService: UserStateService) {
    super();
    this.getCurrentCart();
  }

  public get cart$() {
    return this.cartSubject.asObservable();
  }

  onCartUpdate(data: Cart) {
    this.cartSubject.next(data);
  }

  private getCurrentCart() {
    let cart = JSON.parse(localStorage.getItem('cartData'));
    if (!cart) {
      cart = {
        id: 0,
        totals: 0,
        items: [],
        userId: this.userStateService.currentUserValue.id
      };
    }
    this.onCartUpdate(cart);
  }
}
