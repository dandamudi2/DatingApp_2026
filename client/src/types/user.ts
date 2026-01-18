export interface User {
    id:string;
  displayName: string;
  email: string;
  token: string;
  imageUrl?: string | null;
}


export interface RegisterCred {
    email: string;
    displayName: string;
    password: string;
}