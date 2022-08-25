import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import { untilComponentDestroyed } from '@w11k/ngx-componentdestroyed';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import {BehaviorSubject} from 'rxjs';
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
export class CartComponent extends CartStorage implements OnInit {
  public items: CartItem[];
  public sort: SortColumnDirection;
  notification: Notification;
  displayedColumns: string[] = ['id', 'name', 'image', 'price', 'amount', 'totals'];
  dataSource$: BehaviorSubject<CartItem[]> = new BehaviorSubject<CartItem[]>([]);

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

  ngOnInit() {
    this.notification = new Notification(this.notificationService);
    this.displayedColumns = this.getDefaultColumns();
    this.fillTable();
  }

  getDefaultColumns(): string[] {
   return ['id', 'name', 'image', 'price', 'amount', 'totals'];
  }

  onDeleteClick(el:Product) {}

  fillTable() {
    this.cart$.pipe(untilComponentDestroyed(this)).subscribe((res: Cart) => {
      this.items = res.items;
      this.dataSource$.next(this.items);
      this.cdr.detectChanges();
    });
  }
}
