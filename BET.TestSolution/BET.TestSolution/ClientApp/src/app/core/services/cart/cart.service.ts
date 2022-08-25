import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import {ActionResponse} from "../../../shared/models/actionData";
import {Cart} from "../../../shared/models/cart";

@Injectable({ providedIn: 'root' })
export class CartService {
  httpOptions = {
    headers: new HttpHeaders({
      Accept: 'application/json',
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin' : '*'
    })
  };

  constructor( private http: HttpClient ) { }

  public getAllCarts(): Observable<Cart[]> {
    return this.http.get<Cart[]>(`${environment.apiUrl}/cart`, this.httpOptions);
  }

  public addCart(cart: Cart): Observable<ActionResponse> {
    return this.http.post<ActionResponse>(`${environment.apiUrl}/cart`, cart, this.httpOptions);
  }

  public getCartById(id: number): Observable<Cart> {
    return this.http.get<Cart>(`${environment.apiUrl}/cart/${id}`, this.httpOptions);
  }

  public deleteCart(id: number): Observable<ActionResponse> {
    return this.http.delete<ActionResponse>(`${environment.apiUrl}/cart/${id}`, this.httpOptions);
  }
}
