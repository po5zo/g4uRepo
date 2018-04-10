import { UserService } from './../../services/user.service';
import { SaveWishlist } from './../../models/wishlist';
import { AuthService } from './../../auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { ProductService } from './../../services/product.service';
import { Product } from './../../models/product';
import { WishlistService } from '../../services/wishlist.service';

@Component({
    templateUrl: 'product-list.component.html'
})

export class ProductListComponent implements OnInit {  
    private readonly PAGE_SIZE :number = 10;

    queryResult: any = {};
    query: any = {
        pageSize: this.PAGE_SIZE
    };
    wishlistQuery: any = {
        authSub: this.auth.user.authSub
    }
    existInWishlist: boolean = false;
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
    wishlist: SaveWishlist = {
        id: 0,
        authSub: '',
        productId: 0,
        productIsExist: false
    };

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

    addToWishlist(productId: number) {
        console.log("productId:" + productId + "userId" + this.auth.user.authSub);
        var p = this.productService.getProduct(productId).subscribe(p => {
            if (p) {
                this.wishlist.productId = productId;
                this.wishlist.authSub = this.auth.user.authSub;
                this.wishlist.productIsExist = true;
                this.wishlistService.add(this.wishlist).subscribe();               
            }
        });
    }

    onPageChange(page) {
        this.query.page = page;
        this.populateProducts();
    }

    public isExistInWishlist(productId: number): boolean {    
        this.wishlistQuery.productId = productId;
        this.wishlistService.get(this.wishlistQuery).subscribe(result => {
            if (result != null) this.existInWishlist = true;
        });
        return this.existInWishlist;
    }
}