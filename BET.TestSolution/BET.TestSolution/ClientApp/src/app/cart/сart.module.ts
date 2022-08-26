import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {ProductService} from './../core/services/product/product.service';
import {MatTableModule} from '@angular/material/table';
import {MatSortModule} from '@angular/material/sort';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatToolbarModule} from '@angular/material/toolbar';
import {CommonModule} from '@angular/common';
import {MatCommonModule} from '@angular/material/core';
import {NgLetDirectiveModule} from './../shared/directives/ng-let/ng-let-directive.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatDialogModule, MatDialogRef} from '@angular/material/dialog';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {CartComponent} from "./сart.component";
import {CartDeleteDialogComponent} from "./confirm-dlg/cart-delete-dialog.component";
import {CartService} from "../core/services/cart/cart.service";

@NgModule({
  declarations: [
    CartComponent,
    CartDeleteDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatTableModule,
    MatCommonModule,
    MatSortModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatToolbarModule,
    NgbModule,
    NgLetDirectiveModule,
    FormsModule
  ],
  exports: [
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatTableModule,
    MatCommonModule,
    MatSortModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatToolbarModule],
  providers: [CartService, ProductService]
})
export class CartModule { }
