import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {OnDestroyMixin, untilComponentDestroyed} from '@w11k/ngx-componentdestroyed';
import {AuthenticationService} from '../core/services/authorization/authentication.service';
import {LoggedUser} from '../shared/models/loggedUser';
import {ToastrService} from 'ngx-toastr';
import {Notification} from '../shared/models/notification';
import {Router} from '@angular/router';
import {UserStateService} from '../core/services/user/user-state.service';
import {NgxPubSubService} from '@pscoped/ngx-pub-sub';

@Component({
  templateUrl: './login.component.html'
})
export class LoginComponent extends OnDestroyMixin implements OnInit {
  public form: FormGroup;
  notification: Notification;

  public get field() {
    return this.form.controls;
  }

  constructor (
    private authenticationService: AuthenticationService,
    private notificationService: ToastrService,
    private userStateService: UserStateService,
    private pubSub: NgxPubSubService,
    protected router: Router,
    private fb: FormBuilder
  ) {
    super();
  }

  ngOnInit() {
    this.notification = new Notification(this.notificationService);
    this.form = this.fb.group({
      login: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login() {
    this.authenticationService.authenticate(this.field.login.value, this.field.password.value)
      .pipe(untilComponentDestroyed(this))
      .subscribe((loggedUser) => {
        if (loggedUser.status === 0) {
          const user = new LoggedUser();
          user.initialize(loggedUser.user);
          localStorage.setItem('currentUser',  JSON.stringify(user));
          this.userStateService.refreshUserData();
          this.pubSub.publishEvent('user_state_changed');
          this.router.navigate(['/product/']).then();
        } else {
          this.notification.error('User not exists');
        }
      });
  }
}

