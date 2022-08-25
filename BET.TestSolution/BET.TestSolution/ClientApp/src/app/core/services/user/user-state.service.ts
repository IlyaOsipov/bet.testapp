import {BehaviorSubject, Observable, Subscription} from 'rxjs';
import {OnDestroyMixin} from '@w11k/ngx-componentdestroyed';
import {LoggedUser} from '../../../shared/models/loggedUser';
import {ChangeDetectorRef} from '@angular/core';

export class UserStateService extends OnDestroyMixin {
  public currentUser$: Observable<LoggedUser>;
  private currentUserSubject = new BehaviorSubject<LoggedUser|null> (null);

  constructor() {
    super();
  }

  public get currentUserValue(): LoggedUser {
    return this.currentUserSubject.value;
  }

  public refreshUserData() {
    this.currentUserSubject = new BehaviorSubject<LoggedUser>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser$ = this.currentUserSubject.asObservable();
  }

  logout() {
    localStorage.clear();
    this.currentUserSubject.next(null);
    this.refreshUserData();
  }
}
