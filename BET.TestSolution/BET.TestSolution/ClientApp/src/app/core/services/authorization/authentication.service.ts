import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { LoggedUser } from '../../../shared/models/loggedUser';
import {LoggedUserResponse} from '../../../shared/models/loggedUserResponse';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  httpOptions = {
    headers: new HttpHeaders({
      Accept: 'application/json',
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin' : '*'
    })
  };

  constructor( private http: HttpClient ) { }

  public authenticate(login: string, password: string): Observable<LoggedUserResponse> {
    return this.http.post<LoggedUserResponse>(`${environment.apiUrl}/authentication/auth`, { login, password }, this.httpOptions);
  }
}
