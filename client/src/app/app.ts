import { Component, inject, OnInit, signal } from '@angular/core';
import { NavComponent } from '../layout/nav/nav';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { AccountService } from '../core/Services/account.service';
import { HomeComponent } from '../features/home/home';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavComponent,HomeComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  protected accountService = inject(AccountService);

  //declare members variable with signal
  protected members = signal<any[]>([]);
  title = "Angular App";

  constructor(private http: HttpClient) {}

  async ngOnInit() {
  
  this.members.set(await this.getMembers());
  
  }

  setCurrentUser(){

    const userString = localStorage.getItem('user')
    if(!userString) return;
    const user = JSON.parse(userString);

    this.accountService.setCurrentUser

  }

  
  async getMembers() {
    try {
            return lastValueFrom(this.http.get<any[]>('https://localhost:7150/api/members'));
          
          console.log('Members:', this.members());
        } catch (err) {
          console.error('Error fetching members:', err);
          throw err;

      }
    }

}
