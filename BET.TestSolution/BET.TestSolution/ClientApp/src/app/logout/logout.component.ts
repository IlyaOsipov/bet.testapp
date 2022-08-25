import {Component, OnInit} from '@angular/core';
import {OnDestroyMixin} from '@w11k/ngx-componentdestroyed';
import {UserStateService} from '../core/services/user/user-state.service';
import {NgxPubSubService} from '@pscoped/ngx-pub-sub';

@Component({
  templateUrl: './logout.component.html'
})
export class LogoutComponent extends OnDestroyMixin implements OnInit {
  constructor (private pubSub: NgxPubSubService, private userStateService: UserStateService) {
    super();
  }

  ngOnInit() {
    this.userStateService.logout();
    this.pubSub.publishEvent('user_state_changed');
  }
}

