import { UserService } from './../../services/user.service';
import { AuthService } from './../../auth/auth.service';
import { Product } from './../../models/product';
import { ProductService } from './../../services/product.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    templateUrl: 'product-view.component.html'
})
export class ProductViewComponent implements OnInit {
    private readonly PAGE_SIZE: number = 10;

    product: Product;
    productId: number;
    platform: any = {};
    category: any = {};
    categoryId: number;
    platformId: number;
    ownerEmail: string;
    user: any = {};
    query: any = {
        pageSize: this.PAGE_SIZE,
        authSub: ''
    }

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public auth: AuthService,
        private productService: ProductService,
        private userService: UserService) {

        route.params.subscribe(p => {
            this.productId = +p['id'];
            if (isNaN(this.productId) || this.productId <= 0) {
                router.navigate(['/products']); //TODO: ERROR PAGE
                return;
            }
        });
    }

    ngOnInit() {
        var result$ = this.productService.getProduct(this.productId);
        result$.subscribe(product => {          
            this.productService.getProduct(product.id)
                .subscribe(
                    p => {
                        this.product = p,
                        this.query.authSub = p.authSub;
                        this.userService.getUser(this.query)
                            .subscribe(
                                user => this.user = user,                               
                                err => {
                                    if (err.status == 404) {
                                        this.router.navigate(['/products']); //TODO: ERROR PAGE
                                    }
                                }
                            );
                    },
                    err => {
                        if (err.status == 404) {
                            this.router.navigate(['/products']); //TODO: ERROR PAGE
                            return;
                        }
                    }
                );
            this.productService.getCategory(product.categoryId)
                .subscribe(
                    p => this.category = p,
                    err => {
                        if (err.status == 404) {
                            this.router.navigate(['/products']); //TODO: ERROR PAGE
                            return;
                        }
                    }
                );
            this.productService.getPlatform(product.platformId)
                .subscribe(
                    p => this.platform = p,
                    err => {
                        if (err.status == 404) {
                            this.router.navigate(['/products']); //TODO: ERROR PAGE
                            return;
                        }
                    }
                );
        });
    }

    delete() {
        if (confirm("Are you sure?")) {
            this.productService.delete(this.product.id)
                .subscribe(p => {
                    this.router.navigate(['/products']);
                })
        }
    }
}