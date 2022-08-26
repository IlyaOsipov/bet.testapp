import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import {UserService} from './core/services/user/user.service';
import {AppRoutingModule} from './app.routing.module';
import {CommonModule} from '@angular/common';
import {NgLetDirectiveModule} from './shared/directives/ng-let/ng-let-directive.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {ProductModule} from './product/product.module';
import {ToastrModule} from 'ngx-toastr';
import {LoginModule} from './login/login.module';
import {LogoutModule} from './logout/logout.module';
import {UserStateService} from './core/services/user/user-state.service';
import {NgxPubSubModule} from '@pscoped/ngx-pub-sub';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {CartModule} from "./cart/сart.module";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    ProductModule,
    CartModule,
    LoginModule,
    LogoutModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot({ timeOut: 3500, enableHtml: true }),
    NgLetDirectiveModule,
    NgxPubSubModule,
    NgbModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [UserService, UserStateService],
  bootstrap: [AppComponent]
})
export class AppModule { }
