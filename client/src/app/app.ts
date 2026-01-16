import { Component, OnInit, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  //declare members variable with signal
  protected members = signal<any[]>([]);
  title = "Angular App";

  constructor(private http: HttpClient) {}

  async ngOnInit() {
  
this.members.set(await this.getMembers());
  
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
