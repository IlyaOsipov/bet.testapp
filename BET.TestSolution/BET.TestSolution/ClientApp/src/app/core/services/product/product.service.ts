import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { ActionResponse } from '../../../shared/models/actionData';
import {Product, ProductProfile} from '../../../shared/models/product';

@Injectable({ providedIn: 'root' })
export class ProductService {
  httpOptions = {
    headers: new HttpHeaders({
      Accept: 'application/json',
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin' : '*'
    }),
    dataType: 'json'
  };

  constructor( private http: HttpClient ) { }

  public getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${environment.apiUrl}/product`, this.httpOptions);
  }

  public addProduct(product: Product): Observable<ActionResponse> {
    return this.http.post<ActionResponse>(`${environment.apiUrl}/product`, product, this.httpOptions);
  }

  public updateProduct(productProfile: ProductProfile, id: number): Observable<ActionResponse> {
    return this.http.put<ActionResponse>(`${environment.apiUrl}/product/${id}`, productProfile, this.httpOptions);
  }

  public deleteProduct(id: number): Observable<ActionResponse> {
    return this.http.delete<ActionResponse>(`${environment.apiUrl}/product/${id}`, this.httpOptions);
  }
}
