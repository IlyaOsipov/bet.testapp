import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {MatToolbarModule} from '@angular/material/toolbar';
import {CommonModule} from '@angular/common';
import {MatCommonModule} from '@angular/material/core';
import {NgLetDirectiveModule} from './../shared/directives/ng-let/ng-let-directive.module';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {LogoutComponent} from './logout.component';
import {AuthenticationService} from '../core/services/authorization/authentication.service';

@NgModule({
  declarations: [
    LogoutComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatCommonModule,
    MatToolbarModule,
    NgLetDirectiveModule,
    FormsModule
  ],
  providers: [AuthenticationService]
})
export class LogoutModule { }
