import { UserService } from './../../services/user.service';
import { SaveWishlist } from './../../models/wishlist';
import { AuthService } from './../../auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { ProductService } from './../../services/product.service';
import { Product } from './../../models/product';
import { WishlistService } from '../../services/wishlist.service';

@Component({
    templateUrl: 'wishlist-list.component.html'
})

export class WishListComponent implements OnInit {
    private readonly PAGE_SIZE: number = 10;

    queryResult: any = {};
    query: any = {
        pageSize: this.PAGE_SIZE,
        authSub: this.auth.user.authSub
    }
    productIds: any = [];
    columns = [
        { title: 'Id' },
        { title: 'Product Name', key: 'name', isSortable: true },
        { title: 'Sell Or Buy', key: 'sellOrBuy', isSortable: true },
        { title: 'Age Limit', key: 'ageLimit', isSortable: true },
        { title: 'Description', key: 'description', isSortable: true },
        { title: 'Release Date', key: 'releaseDate', isSortable: true },
        { title: 'Price', key: 'price', isSortable: true },
        {}
    ];

    constructor(
        public auth: AuthService,
        private productService: ProductService,
        private wishlistService: WishlistService,
        private userService: UserService
    ) { }

    ngOnInit() {
        this.populateProducts();
    }

    private populateProducts() {
        this.productService.getProducts(this.query)
            .subscribe(result => this.queryResult = result);
    }

    onFilterChange() {
        this.query.page = 1;
        this.populateProducts();
    }

    resetFilter() {
        this.query = {
            page: 1,
            pageSize: this.PAGE_SIZE
        };
        this.populateProducts();
    }

    sortBy(columnName) {
        if (this.query.sortBy === columnName) {
            this.query.isSortAscending = !this.query.isSortAscending;
        } else {
            this.query.sortBy = columnName;
            this.query.isSortAscending = true;
        }
        this.populateProducts();
    }

    onPageChange(page) {
        this.query.page = page;
        this.populateProducts();
    }
}