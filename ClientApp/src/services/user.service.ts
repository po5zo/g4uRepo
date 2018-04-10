import { SaveUser } from './../models/user';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { catchError } from 'rxjs/operators';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class UserService {
  private readonly userEndpoint = '/api/users';

  constructor(
    private http: HttpClient
  ) { }

  ngOnInit() { }

  create(user: SaveUser) {
    return this.http.post(this.userEndpoint, user)
      .pipe(
        catchError(this.handleError))
  }

  getUser(filter) {
    return this.http.get(this.userEndpoint + '?' + this.toQueryString(filter))
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

  private handleError(err: HttpErrorResponse | any) {
    console.error('An error occurred', err);
    return Observable.throw(err.message || err);
  }
}
