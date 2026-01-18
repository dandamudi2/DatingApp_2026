import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { RegisterCred, User } from '../../types/user';

@Injectable({ providedIn: 'root' })
export class AccountService {
  constructor(private http: HttpClient) {
    // restore user from localStorage if present
    try {
      const userJson = localStorage.getItem('user');
      if (userJson) {
        const user: User = JSON.parse(userJson);
        this.currentUser.set(user);
      }
    } catch (e) {
      console.error('Failed to parse stored user', e);
    }
  }

  baseUrl = 'https://localhost:7150/api/';

  // signal to hold the currently logged-in user
  public currentUser = signal<User | null>(null);

  login(creds: any) {
    return this.http.post<User>(`${this.baseUrl}account/login`, creds).pipe(
      tap((user) => {
       
        try {
          this.setCurrentUser(user);
        } catch (e) {
          console.error('Failed to save user to localStorage', e);
        }
      })
    );
  }

  register(creds: RegisterCred) {
    return this.http.post<User>(`${this.baseUrl}account/register`, creds).pipe(
      tap((user) => {
         this.setCurrentUser(user)
        try {
          localStorage.setItem('user', JSON.stringify(user));
        } catch (e) {
          console.error('Failed to save user to localStorage', e);
        }
      })
    );
  }

  setCurrentUser(user: User | null) {
    this.currentUser.set(user);
    if (user) {
      try {
        localStorage.setItem('user', JSON.stringify(user));
         this.currentUser.set(user);
      } catch (e) {
        console.error('Failed to save user to localStorage', e);
      }
    } else {
      localStorage.removeItem('user');
    }
  }

  logout() {
    this.currentUser.set(null);
    try {
      localStorage.removeItem('user');
    } catch (e) {
      console.error('Failed to remove user from localStorage', e);
    }
  }
}
