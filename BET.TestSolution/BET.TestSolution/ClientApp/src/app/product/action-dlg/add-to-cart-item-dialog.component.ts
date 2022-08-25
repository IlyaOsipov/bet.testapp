import {ChangeDetectionStrategy, Component, Inject, OnInit} from '@angular/core';
import {Product, ProductProfile} from '../../shared/models/product';
import {OnDestroyMixin} from '@w11k/ngx-componentdestroyed';
import {ItemActionType} from '../../shared/tableSettings/sortDirection';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {CartItem} from "../../shared/models/cart";
import {CartService} from "../../core/services/cart/cart.service";

@Component({
  styleUrls: ['add-to-cart-item-dialog.component.scss'],
  templateUrl: 'add-to-cart-item-dialog.component.html'
})
export class AddToCartDialogComponent extends OnDestroyMixin implements OnInit {
  public form: FormGroup;
  public сartItem: CartItem = new CartItem();

  public get field() {
    return this.form.controls;
  }

  constructor (
    private cartService: CartService,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public dialogData: { actionType: ItemActionType; product: Product },
    private dialogRef: MatDialogRef<AddToCartDialogComponent>
  ) {
    super();
  }

  ngOnInit() {
    if (!this.dialogData.actionType) {
      console.error('Please provide actionType for the dialog');
    }
    this.сartItem.id = this.dialogData.product.id == null ? 0 : this.dialogData.product.id;

    this.form = this.fb.group({
      amount: [1, [Validators.required, Validators.min(1), Validators.max( this.dialogData.product.quantity)]],
    });
    this.fillForm(this.сartItem);
  }

  fillForm(сartItem: CartItem): void {
    this.form.patchValue({
      quantity: сartItem.quantity
    });
  }

  public confirm() {
    this.сartItem.productId = this.dialogData.product.id;
    this.сartItem.name = this.dialogData.product.name;
    this.сartItem.image = this.dialogData.product.image;
    this.сartItem.price = this.dialogData.product.price;
    this.сartItem.quantity = this.field.quantity.value;
    this.dialogRef.close(this.сartItem);
  }
}
