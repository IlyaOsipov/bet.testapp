import {ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, OnDestroy, OnInit, ViewChild} from '@angular/core';
import { Product } from '../shared/models/product';
import { ProductService } from '../core/services/product/product.service';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import {Sort} from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import {BehaviorSubject, Subscription} from 'rxjs';
import {ItemActionType, SortColumnDirection, SortDirection} from '../shared/tableSettings/sortDirection';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {ToastrService} from 'ngx-toastr';
import {Notification} from '../shared/models/notification';
import {UserStateService} from '../core/services/user/user-state.service';
import {LoggedUser} from '../shared/models/loggedUser';
import {NgxPubSubService} from '@pscoped/ngx-pub-sub';
import {CartService} from "../core/services/cart/cart.service";
import {CartStorage} from "../core/services/storage/cart.storage";
import {AddToCartDialogComponent} from "./action-dlg/add-to-cart-item-dialog.component";
import {Cart} from "../shared/models/cart";

@Component({
  selector: 'app-product',
  styleUrls: ['product.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './product.component.html'
})
export class ProductComponent extends CartStorage implements OnInit, OnDestroy {
  public products: Product[];
  public sort: SortColumnDirection;
  public loggedUser: LoggedUser;
  private cartChangedEvent: Subscription;
  notification: Notification;
  displayedColumns: string[] = ['name', 'image', 'price', 'quantity', 'action'];
  dataSource$: BehaviorSubject<Product[]> = new BehaviorSubject<Product[]>([]);
  dialogRef: MatDialogRef<AddToCartDialogComponent>;
  cart: Cart = undefined;

  constructor (
    private productService: ProductService,
    private cartService: CartService,
    private cdr: ChangeDetectorRef,
    private notificationService: ToastrService,
    private pubSub: NgxPubSubService,
    private dialog: MatDialog,
    userStateService: UserStateService,
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
    this.fillTable();
  }

  getDefaultColumns(): string[] {
   return ['name', 'image', 'price', 'quantity', 'action'];
  }

  fillTable() {
    const productsData$ = this.productService.getAllProducts();
    productsData$.pipe(untilComponentDestroyed(this)).subscribe((res: Product[]) => {
      this.products = res;
      this.dataSource$.next(this.products);
      this.cdr.detectChanges();
    });
  }
  onAddToCartClick(el:Product) {
    this.dialogRef = this.dialog.open(AddToCartDialogComponent, {
      width: '600px',
      autoFocus: false,
      data: {
        actionType: ItemActionType.add,
        product: el
      }
    });

    this.dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.cart.items.push(result);
        this.onCartUpdate(this.cart);
      }
    });
  }

  sortData(sort: Sort) {
    const data = this.products.slice();
    if (!sort.active || sort.direction === '') {
      this.products = data;
      return;
    }
    this.products = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'name':
          return this.compare(a.name, b.name, isAsc);
        case 'id':
          return this.compare(a.id, b.id, isAsc);
        case 'image':
          return this.compare(a.image, b.image, isAsc);
        case 'price':
          return this.compare(a.price, b.price, isAsc);
        case 'quantity':
          return this.compare(a.quantity, b.quantity, isAsc);
        default:
          return 0;
      }
    });
    this.dataSource$.next(this.products);
    this.cdr.detectChanges();
  }

  private compare(a: number | string, b: number | string, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }
}
