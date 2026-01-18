import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AccountService } from '../../core/Services/account.service';


@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './nav.html',
  styleUrls: ['./nav.css']
})
export class NavComponent {


  protected accountService = inject(AccountService);

  protected creds:any = { };



 login(){
    
   this.accountService.login(this.creds).subscribe({
      next: (res) => {
        console.log('Login response:', res);
           this.creds={}
      },
      error: (err) => console.error('Login error:', err),
    });
  }

  logout() {
      this.accountService.logout();
  }

 
}
