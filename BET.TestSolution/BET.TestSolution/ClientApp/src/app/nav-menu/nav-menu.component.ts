import {ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit} from '@angular/core';
import {UserStateService} from '../core/services/user/user-state.service';
import {OnDestroyMixin, untilComponentDestroyed} from '@w11k/ngx-componentdestroyed';
import {LoggedUser} from '../shared/models/loggedUser';
import {NgxPubSubService} from '@pscoped/ngx-pub-sub';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent extends OnDestroyMixin implements OnInit, OnDestroy {
  isExpanded = false;
  public loggedUser: LoggedUser;
  private userStateChangedEvent: Subscription;

  constructor (
    private cdr: ChangeDetectorRef,
    private pubSub: NgxPubSubService,
    private userStateService: UserStateService
  ) {
    super();
  }

  ngOnDestroy() {
    if (this.userStateChangedEvent) {
      this.userStateChangedEvent.unsubscribe();
    }
  }

  ngOnInit() {
    if (this.userStateService.currentUser$ == null) {
      this.userStateService.refreshUserData();
      this.loggedUser = this.userStateService.currentUserValue;
      this.cdr.detectChanges();
    }
    this.userStateChangedEvent = this.pubSub.subscribe('user_state_changed', () => {
      this.loggedUser = this.userStateService.currentUserValue;
      this.cdr.detectChanges();
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
