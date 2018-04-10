import { WishListComponent } from './wishlist-list/wishlist-list.component';
import { WishlistService } from './../services/wishlist.service';
import { AuthService } from './../auth/auth.service';
import { UserService } from './../services/user.service';
import { CallbackComponent } from './callback/callback.component';
import { ProfileComponent } from './profile/profile.component';
import { PaginationComponent } from './Shared/pagination.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductViewComponent } from './product-view/product-view.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { CdkTableModule } from '@angular/cdk/table';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ProductFormComponent } from './product-form/product-form.component';
import { ProductService } from '../services/product.service';
import { HttpModule } from '@angular/http';
import { ToasterModule, ToasterService } from 'angular5-toaster';


@NgModule({
  exports: [
    CdkTableModule,
  ]
})
export class MaterialModule {}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ProductFormComponent,
    ProductListComponent,
    PaginationComponent,
    ProductViewComponent,
    CallbackComponent,
    ProfileComponent,
    WishListComponent    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    ReactiveFormsModule,
    FormsModule,
    ToasterModule,
    MaterialModule,
    NoopAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: ProductListComponent, pathMatch: 'full' },
      { path: 'product/new', component: ProductFormComponent },
      { path: 'products/edit/:id', component: ProductFormComponent },
      { path: 'products/:id', component: ProductViewComponent },
      { path: 'products', component: ProductListComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'callback', component: CallbackComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'wishlist', component: WishListComponent },
    ])
  ],
  providers: [
    ProductService,
    AuthService,
    UserService,
    WishlistService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
