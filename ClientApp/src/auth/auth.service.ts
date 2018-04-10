import { HttpErrorResponse } from '@angular/common/http';
import { Http } from '@angular/http';
import { UserService } from './../services/user.service';
import { Observable } from 'rxjs/Observable';
import { SaveUser } from './../models/user';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { filter, catchError } from 'rxjs/operators';
import * as auth0 from 'auth0-js';
import { element } from 'protractor';

@Injectable()
export class AuthService {
    userProfile: any;
    authenticated: boolean;
    accessToken: string;
    requestedScopes: string;
    user: SaveUser = {
        id: 0,
        authSub: '',
        givenName: '',
        familyName: '',
        fullName: '',
        locale: '',
        email_Verified: null,
        roles: '',
        email: '',
        phoneNumber: '',
        defaultImageLink: '',
        gender: '',
        rating: 0,
        updatedAt: ''
    };

    roles: any[];

    auth0;

    constructor(
        public router: Router,
        private userService: UserService,
        private http: Http) {

    }

    public login(): void {
        this.auth0.authorize();
    }

    private setAuthOptions(auth) {
        var auth0$ = new auth0.WebAuth({
            clientID: auth.clientID,
            domain: auth.domain,
            responseType: auth.responseType,
            audience: auth.audience,
            redirectUri: auth.redirectUri,
            scope: auth.scope
        });
        this.requestedScopes = auth.scope;
        this.auth0 = auth0$;
    }

    public handleAuthentication(): void {
        var authConfig = this.getAuthSettings().subscribe(auth => {
            this.setAuthOptions(auth);
            this.auth0.parseHash((err, authResult) => {
                if (authResult && authResult.accessToken && authResult.idToken) {
                    this.setSession(authResult);
                    this.getProfile((err, profile) => {
                        this.userProfile = profile;
                    })
                    window.location.hash = '';
                    this.router.navigate(['/products']);
                } else if (err) {
                    this.router.navigate(['/counter']); //TODO: ERROR PAGE
                    console.log(`Error: ${err.error}`);
                }
            });
        });
    }

    private getAuthSettings() {
        return this.http.get('/api/settings/auth')
            .map(res => res.json())
            .pipe(
                catchError(this.handleError))
    }

    private setSession(authResult): void {
        // Set the time that the Access Token will expire at
        const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
        const scopes = authResult.scope || this.requestedScopes || '';

        localStorage.setItem('access_token', authResult.accessToken);
        localStorage.setItem('id_token', authResult.idToken);
        localStorage.setItem('expires_at', expiresAt);
        localStorage.setItem('scopes', JSON.stringify(scopes));
        this.accessToken = authResult.accessToken;
    }

    public isAuthenticated(): boolean {
        // Check whether the current time is past the
        // Access Token's expiry time
        const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
        return new Date().getTime() < expiresAt;
    }

    public logout(): void {
        // Remove tokens and expiry time from localStorage
        localStorage.removeItem('access_token');
        localStorage.removeItem('id_token');
        localStorage.removeItem('expires_at');
        localStorage.removeItem('scopes');
        this.userProfile = undefined;
        this.accessToken = undefined;
        this.authenticated = false;
        // Go back to the home route
        this.router.navigate(['/products']);
    }

    public getProfile(cb): void {
        const accessToken = localStorage.getItem('access_token');
        if (!accessToken) {
            throw new Error('Access token must exist to fetch profile');
        }

        this.auth0.client.userInfo(accessToken, (err, profile) => {
            if (profile) {
                this.userProfile = profile;
                this.setUser(this.userProfile);
                console.log(this.user);
                this.userService.create(this.user).subscribe();
                console.log(this.userProfile);
            }
            cb(err, profile);
        });
    }

    private setUser(profile) {
        this.user.authSub = profile.sub;
        if (profile.family_name) this.user.familyName = profile.family_name;
        if (profile.gender) this.user.gender = profile.gender;
        if (profile.given_name) this.user.givenName = profile.given_name;
        if (profile.locale) this.user.locale = profile.locale;
        if (profile.email) this.user.email = profile.email;
        this.user.email_Verified = profile.email_verified;
        this.user.defaultImageLink = profile.picture;
        this.user.updatedAt = profile.updated_at;
        this.roles = profile["https://g4u.com/roles"];
        this.roles.forEach(element => {
            this.user.roles = this.user.roles + element;
        });;
    }

    private handleError(err: HttpErrorResponse | any) {
        console.error('An error occurred', err);
        return Observable.throw(err.message || err);
    }
}