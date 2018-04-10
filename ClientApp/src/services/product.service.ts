import { AuthService } from './../auth/auth.service';
import { Observable } from 'rxjs/Observable';
import { SaveProduct } from './../models/product';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class ProductService {
  private readonly productEndpoint = '/api/products';

  constructor(
    private http: HttpClient,
    private authService: AuthService
  ) { }

  ngOnInit() { }

  getPlatforms() {
    return this.http.get(this.productEndpoint + '/platforms')
      .pipe(
        catchError(this.handleError))
  }

  getPlatform(id) {
    return this.http.get(this.productEndpoint + '/platforms/' + id)
      .pipe(
        catchError(this.handleError));
  }

  getCategory(id) {
    return this.http.get(this.productEndpoint + '/categories/' + id)
      .pipe(
        catchError(this.handleError))
  }

  getCategories() {
    return this.http.get(this.productEndpoint + '/categories')
      .pipe(
        catchError(this.handleError))
  }

  create(product: SaveProduct) {
    product.authSub = this.authService.user.authSub;
    return this.http.post(this.productEndpoint, product, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${this.authService.accessToken}`)
        .set("content-type", "application/json")
    })
      .pipe(
        catchError(this.handleError))
  }

  getProduct(id) {
    return this.http.get(this.productEndpoint + '/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.authService.accessToken}`)
    })
      .pipe(
        catchError(this.handleError))
  }

  getProducts(filter) {
    return this.http.get(this.productEndpoint + '?' + this.toQueryString(filter), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${this.authService.accessToken}`)
    })
      .pipe(
        catchError(this.handleError))
  }

  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }
    return parts.join('&');
  }

  update(product: SaveProduct) {
    return this.http.put(this.productEndpoint + '/' + product.id, product, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${this.authService.accessToken}`)
    })
      .pipe(
        catchError(this.handleError))
  }

  public delete(id) {
    return this.http.delete(this.productEndpoint + '/' + id, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${this.authService.accessToken}`)
    })
      .pipe(
        catchError(this.handleError))
  }

  private handleError(err: HttpErrorResponse | any) {
    console.error('An error occurred', err);
    return Observable.throw(err.message || err);
  }
}
