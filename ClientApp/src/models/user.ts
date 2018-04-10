export interface User {
    id: number;
    authSub: string;
    givenName: string;
    familyName: string;
    fullName: string;
    locale: string;
    email_Verified: boolean;
    roles: string;
    email: string;
    phoneNumber: string;
    defaultImageLink: string;
    gender: string;
    rating: number;
    updatedAt: string;
}

export interface SaveUser {
    id: number;
    authSub: string;
    givenName: string;
    familyName: string;
    fullName: string;
    locale: string;
    email_Verified: boolean;
    roles: string;
    email: string;
    phoneNumber: string;
    defaultImageLink: string;
    gender: string;
    rating: number;
    updatedAt: string;
}