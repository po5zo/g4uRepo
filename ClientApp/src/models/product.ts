export interface Product {
    id: number;
    platformId: number;
    categoryId: number;
    name: string;
    price: number;
    ageLimit: number;
    sellOrBuy: string;
    description: string;
    releaseDate: string;
    authSub: string;
}

export interface SaveProduct {
    id: number;
    platformId: number;
    categoryId: number;
    name: string;
    price: number;
    ageLimit: number;
    sellOrBuy: string;
    description: string;
    releaseDate: string;
    authSub: string;
}