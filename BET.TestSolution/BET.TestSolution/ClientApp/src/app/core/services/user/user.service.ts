import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { User } from '../../../shared/models/user';
import { ActionResponse } from '../../../shared/models/actionData';
import { UserProfile } from '../../../shared/models/userProfile';
import { UpdateUserProfile } from '../../../shared/models/updateUserProfile';

@Injectable({ providedIn: 'root' })
export class UserService {
  httpOptions = {
    headers: new HttpHeaders({
      Accept: 'application/json',
      'Content-Type': 'application/json'
    }),
    mode: 'cors',
    credentials: 'include',
    withCredentials: true
  };

  constructor( private http: HttpClient ) { }

  public getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${environment.apiUrl}/user`, this.httpOptions);
  }

  public addUser(user: UserProfile): Observable<ActionResponse> {
    return this.http.post<ActionResponse>(`${environment.apiUrl}/user`, user, this.httpOptions);
  }

  public updateUser(user: UpdateUserProfile, id: number): Observable<ActionResponse> {
    return this.http.put<ActionResponse>(`${environment.apiUrl}/user/${id}`, user, this.httpOptions);
  }

  public deleteUser(id: number): Observable<ActionResponse> {
    return this.http.delete<ActionResponse>(`${environment.apiUrl}/user/${id}`, this.httpOptions);
  }
}
