import {Component, Inject, OnInit} from '@angular/core';
import {OnDestroyMixin} from '@w11k/ngx-componentdestroyed';
import {ItemActionType} from '../../shared/tableSettings/sortDirection';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {CartItem} from "../../shared/models/cart";

@Component({
  styleUrls: ['cart-delete-dialog.component.scss'],
  templateUrl: 'cart-delete-dialog.component.html'
})
export class CartDeleteDialogComponent extends OnDestroyMixin implements OnInit {
  productName: string;
  constructor (
    @Inject(MAT_DIALOG_DATA) public dialogData: { actionType: ItemActionType; product: CartItem },
    private dialogRef: MatDialogRef<CartDeleteDialogComponent>
  ) {
    super();
  }

  ngOnInit() {
    if (!this.dialogData.actionType) {
      console.error('Please provide actionType for the dialog');
    }
    this.productName = this.dialogData.product.name;
  }
}
