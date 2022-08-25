import {LoggedUserResponse} from './loggedUserResponse';

export class LoggedUser {
  id: number;
  fullName: string;
  isAdmin: boolean;
  initialized: boolean;

  initialize(data: LoggedUser) {
    this.id = data.id;
    this.fullName = data.fullName;
    this.isAdmin = data.isAdmin;
    this.initialized = true;
  }

  destroy() {
    this.id = null;
    this.fullName = null;
    this.isAdmin = null;
    this.initialized = false;
  }
}
