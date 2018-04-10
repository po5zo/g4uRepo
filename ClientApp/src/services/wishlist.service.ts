import { AuthService } from './../auth/auth.service';
import { SaveWishlist } from './../models/wishlist';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class WishlistService {
    private readonly wishlistEndpoint = '/api/wishlist';

    constructor(
        private http: HttpClient,
        private authService: AuthService
    ) { }

    ngOnInit() { }

    add(wishlist: SaveWishlist) {
        console.log(wishlist)
        return this.http.post(this.wishlistEndpoint, wishlist, {
            headers: new HttpHeaders()
                .set('Authorization', `Bearer ${this.authService.accessToken}`)
                .set("content-type", "application/json")
        })
            .pipe(
                catchError(this.handleError))
    }

    get(filter) {
        return this.http.get(this.wishlistEndpoint + '?' + this.toQueryString(filter), {
            headers: new HttpHeaders()
                .set('Authorization', `Bearer ${this.authService.accessToken}`)
        })
            .pipe(catchError(this.handleError))
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
