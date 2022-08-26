import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit} from '@angular/core';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import {BehaviorSubject, Subscription} from 'rxjs';
import {SortColumnDirection} from '../shared/tableSettings/sortDirection';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {ToastrService} from 'ngx-toastr';
import {Notification} from '../shared/models/notification';
import {UserStateService} from '../core/services/user/user-state.service';
import {NgxPubSubService} from '@pscoped/ngx-pub-sub';
import {Cart, CartItem} from "../shared/models/cart";
import {CartService} from "../core/services/cart/cart.service";
import {CartStorage} from "../core/services/storage/cart.storage";
import {Product} from "../shared/models/product";

@Component({
  selector: 'app-сart',
  styleUrls: ['сart.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './cart.component.html'
})
export class CartComponent extends CartStorage implements OnInit, OnDestroy {
  public items: CartItem[];
  public sort: SortColumnDirection;
  notification: Notification;
  private cartChangedEvent: Subscription;
  displayedColumns: string[] = ['name', 'image', 'price', 'quantity', 'totals', 'action'];
  dataSource$: BehaviorSubject<CartItem[]> = new BehaviorSubject<CartItem[]>([]);
  cart: Cart = undefined;

  constructor (
    userStateService: UserStateService,
    private cartService: CartService,
    private cdr: ChangeDetectorRef,
    private notificationService: ToastrService,
    private pubSub: NgxPubSubService,
    private dialog: MatDialog,
    private _liveAnnouncer: LiveAnnouncer
  ) {
    super(userStateService);
  }

  ngOnDestroy() {
    if (this.cartChangedEvent) {
      this.cartChangedEvent.unsubscribe();
    }
  }

  ngOnInit() {
    this.notification = new Notification(this.notificationService);
    this.cartChangedEvent = this.cartSubject.subscribe(c=>{
      if(c){
        this.cart = c;
      }
    });
    this.displayedColumns = this.getDefaultColumns();
    this.fillTable();
  }

  getDefaultColumns(): string[] {
   return ['name', 'image', 'price', 'quantity', 'totals', 'action'];
  }

  onDeleteClick(el:Product) {}

  fillTable() {
    const productsData$ = this.cartSubject;
    productsData$.pipe(untilComponentDestroyed(this)).subscribe((res: Cart) => {
      this.items = res.items;
      this.dataSource$.next(this.items);
      this.cdr.detectChanges();
    });
  }
}
