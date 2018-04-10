import { AuthService } from './../../auth/auth.service';
import { ToasterService, ToasterConfig } from 'angular5-toaster';
import { SaveProduct, Product } from '../../models/product';
import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute, Router } from '@angular/router'; 
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';
import 'rxjs/add/Observable/forkJoin';
import '@angular/forms';
@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})

export class ProductFormComponent implements OnInit {
  categories: any[];
  platforms: any[];
  product: SaveProduct = {
    id: 0,
    categoryId: 0,
    platformId: 0,
    authSub: '',
    name: '',
    sellOrBuy: '',
    ageLimit: 0,
    description: '',
    price: 0,
    releaseDate: ''
  };

  public toasterconfig : ToasterConfig = 
  new ToasterConfig({
      showCloseButton: false, 
      tapToDismiss: false, 
      timeout: 5000,
      animation: 'fade'
  });

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router,
    public auth: AuthService,
    private toasterService: ToasterService) {
      route.params.subscribe(p => {
        this.product.id = +p['id'] || 0;
      })
     }

  ngOnInit() {
    var sources = [
      this.productService.getCategories(),
      this.productService.getPlatforms(),
    ];  

    if (this.product.id)
      sources.push(this.productService.getProduct(this.product.id));    

    Observable.forkJoin(sources).subscribe(data => {
      this.categories = data[0];
      this.platforms = data[1];

      if (this.product.id) {
        this.setProduct(data[2]);
      }
    }, err => {
      if (err.status == 404)
        this.router.navigate(['/']); //TODO: ERROR PAGE
        console.log("ERROR: " + err);
    });      
  }

  private setProduct(p: Product) {
    this.product.id = p.id;
    this.product.ageLimit = p.ageLimit;
    this.product.categoryId = p.categoryId;
    this.product.description = p.description;
    this.product.name = p.name;
    this.product.platformId = p.platformId;
    this.product.price = p.price;
    this.product.releaseDate = p.releaseDate;
    this.product.sellOrBuy = p.sellOrBuy;
    this.product.authSub = p.authSub;
  }

  submit() {
    var result$ = (this.product.id) ? this.productService.update(this.product) : this.productService.create(this.product);
    result$.subscribe(product => {
      this.toasterService.pop("success","Success","Data was sucessfully saved.");
      this.router.navigate(['/counter'])
    });
  }
}
