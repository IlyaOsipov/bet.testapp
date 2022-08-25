import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ProductComponent } from './product/product.component';
import { LogoutComponent } from './logout/logout.component';
import {CartComponent} from "./cart/сart.component";

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      {
        path: '',
        redirectTo: '/',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: 'cart',
    component: CartComponent,
    data: { namespace: 'cart' }
  },
  {
    path: 'product',
    component: ProductComponent,
    data: { namespace: 'product' }
  },
  {
    path: 'login',
    component: LoginComponent,
    data: { namespace: 'login' }
  },
  {
    path: 'logout',
    component: LogoutComponent,
    data: { namespace: 'logout' }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
